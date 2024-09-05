using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using SqlSugar;
using MarathonMaster;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using MarathonMaster.Models;
using System.Drawing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Infrastructure;

namespace MarathonMaster.Controllers
{
    [Route("/[Controller]/[action]")] // 指定路由模板和控制器的基本配置
    [ApiController]
    [EnableCors("any")]
    //[EnableCors("AllowAllOrigins")]  // 启用跨域请求    

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
            _logger.LogInformation("收到插入接驳车班次的数据: {@shuttlecari.Id}", shuttlecari.Id); // 记录收到的数据

            try
            {
                //await _db.Insertable(shuttlecari).ExecuteCommandAsync();
                List<Shuttlecar> query = await _db.Queryable<Shuttlecar>()
                                           .Where(s => s.Id == shuttlecari.Id)
                                           .ToListAsync();
                if (query.Count == 0)
                {
                    await _db.Insertable(shuttlecari).ExecuteCommandAsync();
                    _logger.LogInformation("成功插入接驳车班次的数据: {@shuttlecari.Id}", shuttlecari.Id); // 记录插入成功
                    return Ok(true); //true表示成功
                }
                else
                {
                    _logger.LogInformation("插入接驳车班次的数据失败，因为已存在该id的接驳车: {@shuttlecari.Id}", shuttlecari.Id); // 记录插入成功
                    return Ok(0);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "插入接驳车班次的失败: {@shuttlecari}", shuttlecari); // 记录错误信息

                return BadRequest(false); //false表示失败
            }
        }

        //接驳车查询，根据赛事的event_id查出所有的接驳车信息
        [HttpGet]
        public async Task<IActionResult> inquire_shuttlecar_info([FromQuery] string event_id)
        {
            _logger.LogInformation("收到查询接驳车班次的event_id数据: {@event_id}", event_id); // 记录收到的数据

            try
            {
                
                List<Shuttlecar> cars = await _db.Queryable<Shuttlecar>().Where(e => e.Event_Id == event_id).ToListAsync();
               
                  
                _logger.LogInformation("成功查询到该event_id的接驳车班次信息: {@event_id}", event_id); // 记录查询成功
                return Ok(cars);
              
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "查询该event_id的接驳车班次信息失败: {@event_id}", event_id); // 记录错误信息

                return BadRequest(false); //false表示失败
            }
        }
        //接驳车删除，根据接驳车id删除相应的接驳车信息
        [HttpDelete]
        public async Task<IActionResult> delete_shuttlecar([FromQuery] int shuttlecar_id)
        {
            _logger.LogInformation("收到删除接驳车班次的id数据: {@shuttlecar_id}", shuttlecar_id); // 记录收到的数据

            try
            {

                await _db.Deleteable<Shuttlecar>().In(shuttlecar_id).ExecuteCommandAsync();

                _logger.LogInformation("成功删除id的接驳车班次信息: {@shuttlecar_id}", shuttlecar_id); // 记录查询成功
                return Ok(shuttlecar_id);

            }

            catch (Oracle.ManagedDataAccess.Client.OracleException ex) when (ex.Number == 2292)
            {
                // 2292 是 Oracle 错误代码 ORA-02292，表示违反外键约束
                _logger.LogInformation("删除id的接驳车班次信息失败，因为该已存在该id的接驳车: {@shuttlecar_id}", shuttlecar_id); 
                return Ok(new { Success = false, Message = "Cannot delete this record because it is referenced by other records.", ShuttlecarId = shuttlecar_id });
            }
            /*catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                _logger.LogError(ex, "无法删除ID为 {@shuttlecar_id} 的接驳车班次信息，因为该记录被引用", shuttlecar_id);
                // 检查异常是否与外键约束有关
                if (ex.InnerException?.Message.Contains("子记录") == true)
                {
                    return Ok("Cannot delete this record because it is referenced by other records.");
                }
                // 处理其他类型的数据库更新异常
                return StatusCode(500, "An error occurred while trying to delete the record.");
            }*/

            catch (System.Exception ex)
            {
                _logger.LogError(ex, "删除该id的接驳车班次信息失败: {@shuttlecar_id}", shuttlecar_id); // 记录错误信息

                return BadRequest(false); //false表示失败
            }
        }
    }

}
