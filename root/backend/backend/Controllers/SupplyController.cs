using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MarathonMaster;
using Microsoft.AspNetCore.Cors;
using MarathonMaster.Models;
using System.Numerics;
using MarathonMaster.Controllers;

namespace Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("any")]
    public class SupplyController : ControllerBase
    {
        private readonly SqlSugarClient _db;
        private readonly ILogger<PlayerController> _logger;

        public SupplyController(ILogger<PlayerController> logger)
        {
            _logger = logger;
            dbORM dborm = new dbORM();
            _db = dborm.getInstance();
        }

        /*添加某件物品*/
        [HttpPost]
        public async Task<IActionResult> add_item([FromBody] Item item)
        {//只用id 名称

            _logger.LogInformation("收到物品数据");

            try
            {
                await _db.Insertable(item).ExecuteCommandAsync();
                _logger.LogInformation("成功插入物品数据");
                return Ok(true);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "插入数据失败: {@Medical_Service}", item); // 记录错误信息

                return BadRequest(new { success = false, message = ex.Message }) ;
            }
        }


        /*删除某件物品*/
        [HttpDelete]
        public async Task<IActionResult> delete_item([FromQuery] int Id)
        {
            try
            {
                int count = await _db.Deleteable<Item>().Where(it => it.Id == Id).ExecuteCommandAsync();
                if (count == 1)
                    return Ok(true);
                else
                    return Ok(false);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "删除数据失败");
                return BadRequest($"删除数据失败: {ex.Message}");
            }
        }

        /*录入补给物资*/
        [HttpPost]
        public async Task<IActionResult> supply_entry([FromBody] Supply record)
        {//物资id 物资点id 数量

            _logger.LogInformation("收到补给录入数据");

            try
            {
                await _db.Insertable(record).ExecuteCommandAsync();
                _logger.LogInformation("成功插入补给录入数据");
                return Ok(true);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "插入数据失败: {@Supply}", record); // 记录错误信息

                return BadRequest(false);
            }
        }

        /*录入参赛包物资*/
        [HttpPost]
        public async Task<IActionResult> package_entry([FromBody] Event_Item record)
        {//物资id 赛事id 类型

            _logger.LogInformation("收到参赛包录入数据");

            try
            {
                await _db.Insertable(record).ExecuteCommandAsync();
                _logger.LogInformation("成功插入参赛包录入数据");
                return Ok(true);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "插入数据失败: {@Event_Item}", record); // 记录错误信息

                return BadRequest(false);
            }
        }


        /*添加补给点*/
        [HttpPost]
        public async Task<IActionResult> add_supplypoint([FromBody] Supplypoint supplypoint)
        {// 伤员id 补给点id

            _logger.LogInformation("收到补给点数据");

            try
            {
                await _db.Insertable(supplypoint).ExecuteCommandAsync();
                _logger.LogInformation("成功插入补给点数据");
                return Ok(true);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "插入数据失败: {@Supplypoint}", supplypoint); // 记录错误信息

                return BadRequest(false);
            }
        }

        /*删除补给点*/
        [HttpDelete]
        public async Task<IActionResult> delete_supplypoint([FromQuery] int Id)
        {
            try
            {
                int count = await _db.Deleteable<Medicalpoint>().Where(it => it.Id == Id).ExecuteCommandAsync();
                if (count == 1)
                    return Ok(true);
                else
                    return Ok(false);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "删除数据失败");
                return BadRequest($"删除数据失败: {ex.Message}");
            }
        }


        /*查询某场赛事的所有补给点*/
        [HttpGet]
        public async Task<IActionResult> get_all_supplypoint([FromQuery] int Event_Id)
        {//收到赛事id，返回对应的补给点信息
            _logger.LogInformation("收到补给点查询请求：Event_Id = {Event_Id}", Event_Id); // 记录收到的数据

            try
            {
                // 将整型数据转换为字符串后进行前缀匹配
                var query = await _db.Queryable<Supplypoint>()
                                           .Where(s => SqlFunc.ToString(s.Id).StartsWith(Event_Id.ToString()))
                                           .ToListAsync();

                var point_list = query.ToList();

                if (point_list == null || point_list.Count == 0)
                {
                    return Ok("无此补给点");
                }

                _logger.LogInformation("成功找到");
                return Ok(point_list);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "查询数据失败");
                return BadRequest($"查询失败: {ex.Message}");
            }
        }

        /*查询某场赛事所有(某种)参赛包的所有物资*/
        [HttpGet]
        public async Task<IActionResult> get_package_by_eventid([FromQuery] int event_id, string? type = null)
        {//传入赛事id 物资包类型(可空）；返回物资id name 参赛包种类

            _logger.LogInformation("收到查询物资包请求");
            try
            {
                List<Event_Item_With_Name> package_list;
                if (type == null)
                {
                    _logger.LogInformation("所有物资包");
                    package_list = await _db.Queryable<Event_Item>()
                     .InnerJoin<Item>((ei, i) => ei.Item_Id == i.Id)
                     .Where(ei => ei.Event_Id == event_id)
                     .Select((ei, i) => new Event_Item_With_Name { Id = i.Id, Name = i.Name, Kind = ei.Kind })
                     .ToListAsync();
                }
                else
                {
                    package_list = await _db.Queryable<Event_Item>()
                     .InnerJoin<Item>((ei, i) => ei.Item_Id == i.Id)
                     .Where(ei => ei.Event_Id == event_id && ei.Kind == type)
                     .Select((ei, i) => new Event_Item_With_Name { Id = i.Id, Name = i.Name, Kind = ei.Kind })
                     .ToListAsync();
                }


                if (package_list == null || package_list.Count == 0)
                {
                    return Ok(false);
                }

                _logger.LogInformation("成功找到");
                return Ok(package_list);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "查询数据失败");
                return BadRequest($"查询失败: {ex.Message}");
            }
        }

        /*查询某场赛事所有（某个）补给点所有补给*/
        [HttpGet]
        public async Task<IActionResult> get_supply_by_eventid([FromQuery] int event_id, int? supplypoint_Id = null)
        {//传入赛事id 补给点id(可空）；返回补给id name 数量

            _logger.LogInformation("收到查询补给请求");
            try
            {
                List<Supply_With_Name> package_list;
                if (supplypoint_Id == null)
                {
                    package_list = await _db.Queryable<Supply>()
                     .InnerJoin<Item>((su, i) => su.Item_Id == i.Id)
                     .Where(su => SqlFunc.ToString(su.Supplypoint_Id).StartsWith(event_id.ToString()))
                     .Select((su, i) => new Supply_With_Name { Id = i.Id, Name = i.Name, Amount = su.Amount, Supplypoint_Id = su.Supplypoint_Id })
                     .ToListAsync();
                }
                else
                {
                    package_list = await _db.Queryable<Supply>()
                     .InnerJoin<Item>((su, i) => su.Item_Id == i.Id)
                     .Where(su => su.Supplypoint_Id == supplypoint_Id)
                     .Select((su, i) => new Supply_With_Name { Id = i.Id, Name = i.Name, Amount = su.Amount, Supplypoint_Id = su.Supplypoint_Id })
                     .ToListAsync();
                }

                if (package_list == null || package_list.Count == 0)
                {
                    return Ok(false);
                }

                _logger.LogInformation("成功找到");
                return Ok(package_list);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "查询数据失败");
                return BadRequest($"查询失败: {ex.Message}");
            }
        }

        


        /*删除补给物资*/
        /*删除物资包物资*/
        /*更改补给数量*/



    }
}

