using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MarathonMaster;
using Microsoft.AspNetCore.Cors;
using MarathonMaster.Models;
using System.Numerics;
using MarathonMaster.Controllers;
using System.Collections.Generic;
using System;

namespace MarathonMaster.Controllers
{
    [Route("/[controller]/[action]")]
    [ApiController]
    [EnableCors("any")]
    public class SupplyController : ControllerBase
    {
        private readonly SqlSugarClient _db;
        private readonly ILogger<SupplyController> _logger;

        public SupplyController(ILogger<SupplyController> logger)
        {
            _logger = logger;
            dbORM dborm = new dbORM();
            _db = dborm.getInstance();
        }

        /*添加某件物品或修改物品名称*/
        [HttpPost]
        public async Task<IActionResult> upsert_item([FromBody] Item item)
        {//只用id 名称

            _logger.LogInformation("收到物品数据");

            try
            {
                var storage = _db.Storageable(item).ToStorage();
                var insertCount = await storage.AsInsertable.ExecuteCommandAsync();//不存在插入
                var updateCount = await storage.AsUpdateable.ExecuteCommandAsync();//存在更新
                if (insertCount > 0)
                {
                    _logger.LogInformation("成功插入: {@Item}", item);
                    return Ok(new { status = true, type = "Inserted" });
                }
                else if (updateCount > 0)
                {
                    _logger.LogInformation("成功更新: {@Item}", item);
                    return Ok(new { status = true, type = "Updated" });
                }
                else
                {
                    _logger.LogInformation("无变更操作: {@Item}", item);
                    return Ok(new { status = false, type = "NoChange", message = "后端不报错但无插入" });
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "插入数据失败: {@Item}", item); // 记录错误信息

                return Ok(new { status = false, type = "NoChange", message = $"插入数据失败: {ex.Message}" });
            }
        }


        /*删除某件物品*/
        [HttpDelete]
        public async Task<IActionResult> delete_item_by_id([FromQuery] int Id)
        {
            try
            {
                int count = await _db.Deleteable<Item>().Where(it => it.Id == Id).ExecuteCommandAsync();
                if (count == 1)
                    return Ok(new { status = true });
                else
                    return Ok(new { status = false, message = "不报错但无更改" });
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "删除数据失败");
                return Ok(new { status = false, message = $"删除数据失败: {ex.Message}" });
            }
        }

        /*单条录入或修改补给物资*/
        [HttpPost]
        public async Task<IActionResult> supply_entry([FromBody] Supply record)
        {//物资id 物资点id 数量

            _logger.LogInformation("收到补给录入数据");

            try
            {
                var storage = _db.Storageable(record).ToStorage();
                var insertCount = await storage.AsInsertable.ExecuteCommandAsync();//不存在插入
                var updateCount = await storage.AsUpdateable.ExecuteCommandAsync();//存在更新
                if (insertCount > 0)
                {
                    _logger.LogInformation("成功插入: {@Supply}", record);
                    return Ok(new { status = true, type = "Inserted" });
                }
                else if (updateCount > 0)
                {
                    _logger.LogInformation("成功更新: {@Supply}", record);
                    return Ok(new { status = true, type = "Updated" });
                }
                else
                {
                    _logger.LogInformation("无变更操作: {@Supply}", record);
                    return Ok(new { status = false, type = "NoChange", message = "后端不报错但无插入" });
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "插入数据失败: {@Supply}", record); // 记录错误信息

                return Ok(new { status = false, type = "NoChange", message = $"插入数据失败: {ex.Message}" });
            }
        }

        /*单条录入或修改参赛包物资*/
        [HttpPost]
        public async Task<IActionResult> package_entry([FromBody] Event_Item record)
        {//物资id 赛事id 类型

            _logger.LogInformation("收到参赛包录入数据");

            try
            {
                var storage = _db.Storageable(record).ToStorage();
                var insertCount = await storage.AsInsertable.ExecuteCommandAsync();//不存在插入
                var updateCount = await storage.AsUpdateable.ExecuteCommandAsync();//存在更新
                if (insertCount > 0)
                {
                    _logger.LogInformation("成功插入: {@Event_Item}", record);
                    return Ok(new { status = true, type = "Inserted" });
                }
                else if (updateCount > 0)
                {
                    _logger.LogInformation("成功更新: {@Event_Item}", record);
                    return Ok(new { status = true, type = "Updated" });
                }
                else
                {
                    _logger.LogInformation("无变更操作: {@Event_Item}", record);
                    return Ok(new { status = false, type = "NoChange", message = "后端不报错但无插入" });
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "插入数据失败: {@Event_Item}", record); // 记录错误信息

                return Ok(new { status = false, type = "NoChange", message = $"插入数据失败: {ex.Message}" });
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
                return Ok(new { status = true });
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "插入数据失败: {@Supplypoint}", supplypoint); // 记录错误信息

                return Ok(new { status = false, message = $"插入数据失败: {ex.Message}" });
            }
        }

        /*删除补给点*/
        [HttpDelete]
        public async Task<IActionResult> delete_supplypoint_by_id([FromQuery] string Id)
        {
            try
            {
                int count = await _db.Deleteable<Medicalpoint>().Where(it => it.Id == Id).ExecuteCommandAsync();
                if (count == 1)
                    return Ok(new { status = true });
                else
                    return Ok(new { status = false });
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "删除数据失败");
                return BadRequest($"删除数据失败: {ex.Message}");
            }
        }


        /*查询某场赛事的所有补给点*/
        [HttpGet]
        public async Task<IActionResult> get_all_supplypoint([FromQuery] string Event_Id)
        {//收到赛事id，返回对应的补给点信息
            _logger.LogInformation("收到补给点查询请求：Event_Id = {Event_Id}", Event_Id); // 记录收到的数据

            try
            {
                // 进行前缀匹配
                var query = await _db.Queryable<Supplypoint>()
                                           .Where(s => s.Id.StartsWith(Event_Id))
                                           .ToListAsync();

                var point_list = query.ToList();


                _logger.LogInformation("成功找到或返回空列表");
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
        public async Task<IActionResult> get_package_by_eventid([FromQuery] string event_id, string? type = null)
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

                /*
                if (package_list == null || package_list.Count == 0)
                {
                    return Ok(false);
                }
                */
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
        public async Task<IActionResult> get_supply_by_eventid([FromQuery] string event_id, string? supplypoint_Id = null)
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
                /*
                if (package_list == null || package_list.Count == 0)
                {
                    return Ok(false);
                }
                */
                _logger.LogInformation("成功找到");
                return Ok(package_list);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "查询数据失败");
                return BadRequest($"查询失败: {ex.Message}");
            }
        }

        /*批量录入补给物资*/
        [HttpPost]
        public async Task<IActionResult> supply_entry_multi([FromBody] List<Supply> records)
        {//物资id 物资点id 数量

            _logger.LogInformation("收到补给批量录入数据");

            try
            {
                // 启动事务
                _db.Ado.BeginTran();

                // 执行批量插入操作
                await _db.Insertable(records).ExecuteCommandAsync();

                // 提交事务
                _db.Ado.CommitTran();

                _logger.LogInformation("成功批量插入数据: {@Records}", records);
                return Ok(new { status = true });
            }
            catch (System.Exception ex)
            {
                // 回滚事务
                _db.Ado.RollbackTran();

                _logger.LogError(ex, "批量插入数据失败，全部回滚: {@Records}", records);
                return Ok(new { status = false, message = $"插入失败: {ex.Message}" });
            }
        }

        /*批量录入物资包物资*/
        [HttpPost]
        public async Task<IActionResult> package_entry_multi([FromBody] List<Event_Item> records)
        {//物资id 赛事id 类型

            _logger.LogInformation("收到物资包批量录入数据");

            try
            {
                // 启动事务
                _db.Ado.BeginTran();

                // 执行批量插入操作
                await _db.Insertable(records).ExecuteCommandAsync();

                // 提交事务
                _db.Ado.CommitTran();

                _logger.LogInformation("成功批量插入数据: {@Records}", records);
                return Ok(new { status = true });
            }
            catch (System.Exception ex)
            {
                // 回滚事务
                _db.Ado.RollbackTran();

                _logger.LogError(ex, "批量插入数据失败，全部回滚: {@Records}", records);
                return Ok(new { status = false, message = $"插入失败: {ex.Message}" });
            }
        }

        /*批量删除补给物资*/
        [HttpDelete]
        public async Task<IActionResult> supply_delete_multi([FromBody] List<Supply> records)
        {//物资id 物资点id 数量

            _logger.LogInformation("收到补给批量删除数据");

            try
            {
                // 启动事务
                _db.Ado.BeginTran();

                // 执行批量删除操作
                await _db.Deleteable(records).ExecuteCommandAsync();

                // 提交事务
                _db.Ado.CommitTran();

                _logger.LogInformation("成功批量删除数据");
                return Ok(new { status = true });
            }
            catch (System.Exception ex)
            {
                // 回滚事务
                _db.Ado.RollbackTran();

                _logger.LogError(ex, "批量删除数据失败，全部回滚");
                return Ok(new { status = false, message = $"删除失败: {ex.Message}" });
            }
        }

        /*批量删除物资包物资*/
        [HttpDelete]
        public async Task<IActionResult> package_delete_multi([FromBody] List<Event_Item> records)
        {//物资id 赛事id 类型

            _logger.LogInformation("收到物资包批量删除数据");

            try
            {
                // 启动事务
                _db.Ado.BeginTran();

                // 执行批量删除操作
                await _db.Deleteable(records).ExecuteCommandAsync();

                // 提交事务
                _db.Ado.CommitTran();

                _logger.LogInformation("成功批量删除数据: {@List<Event_Item>}", records);
                return Ok(new { status = true });
            }
            catch (System.Exception ex)
            {
                // 回滚事务
                _db.Ado.RollbackTran();

                _logger.LogError(ex, "批量删除数据失败，全部回滚: {@List<Event_Item>}", records);
                return Ok(new { status = false, message = $"删除失败: {ex.Message}" });
            }
        }

        /*查询平台所有（指定个数）的物品*/
        [HttpGet]
        public async Task<IActionResult> get_item([FromQuery] int? amount = null)
        {//传入需要返回的物品条数（若空返回全部），返回物品list和条数

            _logger.LogInformation("收到返回物品请求");
            try
            {
                List<Item> item_list;
                int all_count = _db.Queryable<Item>().Count();//平台总物品数
                if (amount != null)
                {
                    item_list = await _db.Queryable<Item>().Take((int)amount).ToListAsync();
                }
                else
                {
                    item_list = await _db.Queryable<Item>().ToListAsync();
                }

                if (item_list == null || item_list.Count == 0)
                {
                    return Ok(new { data = item_list, status = false, total = 0 });//平台无物品
                }

                _logger.LogInformation("成功找到");
                return Ok(new { data = item_list, status = true, total = all_count });//total是平台已有的所有物品
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "查询数据失败");
                List<Item> null_list = [];
                return Ok(new { data = null_list, status = false, total = 0, message = $"查询失败: {ex.Message}" });
            }
        }


    }
}

