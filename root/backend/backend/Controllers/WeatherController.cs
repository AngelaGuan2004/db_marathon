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
        /*更改某场赛事的天气*/
        /*插入天气数据*/
    }
}

