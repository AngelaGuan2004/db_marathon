using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging; 
using MarathonMaster;
using Microsoft.AspNetCore.Cors;
using MarathonMaster.Models;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;

namespace MarathonMaster.Controllers
{
    [Route("/[controller]/[action]")]
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
        public async Task<IActionResult> add_injury([FromBody] Service service)
        {// 伤员id 医疗点id

            _logger.LogInformation("收到伤员数据");

            Medical_Service medical_Service = new Medical_Service();

            try
            {
                var existingPlayer = await _db.Queryable<Player>()
                    .Where(it => it.Name == service.name && it.Id_Number == service.IdNumber)
                    .FirstAsync();

                if (existingPlayer != null)
                {
                    medical_Service.Player_Id = existingPlayer.Id;
                }
                else
                {
                    _logger.LogWarning("查id失败: 无效的用户名或身份证号");
                    return BadRequest(new { status = false, message = "插入数据失败:无效的用户名或身份证号" });
                }
                medical_Service.Medicalpoint_Id = service.medicalPoint_Id;
                await _db.Insertable(medical_Service).ExecuteCommandAsync();
                _logger.LogInformation("成功插入伤员数据");
                return Ok(new { status = true });
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "插入数据失败: {@Medical_Service}", medical_Service); // 记录错误信息

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
        /*删除伤员*/
        [HttpGet]
        public async Task<IActionResult> delete_injury([FromQuery] string IdNumber, [FromQuery] string medicalPoint_Id)
        {
            _logger.LogInformation("收到删伤员信息");
            try
            {
                // 检查是否存在身份证号对应的选手
                var existingPlayer = await _db.Queryable<Player>()
                                              .Where(it => it.Id_Number == IdNumber)
                                              .FirstAsync();

                if (existingPlayer == null)
                {
                    _logger.LogWarning("无此选手"); 
                    return BadRequest("无此选手");
                }
                int count = await _db.Deleteable<Medical_Service>().Where(it => it.Medicalpoint_Id == medicalPoint_Id && it.Player_Id == existingPlayer.Id).ExecuteCommandAsync();
                if (count == 1)
                    return Ok(new { status = true });
                else
                    return Ok(new { status = false, message = "后端不报错，但没有删除任何行" });
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "删除数据失败");
                return BadRequest(new { status = false, message = $"删除数据失败: {ex.Message}" });
            }

        }

        /*查询某场赛事所有（某个医疗点）的伤员*/
        [HttpGet]
        public async Task<IActionResult> get_injury([FromQuery] string? Event_Id, [FromQuery] string? medical_id = null)
        {
            _logger.LogInformation("收到查伤员信息");

            try
            {
                List<Medical_Service> medical_Services;
                if(medical_id == null && Event_Id == null)
                {
                    return BadRequest("未传id参数");
                }
                if (medical_id != null)
                {
                    _logger.LogInformation("根据医疗点id查");
                    medical_Services = await _db.Queryable<Medical_Service>().Where(e => e.Medicalpoint_Id == medical_id).ToListAsync();
                }
                else
                {
                    _logger.LogInformation("根据赛事id查");
                    medical_Services = await _db.Queryable<Medical_Service>().Where(s => s.Medicalpoint_Id.StartsWith(Event_Id)).ToListAsync();
                }
                List<Injury> injuryList = new List<Injury>();
                if (medical_Services == null || medical_Services.Count == 0)
                {
                    // _logger.LogInformation("为空");
                    return Ok(injuryList);
                }
                else
                {
                    foreach (var service in medical_Services)
                    {
                        // 通过 medical_id 查询对应的 medical 记录
                        Medicalpoint medical_point = await _db.Queryable<Medicalpoint>()
                                               .Where(m => m.Id == service.Medicalpoint_Id)
                                               .FirstAsync();
                        // 根据每条结果的伤员id找伤员信息
                        Player player = await _db.Queryable<Player>()
                                               .Where(m => m.Id == service.Player_Id)
                                               .FirstAsync();
                        // 确保查询到了 player 和 medical_point
                        if (medical_point != null && player != null)
                        {
                            Injury injury_item = new Injury
                            {
                                injury = player,
                                medicalpoint = medical_point
                            };

                            // 添加到 injuryList
                            injuryList.Add(injury_item);
                        }
                        else
                        {
                            return BadRequest("查询到的 medical_point 或 player 为空");
                        }
                    }
                    return Ok(injuryList);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "查询伤员数据失败");
                return BadRequest(ex.Message);
            }
        }


    }









}
