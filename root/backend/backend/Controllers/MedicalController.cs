﻿using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging; 
using MarathonMaster;
using Microsoft.AspNetCore.Cors;
using MarathonMaster.Models;
using System.Numerics;

namespace MarathonMaster.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("any")]
    public class MedicalController : ControllerBase
    {
        private readonly SqlSugarClient _db;
        private readonly ILogger<MedicalController> _logger; 

        public MedicalController(ILogger<MedicalController> logger)
        {
            _logger = logger;
            dbORM dborm = new dbORM();
            _db = dborm.getInstance();
        }

        /*录入伤员数据*/
        [HttpPost]
        public async Task<IActionResult> add_injury([FromBody] Medical_Service service)
        {// 伤员id 医疗点id

            _logger.LogInformation("收到伤员数据");

            try
            {
                await _db.Insertable(service).ExecuteCommandAsync();
                _logger.LogInformation("成功插入伤员数据");
                return Ok(new { status = true });
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "插入数据失败: {@Medical_Service}", service); // 记录错误信息

                return Ok(new { status = false, message = $"插入数据失败: {ex.Message}" });
            }
        }

        /*查询某场赛事的所有医疗点*/
        [HttpGet]
        public async Task<IActionResult> get_all_medicalpoint([FromQuery] string Event_Id)
        {//收到赛事id，返回对应的医疗点信息
            _logger.LogInformation("收到医疗点查询请求：Event_Id = {Event_Id}", Event_Id); // 记录收到的数据

            try
            {

                // 直接进行前缀匹配
                var query = await _db.Queryable<Medicalpoint>()
                                           .Where(s => s.Id.StartsWith(Event_Id))
                                           .ToListAsync();

                var medical_list = query.ToList();


                _logger.LogInformation("成功找到");//包含空的情况
                return Ok(new { data = medical_list, status = true});
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "查询数据失败");
                List<Medicalpoint> null_list = [];
                return BadRequest(new { data = null_list, status = false, message = $"查询失败: {ex.Message}" });
            }
        }

        /*添加医疗点*/
        [HttpPost]
        public async Task<IActionResult> add_medicalpoint([FromBody] Medicalpoint medicalpoint)
        {// 伤员id 医疗点id

            _logger.LogInformation("收到医疗点数据");

            try
            {
                await _db.Insertable(medicalpoint).ExecuteCommandAsync();
                _logger.LogInformation("成功插入医疗点数据");
                return Ok(new { status = true });
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "插入数据失败: {@Medicalpoint}", medicalpoint); // 记录错误信息

                return Ok(new { status = false, message = $"插入数据失败: {ex.Message}" });
            }
        }

        /*删除医疗点*/
        [HttpDelete]
        public async Task<IActionResult> delete_medicalpoint([FromQuery] string Id)
        {
            try
            {
                int count = await _db.Deleteable<Medicalpoint>().Where(it => it.Id == Id).ExecuteCommandAsync();
                if (count == 1)
                    return Ok(new { status = true });
                else
                    return Ok(new { status = false, message = "后端不报错，但没有删除任何行" });
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "删除数据失败");
                return Ok(new {status = false,message= $"删除数据失败: {ex.Message}" });
            }
        }





    }









}
