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
    public class StudentsController : ControllerBase
    {

        private readonly SqlSugarClient _db;
        private readonly ILogger<StudentsController> _logger; // 声明日志记录器

        public StudentsController(ILogger<StudentsController> logger)
        {
            _logger = logger; // 初始化日志记录器
            dbORM dborm = new dbORM();
            _db = dborm.getInstance();
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] Student student)
        {
            _logger.LogInformation("收到学生数据: {@Student}", student); // 记录收到的数据

            try
            {
                await _db.Insertable(student).ExecuteCommandAsync();
                _logger.LogInformation("成功插入学生数据: {@Student}", student); // 记录插入成功
                return Ok("插入成功");
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "插入学生数据失败: {@Student}", student); // 记录错误信息

                return BadRequest($"插入失败: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetStudent([FromQuery] string Id)
        {
            _logger.LogInformation("收到查询请求：ID = {Id}", Id); // 记录收到的数据

            try
            {
                var query = _db.Queryable<Student>().Where(e => e.Id == Id);

                var stu = await query.ToListAsync();

                if (stu == null || stu.Count == 0)
                {
                    return Ok("未找到学生");
                }

                _logger.LogInformation("成功找到学生:{stu[0].name}", stu[0].Name);
                return Ok(stu[0]);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "查询学生数据失败");
                return BadRequest($"查询失败: {ex.Message}");
            }
        }
    }

}


