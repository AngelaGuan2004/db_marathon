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
    public class MedicalController : ControllerBase
    {
        private readonly SqlSugarClient _db;
        private readonly ILogger<PlayerController> _logger; 

        public MedicalController(ILogger<PlayerController> logger)
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
                return Ok(true);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "插入数据失败: {@Medical_Service}", service); // 记录错误信息

                return BadRequest(false);
            }
        }

        /*查询某场赛事的所有医疗点*/
        [HttpGet]
        public async Task<IActionResult> get_all_medicalpoint([FromQuery] int Event_Id)
        {//收到赛事id，返回对应的医疗点信息
            _logger.LogInformation("收到医疗点查询请求：Event_Id = {Event_Id}", Event_Id); // 记录收到的数据

            try
            {

                // 将整型数据转换为字符串后进行前缀匹配
                var query = await _db.Queryable<Medicalpoint>()
                                           .Where(s => SqlFunc.ToString(s.Id).StartsWith(Event_Id.ToString()))
                                           .ToListAsync();

                var medical_list = query.ToList();

                if (medical_list == null || medical_list.Count == 0)
                {
                    return Ok("无此医疗点");
                }

                _logger.LogInformation("成功找到");
                return Ok(medical_list);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "查询数据失败");
                return BadRequest($"查询失败: {ex.Message}");
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
                return Ok(true);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "插入数据失败: {@Medicalpoint}", medicalpoint); // 记录错误信息

                return BadRequest(false);
            }
        }

        /*删除医疗点*/
        [HttpDelete]
        public async Task<IActionResult> delete_medicalpoint([FromQuery] int Id)
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





    }









}
