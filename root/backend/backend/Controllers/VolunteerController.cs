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

    [Route("/[Controller]/[action]")] // 指定路由模板和控制器的基本配置
    [ApiController]
    [EnableCors("any")]
    //[EnableCors("AllowAllOrigins")]  // 启用跨域请求    
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
        
        //志愿者报名赛事志愿
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


        //志愿者详情-查找某场赛事的某个志愿者的相关信息
        [HttpGet]
        public async Task<IActionResult> acquire_volunteer_information([FromQuery] int volunteer_id, string event_id)   //收到志愿者id和赛事id
        {
            _logger.LogInformation("收到志愿者排班的Drive：数据: {@volunteer_id}", volunteer_id); // 记录收到的数据

            Schedule p =await _db.Queryable<Schedule>().SingleAsync(it => it.Volunteer_Id == volunteer_id && it.Event_Id == event_id); //查询单条记录，没有返回Null，如果结果大于1条会抛出错误

            int status;               //志愿者状态，是否分工，查schedule表job_category的值是否为null
            string job_category ="未排班";      //该志愿者的工作种类
            bool is_scheduled =false;         //该志愿者是否排班，去对应工作的表（比如VolunteerSupplypoint）查他是否排班，即查有无该志愿者id
            List<Partner> partners = new List<Partner>();   //该志愿者工作的同伴
            //partners.Add()
            try
            {
                if (p==null || p.Job_Category == null)
                    status = 0;   //0表示未分工
                else
                {
                    status = 1;
                    job_category = p.Job_Category.ToString();   //job_category的值不是null，需要知道工作种类，等会要传回前端
                  
                    if (job_category != null)
                    {
                        //检查是否排班，即在对应的工作关系中是否存在该志愿者的id
                        if (job_category == "接驳车")                    
                            is_scheduled = await _db.Queryable<Drive>().AnyAsync(it => it.Volunteer_Id == volunteer_id);
                        else if (job_category == "补给点")
                            is_scheduled = await _db.Queryable<VolunteerSupplypoint>().AnyAsync(it => it.volunteer_id == volunteer_id);  
                        else if (job_category == "医疗点")           
                            is_scheduled = await _db.Queryable<VolunteerMedicalpoint>().AnyAsync(it => it.volunteer_id == volunteer_id);
                        
                        //如果已经排班，查询其同伴
                        if (job_category == "接驳车" && is_scheduled)
                            partners = GetPartners(volunteer_id, 0);
                        else if (job_category == "补给点" && is_scheduled)
                            partners = GetPartners(volunteer_id, 1);
                        else if (job_category == "医疗点" && is_scheduled)
                            partners = GetPartners(volunteer_id, 2);

                    }

                }
                _logger.LogInformation("成功查询志愿者相关信息: {@p}", p); // 记录插入成功
                return Ok(new { Status=status, Job_category = job_category, Is_scheduled= is_scheduled, Partners=partners });  //true表示成功
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "查询志愿者相关信息数据失败: {@p}", p); // 记录错误信息
                return BadRequest(false); //false表示失败
            }
        }

        // 志愿者详情-寻找搭档
        private List<Partner> GetPartners(int volunteer_id, int type)
        {
            // 工种：0接驳车 1补给点 2医疗点
            List<Partner> partners = new List<Partner>();
            if (type == 0)
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

        //查询赛事的志愿者
        [HttpGet]
        public async Task<IActionResult> inquiry_volunteer_by_eventid([FromQuery] string event_id) //收到一个schedule类的值schedulei，其job_category的值为null
        {
            _logger.LogInformation("收到查询赛事志愿者数据: {@event_id)}", event_id); // 记录收到的数据

            try
            {
                List<Volunteer> volunteers = await _db.Queryable<Schedule>()
                    .LeftJoin<Event>((s, e) => s.Event_Id == e.Id)
                    .LeftJoin<Volunteer>((s, e, v) => s.Volunteer_Id == v.Id)
                    .Where((s, e, v) => s.Event_Id == event_id)
                    .Select((s, e, v) => new Volunteer
                    {
                        Id = s.Volunteer_Id,
                        Telephone_Number = v.Telephone_Number,
                        Name = v.Name
                    }).ToListAsync();

                _logger.LogInformation("成功查询赛事志愿者数据: {@event_id}", event_id); // 记录插入成功
                return Ok(volunteers); //true表示成功？？
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "查询赛事志愿者数据失败: {@event_id}", event_id); // 记录错误信息

                return BadRequest(false); //false表示失败
            }
        }


        //查找志愿者报名的赛事
        [HttpGet]
        public async Task<IActionResult> acquire_volunteer_event([FromQuery] int volunteer_id)
        {
            try
            {

                List<Event> events = await _db.Queryable<Schedule>()
               .LeftJoin<Event>((s, e) => s.Event_Id == e.Id)//多个条件用&&
               .Where(s => s.Volunteer_Id == volunteer_id)
               .Select((s, e) => new Event
               {
                   Id = e.Id,
                   Category = e.Category,
                   Name = e.Name,
                   Start_Date = e.Start_Date,
                   End_Date = e.End_Date,
                   Event_Date = e.Event_Date,
                   Is_Drawn = e.Is_Drawn
               })
               .ToListAsync();

                _logger.LogInformation("查找志愿者报名的赛事成功：{@information_of_photos}", events);

                return Ok(events); //true表示成功
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "查找志愿者报名的赛事失败"); // 记录错误信息

                return BadRequest(ex); //false表示失败
            }
        }

        /*查询某场赛事所有（某个补给点）的志愿者*/
        [HttpGet]
        public async Task<IActionResult> get_supply_volunteer([FromQuery] string? Event_Id, [FromQuery] string? supplypoint_id = null)
        {
            _logger.LogInformation("收到查补给志愿者信息");

            try
            {
                List<VolunteerSupplypoint> volunteerSupplypoints;
                if (supplypoint_id == null && Event_Id == null)
                {
                    return BadRequest("未传id参数");
                }
                if (supplypoint_id != null)
                {
                    _logger.LogInformation("根据补给点id查");
                    volunteerSupplypoints = await _db.Queryable<VolunteerSupplypoint>().Where(e => e.supplypoint_id == supplypoint_id).ToListAsync();
                }
                else
                {
                    _logger.LogInformation("根据赛事id查");
                    volunteerSupplypoints = await _db.Queryable<VolunteerSupplypoint>().Where(s => s.supplypoint_id.StartsWith(Event_Id)).ToListAsync();
                }
                List<Volunteer> volunteers = new List<Volunteer>();
                if (volunteerSupplypoints == null || volunteerSupplypoints.Count == 0)
                {
                    // _logger.LogInformation("为空");
                    return Ok(volunteers);
                }
                else
                {
                    List<Player> playerlist = new List<Player>();
                    foreach (var service in volunteerSupplypoints)
                    {

                        // 根据每条结果的id找志愿者信息
                        Player player = await _db.Queryable<Player>()
                                               .Where(m => m.Id == service.volunteer_id)
                                               .FirstAsync();
                        if (player != null) playerlist.Add(player);
                    }
                    return Ok(playerlist);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "查询某场赛事所有（某个补给点）的志愿者");
                return BadRequest(ex.Message);
            }
        }

        /*查询某场赛事所有（某个医疗点）的志愿者*/
        [HttpGet]
        public async Task<IActionResult> get_medical_volunteer([FromQuery] string? Event_Id, [FromQuery] string? medicalpoint_id = null)
        {
            _logger.LogInformation("收到查补给志愿者信息");

            try
            {
                List<VolunteerMedicalpoint> volunteerMedicalpoints;
                if (medicalpoint_id == null && Event_Id == null)
                {
                    return BadRequest("未传id参数");
                }
                if (medicalpoint_id != null)
                {
                    _logger.LogInformation("根据医疗点id查");
                    volunteerMedicalpoints = await _db.Queryable<VolunteerMedicalpoint>().Where(e => e.medicalpoint_id == medicalpoint_id).ToListAsync();
                }
                else
                {
                    _logger.LogInformation("根据赛事id查");
                    volunteerMedicalpoints = await _db.Queryable<VolunteerMedicalpoint>().Where(s => s.medicalpoint_id.StartsWith(Event_Id)).ToListAsync();
                }
                List<Volunteer> volunteers = new List<Volunteer>();
                if (volunteerMedicalpoints == null || volunteerMedicalpoints.Count == 0)
                {
                    // _logger.LogInformation("为空");
                    return Ok(volunteers);
                }
                else
                {
                    List<Player> playerlist = new List<Player>();
                    foreach (var service in volunteerMedicalpoints)
                    {

                        // 根据每条结果的id找志愿者信息
                        Player player = await _db.Queryable<Player>()
                                               .Where(m => m.Id == service.volunteer_id)
                                               .FirstAsync();
                        if (player != null) playerlist.Add(player);
                    }
                    return Ok(playerlist);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "某场赛事所有（某个医疗点）的志愿者失败");
                return BadRequest(ex.Message);
            }
        }
        /*查询某场赛事所有（某个接驳车）的志愿者*/
        [HttpGet]
        public async Task<IActionResult> get_shuttlecar_volunteer([FromQuery] string? Event_Id, [FromQuery] int? shuttlecar_id = null)
        {
            _logger.LogInformation("收到查接驳车志愿者信息");

            try
            {
                List<Drive> drives;
                if (shuttlecar_id == null && Event_Id == null)
                {
                    return BadRequest("未传id参数");
                }
                if (shuttlecar_id != null)
                {
                    _logger.LogInformation("根据接驳车id查");
                    drives = await _db.Queryable<Drive>().Where(e => e.Shuttlecar_Id == shuttlecar_id).ToListAsync();
                }
                else
                {
                    _logger.LogInformation("根据赛事id查接驳车");
                    drives = await _db.Queryable<Drive>()
                        .LeftJoin<Volunteer>((d,v)=>d.Volunteer_Id == v.Id)
                        .LeftJoin<Shuttlecar>((d, v,s) => d.Shuttlecar_Id  == s.Id && s.Event_Id == Event_Id)                      
                        .Select((d, v, s) => new Drive
                        {
                            Volunteer_Id =d.Volunteer_Id,
                            Shuttlecar_Id = d.Shuttlecar_Id
                        })
                        .ToListAsync();
                }
                List<Volunteer> volunteers = new List<Volunteer>();
                if (drives == null || drives.Count == 0)
                {
                    // _logger.LogInformation("为空");
                    return Ok(volunteers);
                }
                else
                {
                    List<Player> playerlist = new List<Player>();
                    foreach (var service in drives)
                    {

                        // 根据每条结果的id找志愿者信息
                        Player player = await _db.Queryable<Player>()
                                               .Where(m => m.Id == service.Volunteer_Id)
                                               .FirstAsync();
                        if (player != null) playerlist.Add(player);
                    }
                    return Ok(playerlist);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "查询某场赛事所有（某个接驳车）的志愿者失败");
                return BadRequest(ex.Message);
            }
        }
    }


}



