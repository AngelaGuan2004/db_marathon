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
    [Route("/[controller]/[action]")]
    [ApiController]
    [EnableCors("any")]
    public class WeatherController : ControllerBase
    {

        private readonly SqlSugarClient _db;
        private readonly ILogger<WeatherController> _logger;

        public WeatherController(ILogger<WeatherController> logger)
        {
            _logger = logger;
            dbORM dborm = new dbORM();
            _db = dborm.getInstance();
        }


        /*查询某场赛事的天气*/
        [HttpGet]
        public async Task<IActionResult> get_weather([FromQuery] string event_id)
        {
            // _logger.LogInformation("收到查天气关键字信息: {@event_id}", event_id); // 记录收到的数据
            List<Weather> weathers = new List<Weather>();

            try
            {
                weathers = await _db.Queryable<Weather>().Where(p => p.Id == event_id).ToListAsync();

                // _logger.LogInformation("查找天气信息成功: {@event_id}", event_id); 
                return Ok(weathers); 
            }
            catch (System.Exception ex)
            {
                // _logger.LogError(ex.Message, "查找天气信息失败:"); // 记录错误信息
                

                return BadRequest(weathers); //false表示失败
            }
        }

        /*插入或更改某场赛事的天气*/
        [HttpPost]
        public async Task<IActionResult> upsert_weather([FromBody] Weather weather)
        {
            // _logger.LogInformation("收到改天气关键字信息: {@weather}", weather); // 记录收到的数据
            

            try
            {
                var storage = _db.Storageable(weather).ToStorage();
                var insertCount = await storage.AsInsertable.ExecuteCommandAsync();//不存在插入
                var updateCount = await storage.AsUpdateable.ExecuteCommandAsync();//存在更新
                if (insertCount > 0)
                {
                    // _logger.LogInformation("成功插入: {@weather}", weather);
                    return Ok(new { status = true, type = "Inserted" });
                }
                else if (updateCount > 0)
                {
                    // _logger.LogInformation("成功更新: {@weather}", weather);
                    return Ok(new { status = true, type = "Updated" });
                }
                else
                {
                    // _logger.LogInformation("无变更操作: {@weather}", weather);
                    return Ok(new { status = false, type = "NoChange", message = "后端不报错但无插入" });
                }
            }
            catch (System.Exception ex)
            {
                // _logger.LogError(ex.Message, "更改天气信息失败:"); // 记录错误信息


                return BadRequest(new { status = false }); //false表示失败
            }
        }
    }
}

