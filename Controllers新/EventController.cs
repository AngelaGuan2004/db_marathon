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
using System.Numerics;

namespace db_marathon.Controllers
{
    [Route("/[Controller]/[action]")]
    [ApiController]
    [EnableCors("any")]
    public class EventController : Controller
    {
        private readonly SqlSugarClient _db;
        private readonly ILogger<EventController> _logger; // 声明日志记录器

        public EventController(ILogger<EventController> logger)
        {
            _logger = logger; // 初始化日志记录器
            //获取数据库连接
            dbORM dborm = new dbORM();
            _db = dborm.getInstance();
        }

        //上传Event
        [HttpPost]
        public async Task<IActionResult> add_event([FromBody] Event event_)
        {
            _logger.LogInformation("收到赛事数据: {@Event}", event_); // 记录收到的数据

            try
            {
                await _db.Insertable(event_).ExecuteCommandAsync();
                _logger.LogInformation("成功插入赛事数据: {@Event}", event_); // 记录插入成功
                return Ok(true);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "插入选手赛事失败: {@Event}", event_); // 记录错误信息

                return BadRequest(false);
            }

        }

        //event修改信息
        [HttpPatch]
        public async Task<IActionResult> update_event([FromBody] Event event_)
        {
            _logger.LogInformation("收到赛事数据: {@Event}", event_); // 记录收到的数据

            try
            {
                await _db.Updateable(event_).ExecuteCommandAsync();
                _logger.LogInformation("成功更改赛事数据: {@Event}", event_); // 记录更改成功
                return Ok(true);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "更改选赛事据失败: {@Event}", event_); // 记录错误信息

                return BadRequest(false);
            }
        }

        //删除赛事
        [HttpDelete]
        public async Task<IActionResult> delete_event([FromBody] Event event_)
        {
            _logger.LogInformation("收到赛事数据: {@Event}", event_); // 记录收到的数据
            try
            {
                await _db.Deleteable<Event>(new Event() { Id = event_.Id }).ExecuteCommandAsync();
                _logger.LogInformation("成功删除赛事数据: {@Event}", event_); // 记录删除成功
                return Ok(true);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "删除选赛事据失败: {@Event}", event_); // 记录错误信息

                return BadRequest(false);
            }
        }

        //获取赛事详情
        [HttpGet]
        public async Task<IActionResult> get_by_id([FromBody] Event event_)
        {
            _logger.LogInformation("收到赛事数据: {@Event}", event_); // 记录收到的数据
            try
            {
                var existingEvent_ = await _db.Queryable<Event>()
                .Where(it => it.Id == event_.Id)
                .FirstAsync();

                var existingWeather = await _db.Queryable<Weather>()
                .Where(it => it.Id == event_.Id)
                .FirstAsync();

                if (existingEvent_ != null)
                {
                    var eventAndWeather = new
                    {
                        Event = existingEvent_,
                        Weather = existingWeather
                    };
                    _logger.LogInformation("赛事查找成功: {@Event}", event_); // 记录成功
                    return Ok(eventAndWeather);
                }
                else
                {
                    _logger.LogWarning("赛事查找失败");
                    return Unauthorized(null);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "赛事查找失败", event_); // 记录错误信息

                return BadRequest(false);
            }
        }


        ////查询对应种类的赛事
        //[HttpGet]
        //public List<Event> get_by_category(string Category)
        //{
        //    dbORM dborm = new dbORM();
        //    SqlSugarClient db = dborm.getInstance();//获取数据库连接

        //    try
        //    {
        //        List<Event> eventlist = db.Queryable<Event>().Where(it => it.Category == Category).ToList();
        //        return eventlist;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        ////查询对应名称的赛事
        //[HttpGet]
        //public string get_by_name(string Name)
        //{
        //    Event event1 = new Event();
        //    dbORM dborm = new dbORM();
        //    SqlSugarClient db = dborm.getInstance();//获取数据库连接

        //    event1 = db.Queryable<Event>().Where(it => it.Name == Name).First();
        //    return JsonSerializer.Serialize(event1);
        //}

        
    }
}
