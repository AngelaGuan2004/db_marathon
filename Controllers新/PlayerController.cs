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
using Microsoft.Extensions.Logging;


namespace db_marathon.Controllers
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
            _logger.LogInformation("收到参赛数据: {@Participate}", participate); // 记录收到的数据

            try
            {
                await _db.Insertable(participate).ExecuteCommandAsync();
                _logger.LogInformation("成功报名: {@Participate}", participate); // 记录插入成功
                return Ok(true);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "报名失败: {@Participate}", participate); // 记录错误信息

                return BadRequest(false);
            }
        }

        //查询中签信息、参赛号码
        [HttpGet]
        public async Task<IActionResult> search_participate([FromQuery] Participate participate)
        {
            _logger.LogInformation("收到参赛数据: {@Participate}", participate); // 记录收到的数据

            try
            {
                var existingParticipate = await _db.Queryable<Participate>()
                .Where(it => it.Player_Id == participate.Player_Id && it.Event_Id == participate.Event_Id)
                .FirstAsync();

                if (existingParticipate.Number_ != null)
                {
                    _logger.LogInformation("选手抽签成功: {@Participate}", participate); // 记录抽签成功
                    return Ok(existingParticipate.Number_);
                }
                else
                {
                    _logger.LogWarning("抽签不中");
                    return Unauthorized(false);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "选手抽签失败: {@Participate}", participate); // 记录错误信息

                return BadRequest(false);
            }
        }

        //删除报名
        [HttpDelete]
        public async Task<IActionResult> delete_participate([FromBody] Participate participate)
        {
            _logger.LogInformation("收到参赛数据: {@Participate}", participate); // 记录收到的数据

            try
            {
                await _db.Deleteable<Participate>(new Participate() { Event_Id = participate.Event_Id,Player_Id= participate .Player_Id}).ExecuteCommandAsync();
                _logger.LogInformation("成功删除参赛数据: {@Event}", participate); // 记录删除成功
                return Ok(true);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "删除参赛数据失败: {@Event}", participate); // 记录错误信息

                return BadRequest(false);
            }
        }

        // 查询选手报名的所有比赛ID
        [HttpGet]
        public async Task<IActionResult> get_events_by_playerid(int Id)
        {
            try
            {
                // 查询数据库，获取指定选手ID报名的所有比赛ID列表
                var eventIds = await _db.Queryable<Participate>()
                                        .Where(p => p.Player_Id == Id)
                                        .Select(p => p.Event_Id)
                                        .ToListAsync();

                if (eventIds != null && eventIds.Count > 0)
                {
                    _logger.LogInformation("查询成功, 选手ID: {PlayerId}, 比赛ID列表: {@EventIds}", Id, eventIds);
                    return Ok(eventIds);
                }
                else
                {
                    _logger.LogWarning("未找到选手ID: {PlayerId} 的报名记录", Id);
                    return NotFound("未找到该选手的报名记录");
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "查询选手ID: {PlayerId} 的报名记录时出错", Id);
                return BadRequest("查询失败");
            }
        }

        // 根据比赛id和选手id查询比赛号码
        [HttpGet]
        public async Task<IActionResult> get_number_by_eventid_and_playerid(int eventId, int playerId)
        {
            try
            {
                // 查询数据库，获取匹配的比赛号码
                var participateRecord = await _db.Queryable<Participate>()
                                                 .Where(p => p.Event_Id == eventId && p.Player_Id == playerId)
                                                 .Select(p => p.Number_)
                                                 .FirstAsync();

                if (participateRecord != null)
                {
                    _logger.LogInformation("查询成功, 比赛ID: {EventId}, 选手ID: {PlayerId}, 比赛号码: {Number}", eventId, playerId, participateRecord);
                    return Ok(participateRecord); // 返回比赛号码
                }
                else
                {
                    _logger.LogWarning("未找到比赛ID: {EventId} 和选手ID: {PlayerId} 对应的比赛号码", eventId, playerId);
                    return NotFound("未找到对应的比赛号码");
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "查询比赛ID: {EventId} 和选手ID: {PlayerId} 的比赛号码时出错", eventId, playerId);
                return BadRequest("查询失败");
            }
        }


    }



}
