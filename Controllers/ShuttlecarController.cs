using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using SqlSugar;
using WebApplication1;

using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using WebApplication1.Models;
using System.Drawing;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    [Route("/[Controller]/[action]")] // 指定路由模板和控制器的基本配置
    [ApiController]
    //[EnableCors("any")]
    [EnableCors("AllowAllOrigins")]  // 启用跨域请求    

    public class ShuttlecarController : ControllerBase
    {

        private readonly SqlSugarClient _db;
        private readonly ILogger<ShuttlecarController> _logger; // 声明日志记录器

        public ShuttlecarController(ILogger<ShuttlecarController> logger)
        {
            _logger = logger; // 初始化日志记录器
            /*
            _db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=system;Password=oracle;",
                DbType = DbType.Oracle,
                IsAutoCloseConnection = true,
            });*/
            //获取数据库连接
            dbORM dborm = new dbORM();
            _db = dborm.getInstance();
        }


        //发布接驳车班次信息
        [HttpPost]
        public async Task<IActionResult> release_shuttlecar_info([FromBody] Shuttlecar shuttlecari) 
        {
            _logger.LogInformation("收到接驳车班次的Shuttlecar数据: {@shuttlecari}", shuttlecari); // 记录收到的数据

            try
            {
                await _db.Insertable(shuttlecari).ExecuteCommandAsync();
                _logger.LogInformation("成功插入接驳车班次的Shuttlecar数据: {@shuttlecari}", shuttlecari); // 记录插入成功
                return Ok(true); //true表示成功
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "插入接驳车班次的Shuttlecar失败: {@shuttlecari}", shuttlecari); // 记录错误信息

                return BadRequest(false); //false表示失败
            }
        }

        //接驳车查询，根据赛事的event_id查出所有的接驳车信息
        [HttpGet]
        public async Task<IActionResult> inquire_shuttlecar_info([FromQuery] int event_id)
        {
            _logger.LogInformation("收到查询接驳车班次的event_id数据: {@event_id}", event_id); // 记录收到的数据

            try
            {
                
                List<Shuttlecar> cars = await _db.Queryable<Shuttlecar>().Where(e => e.Event_Id == event_id).ToListAsync();
                if (cars == null || cars.Count == 0)
                {
                    _logger.LogError("查询该event_id的接驳车班次信息的结果为空: {@event_id}", event_id); // 记录查询结果为空
                    return NotFound();
                }
                  
                _logger.LogInformation("成功查询到该event_id的接驳车班次信息: {@event_id}", event_id); // 记录查询成功
                return Ok(cars);
              
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "查询该event_id的接驳车班次信息失败: {@event_id}", event_id); // 记录错误信息

                return BadRequest(false); //false表示失败
            }
        }
    }

}
