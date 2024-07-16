using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using SqlSugar;
<<<<<<< HEAD
using db_marathon.Models;
=======
using MarathonMaster.Models;
>>>>>>> b8150caa4459344159602aeb46e26741665373c1
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Security.Cryptography;
using Microsoft.Extensions.Logging; // 引入日志记录命名空间
using System.Numerics;


<<<<<<< HEAD
namespace db_marathon.Controllers
=======
namespace MarathonMaster.Controllers
>>>>>>> b8150caa4459344159602aeb46e26741665373c1
{
    [Route("/[controller]/[action]")]
    [ApiController]
    [EnableCors("any")]
    public class AuthController : ControllerBase
    {
        private readonly SqlSugarClient _db;
        private readonly ILogger<AuthController> _logger; // 声明日志记录器

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger; // 初始化日志记录器
            //获取数据库连接
            dbORM dborm = new dbORM();
            _db = dborm.getInstance();
        }

        //注册Player
        [HttpPost]
        public async Task<IActionResult> add_player([FromBody] Player player)
        {
            _logger.LogInformation("收到选手数据: {@Player}", player); // 记录收到的数据

            try
            {
                await _db.Insertable(player).ExecuteCommandAsync();
                _logger.LogInformation("成功插入选手数据: {@Player}", player); // 记录插入成功
                return Ok(true);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "插入选手数据失败: {@Player}", player); // 记录错误信息

                return BadRequest(false);
            }

        }

        //注册volunteer
        [HttpPost]
        public async Task<IActionResult> add_volunteer([FromBody] Volunteer volunteer)
        {
            _logger.LogInformation("收到志愿者数据: {@Volunteer}", volunteer); // 记录收到的数据

            try
            {
                await _db.Insertable(volunteer).ExecuteCommandAsync();
                _logger.LogInformation("成功插入志愿者数据: {@Volunteer}", volunteer); // 记录插入成功
                return Ok(true);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "插入志愿者数据失败: {@Volunteer}", volunteer); // 记录错误信息

                return BadRequest(false);
            }

        }


        //选手登录
        [HttpGet]
        public async Task<IActionResult> login_player([FromBody] Player player)
        {
            _logger.LogInformation("收到选手数据: {@Player}", player); // 记录收到的数据

            try
            {
                var existingPlayer = await _db.Queryable<Player>()
                    .Where(it => it.Name == player.Name && it.Password == player.Password && it.Id_Number == player.Id_Number)
                    .FirstAsync();

                if (existingPlayer != null)
                {
                    _logger.LogInformation("选手登录成功: {@Player}", player); // 记录登录成功
                    return Ok(true);
                }
                else
                {
                    _logger.LogWarning("登录失败: 无效的用户名或密码或身份证号");
                    return Unauthorized(false);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "选手登登录录失败: {@LoginRequest}", player); // 记录错误信息

                return BadRequest(false);
            }
        }

        //志愿者登录
        [HttpGet]
        public async Task<IActionResult> login_volunteer([FromBody] Volunteer volunteer)
        {
            _logger.LogInformation("收到志愿者数据: {@Volunteer}", volunteer); // 记录收到的数据

            try
            {
                var existingVolunteer = await _db.Queryable<Volunteer>()
                    .Where(it => it.Name == volunteer.Name && it.Password == volunteer.Password && it.Telephone_Number == volunteer.Telephone_Number)
                    .FirstAsync();

                if (existingVolunteer != null)
                {
                    _logger.LogInformation("志愿者登录成功: {@Volunteer}", volunteer); // 记录登录成功
                    return Ok(true);
                }
                else
                {
                    _logger.LogWarning("登录失败: 无效的用户名或密码或手机号");
                    return Unauthorized(false);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "志愿者登录失败: {@LoginRequest}", volunteer); // 记录错误信息

                return BadRequest(false);
            }
        }

        //摄影师登录
        [HttpGet]
        public async Task<IActionResult> login_photographer([FromBody] Photographer photographer)
        {
            _logger.LogInformation("收到摄影师数据: {@Photographer}", photographer); // 记录收到的数据

            try
            {
                var existingPhotographer = await _db.Queryable<Photographer>()
                    .Where(it => it.Name == photographer.Name && it.Password == photographer.Password)
                    .FirstAsync();

                if (existingPhotographer != null)
                {
                    _logger.LogInformation("摄影师登录成功: {@Volunteer}", photographer); // 记录登录成功
                    return Ok(true);
                }
                else
                {
                    _logger.LogWarning("登录失败: 无效的用户名或密码");
                    return Unauthorized(false);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "志愿者登录失败: {@LoginRequest}", photographer); // 记录错误信息

                return BadRequest(false);
            }
        }

        //选手修改信息
        [HttpPatch]
        public async Task<IActionResult> update_player([FromBody] Player player)
        {
            _logger.LogInformation("收到选手数据: {@Player}", player); // 记录收到的数据

            try
            {
                await _db.Updateable(player).ExecuteCommandAsync();
                _logger.LogInformation("成功更改选手数据: {@Player}", player); // 记录插入成功
                return Ok(true);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "更改选手数据失败: {@Player}", player); // 记录错误信息

                return BadRequest(false);
            }
        }

        //志愿者修改信息
        [HttpPatch]
        public async Task<IActionResult> update_volunteer([FromBody] Volunteer volunteer)
        {
            _logger.LogInformation("收到选手数据: {@Volunteer}", volunteer); // 记录收到的数据

            try
            {
                await _db.Updateable(volunteer).ExecuteCommandAsync();
                _logger.LogInformation("成功更改选手数据: {@Volunteer}", volunteer); // 记录插入成功
                return Ok(true);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "更改选手数据失败: {@Volunteer}", volunteer); // 记录错误信息

                return BadRequest(false);
            }
        }

        //摄影师修改信息
        [HttpPatch]
        public async Task<IActionResult> update_photographer([FromBody] Photographer photographer)
        {
            _logger.LogInformation("收到选手数据: {@Photographer}", photographer); // 记录收到的数据

            try
            {
                await _db.Updateable(photographer).ExecuteCommandAsync();
                _logger.LogInformation("成功更改选手数据: {@Photographer}", photographer); // 记录插入成功
                return Ok(true);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "更改选手数据失败: {@Photographer}", photographer); // 记录错误信息

                return BadRequest(false);
            }
        }

        ////查询id的选手
        //[HttpGet]
        //public string get_by_id(int Id)
        //{
        //    Player player = new Player();
        //    dbORM dborm = new dbORM();
        //    SqlSugarClient db = dborm.getInstance();//获取数据库连接

        //    player = db.Queryable<Player>().Where(it => it.Id == Id).First();
        //    if (player != null)
        //    {
        //        return JsonSerializer.Serialize(player);
        //    }
        //    else
        //    {
        //        return "无"; // 或者返回一个适当的错误信息
        //    }
        //}
    }

}
