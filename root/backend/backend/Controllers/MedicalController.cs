using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging; 
using backend;
using Microsoft.AspNetCore.Cors;
using db_marathon.Models;
using System.Numerics;

namespace MarathonMaster.Controllers
{
    [Route("/[controller]/[action]")]
    [ApiController]
    [EnableCors("any")]
    public class MedicalController : ControllerBase
    {
        private readonly SqlSugarClient _db;
        private readonly ILogger<PlayerController> _logger; // 声明日志记录器

        public MedicalController(ILogger<PlayerController> logger)
        {
            _logger = logger;
            dbORM dborm = new dbORM();
            _db = dborm.getInstance();
        }
        [HttpPost]
        public async Task<IActionResult> add_injury([FromBody] Medical_Service service)
        {// 伤员id 医疗点id
            
            _logger.LogInformation("收到伤员数据");

            try
            {
                await _db.Insertable(service).ExecuteCommandAsync();
                _logger.LogInformation("成功插入伤员数据"); 
                return Ok(true);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "插入数据失败: {@Medical_Service}", service); // 记录错误信息

                return BadRequest(false);
            }
        }
    }
    
}
