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
using Microsoft.Extensions.Logging; // 引入日志记录命名空间
using System.Numerics;


namespace MarathonMaster.Controllers
{
    [Route("/[controller]/[action]")]
    [ApiController]
    [EnableCors("any")]
    public class EventManageController : ControllerBase
    {
        private readonly SqlSugarClient _db;
        private readonly ILogger<EventManageController> _logger; // 声明日志记录器

        public EventManageController(ILogger<EventManageController> logger)
        {
            _logger = logger; // 初始化日志记录器
            //获取数据库连接
            dbORM dborm = new dbORM();
            _db = dborm.getInstance();
        }

        //Player抽签
        [HttpPatch]
        public async Task<IActionResult> ballot([FromBody] List<Participate> participateList)
        {
            _logger.LogInformation("收到参赛数据: {@ParticipateList}", participateList); // 记录收到的数据

            try
            {
                foreach (var participate in participateList)
                {
                    if (participate.Number_ != null)
                    {
                        // 选手有参赛号码，更新其信息
                        await _db.Updateable(participate).ExecuteCommandAsync();
                        _logger.LogInformation("成功参赛: {@Participate}", participate); // 记录成功参赛的选手
                    }
                    else
                    {
                        // 选手没有参赛号码，检查其身份并更新role
                        if (participate.Role_ != "normal")
                        {
                            participate.Role_ = "normal";
                            await _db.Updateable(participate).ExecuteCommandAsync();
                            _logger.LogInformation("未抽中，身份更新为normal: {@Participate}", participate); // 记录更新身份的选手
                        }
                    }
                }

                return Ok(true);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "失败: {@ParticipateList}", participateList); // 记录错误信息

                return BadRequest($"更改数据失败: {ex.Message}");
            }

        }

        //特殊身份选拔
        [HttpPatch]
        public async Task<IActionResult> choose_pacer([FromBody] List<Participate> pacerList)
        {
            _logger.LogInformation("收到落选的特殊身份数据: {PacerList}", pacerList); // 记录收到的数据

            try
            {
                string event_id = "0";
                string role_ = "0";
                foreach (var participate in pacerList)
                {
                    if (participate.Role_ != "normal")
                    {
                        role_ = participate.Role_;
                        participate.Role_ = "normal_";
                        //await _db.Updateable(participate).ExecuteCommandAsync();
                        await _db.Updateable<Participate>().Where(p => p.Player_Id == participate.Player_Id && p.Event_Id == participate.Event_Id).ExecuteCommandAsync();
                        _logger.LogInformation("身份更新为normal_: {Participate}", participate); // 记录更新身份的选手
                        event_id = participate.Event_Id;
                    }

                }
                var existingevent = await _db.Queryable<Event>()
                    .Where(it => it.Id == event_id)
                    .FirstAsync();
                if (role_ == "pacer")
                    existingevent.Pacer_Is_Chosen = "是";
                else if (role_ == "first_aid")
                    existingevent.Aid_Is_Chosen = "是";

                await _db.Updateable(existingevent).ExecuteCommandAsync();
                return Ok(true);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "失败: {PacerList}", pacerList); // 记录错误信息
                _logger.LogError("详细错误信息: {ErrorDetails}", ex.ToString()); // 打印详细的错误信息
                return BadRequest($"更改数据失败: {ex.Message}");
            }

        }

    

        //查看特殊身份选拔信息
        [HttpGet]
        public async Task<IActionResult> search_role([FromQuery] int Player_Id, [FromQuery] string Event_Id)
        {
            var existingParticipate = await _db.Queryable<Participate>()
                .Where(it => it.Player_Id == Player_Id && it.Event_Id == Event_Id)
                .FirstAsync();

            var existingEvent = await _db.Queryable<Event>()
                .Where(it => it.Id == Event_Id)
                .FirstAsync();
            if (existingParticipate.Role_ == "normal_")
                return Ok("未被选上");
            else
            {
                if (existingParticipate.Role_ == "pacer")
                {
                    if (existingEvent.Pacer_Is_Chosen == "否")
                        return Ok("尚未选拔");
                    else
                        return Ok("未被选上");
                }
                else
                {
                    if (existingEvent.Aid_Is_Chosen == "否")
                        return Ok("尚未选拔");
                    else
                        return Ok("未被选上");
                }
            }
        }
        //查询某个赛事的所有参与者
        [HttpGet]
        public async Task<IActionResult> get_players_by_event(string eventId)
        {
            try
            {
                // 从数据库中查询某个赛事的所有参与者
                var participants = await _db.Queryable<Participate>()
                                            .Where(p => p.Event_Id == eventId)
                                            .ToListAsync();

                // 检查是否有参与者
                if (participants != null && participants.Count > 0)
                {
                    // 返回参与者列表
                    return Ok(participants);
                }
                else
                {
                    // 如果没有参与者，返回一个空的列表
                    return Ok(new List<Participate>());
                }
            }
            catch (System.Exception ex)
            {
                // 记录错误日志
                _logger.LogError(ex, "获取选手列表失败");

                // 返回错误响应
                return StatusCode(500, "获取选手列表失败");
            }
        }

    }

}