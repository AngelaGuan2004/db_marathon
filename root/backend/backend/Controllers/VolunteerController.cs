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
using Microsoft.Extensions.Logging;


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

        // 志愿者详情-寻找搭档
        private List<Partner> GetPartners(int volunteer_id, int type)
        {
            // 工种：0接驳车 1补给点 2医疗点
            List<Partner> partners = new List<Partner>();
            if(type == 0)
            {
                try
                {
                    // 根据该志愿者id找对应的班次id，再根据对应的班次id找对应的志愿者
                    var shuttle_ids = _db.Queryable<Drive>()
                           .Where(it => it.Volunteer_Id == volunteer_id)
                           .Select(it => it.Shuttlecar_Id)
                           .ToList();
                    var volunteerIds = _db.Queryable<Drive>()
                          .Where(it => shuttle_ids.Contains(it.Shuttlecar_Id))
                          .Select(it => it.Volunteer_Id)
                          .ToList();
                    partners = _db.Queryable<Volunteer>()
                           .Where(it => volunteerIds.Contains(it.Id))
                           .Select(it => new Partner { Id = it.Id, Name = it.Name, Telephone_Number = it.Telephone_Number })
                           .ToList();
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex, "接驳车搭档查找失败"); // 记录错误信息
                    return partners;
                }
            }
            else if (type == 1)
            {
                try
                {
                    // 根据该志愿者id找对应的补给点id，再根据对应的补给点id找对应的志愿者
                    var ids = _db.Queryable<VolunteerSupplypoint>()
                           .Where(it => it.volunteer_id == volunteer_id)
                           .Select(it => it.supplypoint_id)
                           .ToList();
                    var volunteerIds = _db.Queryable<VolunteerSupplypoint>()
                          .Where(it => ids.Contains(it.supplypoint_id))
                          .Select(it => it.volunteer_id)
                          .ToList();
                    partners = _db.Queryable<Volunteer>()
                           .Where(it => volunteerIds.Contains(it.Id))
                           .Select(it => new Partner { Id = it.Id, Name = it.Name, Telephone_Number = it.Telephone_Number })
                           .ToList();
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex, "补给点搭档查找失败"); // 记录错误信息
                    return partners;
                }
            }
            else if (type == 2)
            {
                try
                {
                    // 根据该志愿者id找对应的医疗点id，再根据对应的医疗点id找对应的志愿者
                    var ids = _db.Queryable<VolunteerMedicalpoint>()
                           .Where(it => it.volunteer_id == volunteer_id)
                           .Select(it => it.medicalpoint_id)
                           .ToList();
                    var volunteerIds = _db.Queryable<VolunteerMedicalpoint>()
                          .Where(it => ids.Contains(it.medicalpoint_id))
                          .Select(it => it.volunteer_id)
                          .ToList();
                    partners = _db.Queryable<Volunteer>()
                           .Where(it => volunteerIds.Contains(it.Id))
                           .Select(it => new Partner { Id = it.Id, Name = it.Name, Telephone_Number = it.Telephone_Number })
                           .ToList();
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex, "医疗点搭档查找失败"); // 记录错误信息
                    return partners;
                }
            }
            return partners;
        }
    }
}



