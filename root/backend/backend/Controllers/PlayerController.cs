using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using SqlSugar;
using MarathonMaster.Models;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Security.Cryptography;
using System.Numerics;
using Microsoft.Extensions.Logging;


namespace MarathonMaster.Controllers
{
    [Route("/[controller]/[action]")]
    [ApiController]
    [EnableCors("any")]
    public class PlayerController : Controller
    {
        private readonly SqlSugarClient _db;
        private readonly ILogger<PlayerController> _logger; // 声明日志记录器

        public PlayerController(ILogger<PlayerController> logger)
        {
            _logger = logger; // 初始化日志记录器
            //获取数据库连接
            dbORM dborm = new dbORM();
            _db = dborm.getInstance();
        }

        //报名比赛
        [HttpPost]
        public async Task<IActionResult> add_participate([FromBody] Participate participate)
        {
            // _logger.LogInformation("收到参赛数据: {@Participate.Player_Id}", participate.Player_Id); // 记录收到的数据

            try
            {
                //先检查这个人是否是该赛事的志愿者
                List<Schedule> query = await _db.Queryable<Schedule>()
                                         .Where(s => s.Event_Id == participate.Event_Id && s.Volunteer_Id ==participate.Player_Id )
                                         .ToListAsync();
                if (query.Count == 0) //不是志愿者
                {
                    // 查询数据库，获取指定选手ID报名的所有比赛ID列表
                    var eventids = await _db.Queryable<Participate>()
                                           .Where(p => p.Player_Id == participate.Player_Id)
                                           .Select(p => p.Event_Id)
                                           .ToListAsync();
                    if (eventids.Count == 0) //该选手没有报名该赛事
                    {
                        await _db.Insertable(participate).ExecuteCommandAsync();
                        // _logger.LogInformation("成功报名: {@participate.Player_Id}", participate.Player_Id); // 记录插入成功
                        return Ok(true);
                    }
                    else
                    {
                        // _logger.LogInformation("报名失败，该用户已经报名过该赛事: {@participate.Player_Id}", participate.Player_Id); // 记录插入成功
                        return Ok(3);
                    }                   
                }
                else
                {
                    // _logger.LogInformation("报名失败，该用户已是该赛事的志愿者: {@participate.Player_Id}", participate.Player_Id); // 记录插入成功
                    return Ok(2);
                }
                
            }
            catch (System.Exception ex)
            {
                // _logger.LogError(ex, "报名失败: {@Participate}", participate); // 记录错误信息

                return StatusCode(500,ex.Message);
            }
        }

        //查询中签信息、参赛号码
        [HttpGet]
        public async Task<IActionResult> search_participate([FromQuery] int Player_Id, [FromQuery] string Event_Id)
        {
            var existingParticipate = await _db.Queryable<Participate>()
                .Where(it => it.Player_Id == Player_Id && it.Event_Id == Event_Id)
                .FirstAsync();

            var existingPlayer = await _db.Queryable<Player>()
                .Where(it => it.Id == Player_Id)
                .FirstAsync();
            var existingEvent = await _db.Queryable<Event>()
                .Where(it => it.Id == Event_Id)
                .FirstAsync();

            if (existingPlayer != null)
            {
                var participateAndPlayerAndEvent = new
                {
                    Participate = existingParticipate,
                    //Player = existingPlayer,
                    Event=existingEvent
                };
                return Ok(JsonSerializer.Serialize(participateAndPlayerAndEvent));
            }
            else
            {
                return Unauthorized("player id 不存在");
            }
        }


        //删除报名
        [HttpDelete]
        public async Task<IActionResult> delete_participate([FromQuery]int Player_Id, [FromQuery] string Event_Id)
        {
            try
            {
                await _db.Deleteable<Participate>(new Participate() { Event_Id = Event_Id, Player_Id = Player_Id }).ExecuteCommandAsync();
                return Ok(true);
            }
            catch (System.Exception ex)
            {

                return BadRequest(false);
            }
        }

        // 查询选手报名的所有比赛
        [HttpGet]
        public async Task<IActionResult> get_events_by_playerid([FromQuery] int Id)
        {
            try
            {
                // 查询数据库，获取指定选手ID报名的所有比赛ID列表
                var eventIds = await _db.Queryable<Participate>()
                                        .Where(p => p.Player_Id == Id)
                                        .Select(p => p.Event_Id)
                                        .ToListAsync();

                
                var events = await _db.Queryable<Event>()
                                 .Where(e => eventIds.Contains(e.Id))
                                 .ToListAsync();
                    
                // _logger.LogInformation("查询成功, 选手ID: {PlayerId}, 比赛列表: {@Events}", Id, events);
                return Ok(events);
                   
            }
            catch (System.Exception ex)
            {
                // _logger.LogError(ex, "查询选手ID: {PlayerId} 的报名记录时出错", Id);
                return BadRequest("查询失败");
            }
        }

        // 根据比赛id和选手id查询比赛号码
        [HttpGet]
        public async Task<IActionResult> get_number_by_eventid_and_playerid([FromQuery] string eventId, [FromQuery] int playerId)
        {
            try
            {
                // 查询数据库，获取匹配的比赛号码
                var participateRecord = await _db.Queryable<Participate>()
                                                 .Where(p => p.Event_Id == eventId && p.Player_Id == playerId)
                                                 //.Select(p => p.Number_)
                                                 .FirstAsync();

                var _event = await _db.Queryable<Event>()
                                                 .Where(p => p.Id == eventId)
                                                 .FirstAsync();

                if (participateRecord.Number_ != null)
                {
                    // _logger.LogInformation("查询成功, 比赛ID: {EventId}, 选手ID: {PlayerId}, 比赛号码: {Number}", eventId, playerId, participateRecord);
                    return Ok(participateRecord.Number_); // 返回比赛号码
                }
                else if(_event.Is_Drawn=="是")//已经抽签
                {
                    // _logger.LogWarning("未找到比赛ID: {EventId} 和选手ID: {PlayerId} 对应的比赛号码", eventId, playerId);
                    return Ok("未中签");
                }
                else
                {
                    return Ok("尚未抽签");
                }
            }
            catch (System.Exception ex)
            {
                // _logger.LogError(ex, "查询比赛ID: {EventId} 和选手ID: {PlayerId} 的比赛号码时出错", eventId, playerId);
                return BadRequest("查询失败");
            }
        }

    }

}
