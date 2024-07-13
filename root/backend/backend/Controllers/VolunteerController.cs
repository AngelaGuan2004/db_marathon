using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using SqlSugar;
using MarathonMaster;

using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using MarathonMaster.Models;
using System.Drawing;
using Microsoft.AspNetCore.Authorization;


namespace MarathonMaster.Controllers
{

    [Route("/[Controller]/[action]")] // 指定路由模板和控制器的基本配置
    [ApiController]
    //[EnableCors("any")]
    [EnableCors("AllowAllOrigins")]  // 启用跨域请求    
    public class VolunteerController : ControllerBase
    {

        private readonly SqlSugarClient _db;
        private readonly ILogger<VolunteerController> _logger; // 声明日志记录器

        public VolunteerController(ILogger<VolunteerController> logger)
        {
            _logger = logger; // 初始化日志记录器
            /*
            _db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=system;Password=oracle;",
                DbType = DbType.Oracle,
                IsAutoCloseConnection = true,
            });*/
            //获取数据库连接
            dbORM dborm = new dbORM();
            _db = dborm.getInstance();
        }

        //志愿者报名
        [HttpPost]
        public async Task<IActionResult> add_volunteer([FromBody] Schedule Schedulei) //收到一个schedule类的值schedulei，其job_category的值为null
        {
            _logger.LogInformation("收到志愿者报名的Schedule数据: {@Schedulei}", Schedulei); // 记录收到的数据

            try
            {
                await _db.Insertable(Schedulei).ExecuteCommandAsync();
                _logger.LogInformation("成功插入志愿者报名的Schedule数据: {@Schedulei}", Schedulei); // 记录插入成功
                return Ok(true); //true表示成功
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "插入志愿者报名的Schedule数据失败: {@Schedulei}", Schedulei); // 记录错误信息

                return BadRequest(false); //false表示失败
            }
        }

        //志愿者排班-更新志愿者的job_category
        [HttpPatch]
        public async Task<IActionResult> schedule_volunteer([FromBody] Schedule Schedulei)// 收到的也是一个schedule类型的值，此时要更新他的job_category
        {
            _logger.LogInformation("收到志愿者排班的Schedule数据: {@Schedulei}", Schedulei); // 记录收到的数据

            try
            {
                //await _db.Insertable(Schedulei).ExecuteCommandAsync();
                await _db.Updateable(Schedulei).ExecuteCommandAsync();//根据主键更新
                _logger.LogInformation("成功更新志愿者排班的Schedule数据: {@Schedulei}", Schedulei); // 记录更新成功
                return Ok(true);  //true表示成功
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "更新志愿者排班的Schedule数据失败: {@Schedulei}", Schedulei); // 记录错误信息

                return BadRequest(false); //false表示失败
            }
        }

        //志愿者排班-插入VolunteerSupplypoint表
        [HttpPost]
        public async Task<IActionResult> add_volunteer_supplypoint([FromBody] VolunteerSupplypoint p)
        {
            _logger.LogInformation("收到志愿者排班的VolunteerSupplypoint：数据: {@p}", p); // 记录收到的数据

            try
            {                                
                await _db.Insertable(p).ExecuteCommandAsync();           
                _logger.LogInformation("成功更新志愿者排班的VolunteerSupplypoint数据: {@p}", p); // 记录插入成功
                return Ok(true);  //true表示成功
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "更新志愿者排班的VolunteerSupplypoint数据失败: {@p}", p); // 记录错误信息

                return BadRequest(false); //false表示失败
            }
        }

        //志愿者排班-插入VolunteerMedicalpoint表
        [HttpPost]
        public async Task<IActionResult> add_volunteer_medicalpoint([FromBody] VolunteerMedicalpoint p)
        {
            _logger.LogInformation("收到志愿者排班的VolunteerMedicalpoint：数据: {@p}", p); // 记录收到的数据

            try
            {
                await _db.Insertable(p).ExecuteCommandAsync();
                _logger.LogInformation("成功更新志愿者排班的VolunteerMedicalpoint数据: {@p}", p); // 记录插入成功
                return Ok(true);  //true表示成功
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "更新志愿者排班的VolunteerMedicalpoint数据失败: {@p}", p); // 记录错误信息
                return BadRequest(false); //false表示失败
            }
        }

        //志愿者排班-插入Drive表
        [HttpPost]
        public async Task<IActionResult> add_drive([FromBody] Drive p)
        {
            _logger.LogInformation("收到志愿者排班的Drive：数据: {@p}", p); // 记录收到的数据

            try
            {
                await _db.Insertable(p).ExecuteCommandAsync();
                _logger.LogInformation("成功更新志愿者排班的Drive数据: {@p}", p); // 记录插入成功
                return Ok(true);  //true表示成功
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "更新志愿者排班的Drive数据失败: {@p}", p); // 记录错误信息
                return BadRequest(false); //false表示失败
            }
        }
    }
}



