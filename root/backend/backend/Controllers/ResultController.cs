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
using MarathonMaster;


namespace MarathonMaster.Controllers
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
        public async Task<IActionResult> search_history_result([FromQuery] int Player_Id)
        {
            // 使用联表查询将 Result_ 中的 Event_Id 对应到 Event.Name
            var resultsWithEventNames = await _db.Queryable<Result_>()
                .Where(it => it.Player_Id == Player_Id)
                .InnerJoin<Event>((result, event_) => result.Event_Id == event_.Id)
                .Select((result, event_) => new
                {
                    result.Id,
                    result.Gun_Result,
                    result.Net_Result,
                    result.Player_Id,
                    result.Event_Id,
                    Event_Name = event_.Name, // 获取 Event 的名字
                    result.Rank,
                    result.Gender_Rank
                })
                .ToListAsync();

            if (resultsWithEventNames.Count > 0)
            {
                // _logger.LogInformation("查找Player_Id对应的成绩信息成功: {@resultsWithEventNames}", resultsWithEventNames);
                return Ok(resultsWithEventNames);
            }
            else
            {
                // _logger.LogInformation("无信息");
                return Ok("无信息");
            }
        }

        // 查询选手历史半马成绩
        [HttpGet]
        public async Task<IActionResult> search_history_half_result([FromQuery] int Player_Id)
        {
            var halfMarathonResults = await _db.Queryable<Result_>()
                .InnerJoin<Event>((result, event_) => result.Event_Id == event_.Id && event_.Category == "半马")
                .Where(result => result.Player_Id == Player_Id)
                .OrderBy(result => result.Net_Result)  // 按净成绩由小到大排序
                .Select((result, event_) => new
                {
                    result.Id,
                    result.Gun_Result,
                    result.Net_Result,
                    result.Player_Id,
                    result.Event_Id,
                    Event_Name = event_.Name, // 获取 Event 的名字
                    result.Rank,
                    result.Gender_Rank
                })
                .ToListAsync();

            if (halfMarathonResults.Count > 0)
            {
                // _logger.LogInformation("查找Player_Id对应的半马成绩信息成功: {@halfMarathonResults}", halfMarathonResults);
                return Ok(halfMarathonResults);
            }
            else
            {
                // _logger.LogInformation("无半马成绩信息");
                return Ok("无信息");
            }
        }

        // 查询选手历史全马成绩
        [HttpGet]
        public async Task<IActionResult> search_history_full_result([FromQuery] int Player_Id)
        {
            var fullMarathonResults = await _db.Queryable<Result_>()
                .InnerJoin<Event>((result, event_) => result.Event_Id == event_.Id && event_.Category == "全马")
                .Where(result => result.Player_Id == Player_Id)
                .OrderBy(result => result.Net_Result)  // 按净成绩由小到大排序
                .Select((result, event_) => new
                {
                    result.Id,
                    result.Gun_Result,
                    result.Net_Result,
                    result.Player_Id,
                    result.Event_Id,
                    Event_Name = event_.Name, // 获取 Event 的名字
                    result.Rank,
                    result.Gender_Rank
                })
                .ToListAsync();

            if (fullMarathonResults.Count > 0)
            {
                // _logger.LogInformation("查找Player_Id对应的全马成绩信息成功: {@fullMarathonResults}", fullMarathonResults);
                return Ok(fullMarathonResults);
            }
            else
            {
                // _logger.LogInformation("无全马成绩信息");
                return Ok("无信息");
            }
        }


        //查询选手某次成绩
        [HttpGet]
        public async Task<IActionResult> search_result([FromQuery] int Player_Id, [FromQuery] string Event_id)
        {
            // _logger.LogInformation("收到的数据: {@Player_Id}", Player_Id); // 记录收到的数据

            try
            {
                Result_ results = await _db.Queryable<Result_>()
                                  .Where(it => it.Player_Id == Player_Id && it.Event_Id == Event_id)
                                  .SingleAsync(); ;
                if (results != null)
                {
                    // _logger.LogInformation("査找的成绩信息:{ @results}", results);
                    return Ok(results);
                }
                else
                {
                    // _logger.LogInformation("无信息");
                    return Ok("无信息");
                }
            }
            catch (System.Exception ex)
            {
                // _logger.LogError(ex, "失败: {@Player_Id}", Player_Id); // 记录错误信息

                return BadRequest(false);
            }
        }


        //录入完赛信息
        [HttpPost]
        public async Task<IActionResult> add_result([FromBody] List<Result_> resultList)
        {
            // _logger.LogInformation("收到成绩数据: {@Volunteer}", resultList); // 记录收到的数据

            try
            {
                foreach (var result_ in resultList)
                {
                    await _db.Insertable(result_).ExecuteCommandAsync();
                    // _logger.LogInformation("成功插入成绩数据: {@result}", result_); // 记录插入成功

                }

                return Ok(true);
            }
            catch (System.Exception ex)
            {
                // _logger.LogError(ex, "失败: {@result}", resultList); // 记录错误信息

                return BadRequest($"更改数据失败: {ex.Message}");
            }
        }

        // 获取前N名选手列表
        [HttpGet]
        public async Task<IActionResult> get_top_players([FromQuery] string event_id, [FromQuery] int topN, [FromQuery] string gender)
        {
            // _logger.LogInformation("收到请求: event_id={@event_id}, topN={@topN}", event_id, topN);

            try
            {
                // 查询所有符合条件的成绩，并联接 Player 表和 Event 表
                var query = _db.Queryable<Result_>()
                               .InnerJoin<Player>((result, player) => result.Player_Id == player.Id)
                               .Where((result, player) => result.Event_Id == event_id);

                if (gender != "全部")
                {
                    // 根据性别筛选
                    query = query.Where((result, player) => player.Gender == gender);
                }

                // 按排名排序并取前N名
                var topPlayers = await query
                                     .OrderBy(result => result.Rank)
                                     .Select((result, player) => new
                                     {
                                         result.Id,
                                         result.Rank,
                                         result.Gender_Rank,
                                         result.Net_Result,
                                         result.Gun_Result,
                                         Player_Id = player.Id,  // 选手ID
                                         player.Name,  // 选手姓名
                                         player.Gender,  // 选手性别
                                     })
                                     .Take(topN)
                                     .ToListAsync();

                if (topPlayers != null)
                {
                    // _logger.LogInformation("成功获取前{@topN}名选手数据: {@topPlayers}", topN, topPlayers);
                    return Ok(topPlayers);
                }
                else
                {
                    // _logger.LogInformation("未找到符合条件的选手");
                    return NotFound(null);
                }
            }
            catch (System.Exception ex)
            {
                // _logger.LogError(ex, "获取前{@topN}名选手数据失败: event_id={@event_id}", topN, event_id);
                return BadRequest($"获取数据失败: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> get_last_players([FromQuery] string event_id, [FromQuery] int lastN)
        {
            // _logger.LogInformation("收到请求: event_id={@event_id}, lastN={@lastN}", event_id, lastN);

            try
            {
                List<Result_> lastPlayers = await _db.Queryable<Result_>()
                                                     .Where(it => it.Event_Id == event_id)
                                                     .OrderByDescending(it => it.Rank) // 倒序排列，Rank假设为选手的名次
                                                     .Take(lastN)
                                                     .ToListAsync();

                if (lastPlayers.Count > 0)
                {
                    // _logger.LogInformation("成功获取最后{@lastN}名选手数据: {@lastPlayers}", lastN, lastPlayers);
                    return Ok(lastPlayers);
                }
                else
                {
                    // _logger.LogInformation("未找到符合条件的选手");
                    return NotFound(null);
                }
            }
            catch (System.Exception ex)
            {
                // _logger.LogError(ex, "获取最后{@lastN}名选手数据失败: event_id={@event_id}", lastN, event_id);
                return BadRequest($"获取数据失败: {ex.Message}");
            }
        }

        //获取某场比赛下所有人的成绩信息
        [HttpGet]
        public async Task<IActionResult> get_all_results_by_eventid([FromQuery] string event_id)
        {
            // _logger.LogInformation("收到请求: event_id={@event_id}", event_id);

            try
            {
                // 查询所有符合条件的成绩，并获取对应的选手姓名
                var allResults = await _db.Queryable<Result_>()
                                          .InnerJoin<Player>((result, player) => result.Player_Id == player.Id)
                                          .Where((result, player) => result.Event_Id == event_id)
                                          .OrderBy(result => result.Rank)  // 按排名排序
                                          .Select((result, player) => new
                                          {
                                              result.Id,
                                              result.Rank,
                                              result.Gender_Rank,
                                              result.Net_Result,
                                              result.Gun_Result,
                                              //Result = result,
                                              PlayerName = player.Name
                                          })
                                          .ToListAsync();

                if (allResults.Count > 0)
                {
                    // _logger.LogInformation("成功获取赛事ID为{@event_id}的所有成绩数据", event_id);
                    return Ok(allResults);
                }
                else
                {
                    // _logger.LogInformation("未找到赛事ID为{@event_id}的成绩数据", event_id);
                    return NotFound("未找到对应的成绩数据");
                }
            }
            catch (System.Exception ex)
            {
                // _logger.LogError(ex, "获取赛事ID为{@event_id}的成绩数据失败", event_id);
                return BadRequest($"获取数据失败: {ex.Message}");
            }
        }
    }

}