using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using SqlSugar;
using db_marathon.Models;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Security.Cryptography;
using Microsoft.Extensions.Logging; // 引入日志记录命名空间
using System.Numerics;


namespace db_marathon.Controllers
{
    [Route("/[controller]/[action]")]
    [ApiController]
    [EnableCors("any")]
    public class ResultController : ControllerBase
    {
        private readonly SqlSugarClient _db;
        private readonly ILogger<ResultController> _logger; // 声明日志记录器

        public ResultController(ILogger<ResultController> logger)
        {
            _logger = logger; // 初始化日志记录器
            //获取数据库连接
            dbORM dborm = new dbORM();
            _db = dborm.getInstance();
        }

        //查询选手历史成绩
        [HttpGet]
        public async Task<IActionResult> search_result([FromBody] int Player_Id)
        {
            _logger.LogInformation("收到的数据: {@Player_Id}", Player_Id); // 记录收到的数据

            try
            {
                List<Result_> results = await _db.Queryable<Result_>().Where(it => it.Player_Id == Player_Id).ToListAsync();
                if (results.Count > 0)
                {
                    _logger.LogInformation("査找Player_Id对应的成绩信息成功:{ @results}", results);
                    return Ok(results);
                }
                else
                {
                    _logger.LogInformation("无信息");
                    return Unauthorized(null);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "失败: {@Player_Id}", Player_Id); // 记录错误信息

                return BadRequest(false);
            }
        }


        //录入完赛信息
        [HttpPost]
        public async Task<IActionResult> add_result([FromBody] List<Result_> resultList)
        {
            _logger.LogInformation("收到成绩数据: {@Volunteer}", resultList); // 记录收到的数据

            try
            {
                foreach (var result_ in resultList)
                {
                    await _db.Insertable(result_).ExecuteCommandAsync();
                    _logger.LogInformation("成功插入成绩数据: {@result}", result_); // 记录插入成功

                }

                return Ok(true);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "失败: {@result}", resultList); // 记录错误信息

                return BadRequest($"更改数据失败: {ex.Message}");
            }

        }

    }

}
