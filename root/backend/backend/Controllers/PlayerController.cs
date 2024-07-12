using Microsoft.AspNetCore.Mvc;
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
    public class PlayerController : ControllerBase
    {
        private readonly SqlSugarClient _db;
        private readonly ILogger<PlayerController> _logger;

        public PlayerController(ILogger<PlayerController> logger)
        {
            _logger = logger;
            dbORM dborm = new dbORM();
            _db = dborm.getInstance();
        }
        [HttpPost]//添加Player（注册需要手动输入Name、Id_Number、Password、Gender平台分配id）
        public async Task<IActionResult> add_player([FromBody] Player player)
        {
            
            _logger.LogInformation("收到数据: {@Player}", player); // 记录收到的数据

            try
            {
                await _db.Insertable(player).ExecuteCommandAsync();
                _logger.LogInformation("成功插入数据: {@Player}", player); // 记录插入成功
                return Ok(true);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "插入数据失败: {@Player}", player); // 记录错误信息

                return BadRequest($"插入失败: {ex.Message}");
            }
        }
    }
    
}
