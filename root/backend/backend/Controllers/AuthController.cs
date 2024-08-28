using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using SqlSugar;
using MarathonMaster.Models;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Security.Cryptography;
using Microsoft.Extensions.Logging; // 引入日志记录命名空间
using System.Numerics;
using MarathonMaster.Models;
using MarathonMaster;
using System.Net.Cache;


namespace MarathonMaster.Controllers
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
        public async Task<IActionResult> add_player([FromBody] PlayerLogin player0)
        {
            _logger.LogInformation("收到选手数据: {@Player}", player0); // 记录收到的数据

            try
            {
                // 查询数据库中的最大 Player_Id
                var maxPlayerId = await _db.Queryable<Player>()
                                           .MaxAsync(it => it.Id);

                // 如果数据库中还没有记录，设置初始值为1
                int newPlayerId = (maxPlayerId != 0) ? maxPlayerId + 1 : 1;

                Player player = new Player();
                player.Name = player0.Name;
                player.Id_Number = player0.Id_Number;
                player.Gender = player0.Gender;
                player.Password = player0.Password;

                // 为新选手设置唯一的 Player_Id
                player.Id = newPlayerId;

                // 插入新选手数据
                await _db.Insertable(player).ExecuteCommandAsync();
                _logger.LogInformation("成功插入选手数据: {@Player}", player); // 记录插入成功

                return Ok(newPlayerId);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "插入选手数据失败"); // 记录错误信息

                return BadRequest(-1);
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
        [HttpPost]
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
        [HttpPost]
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
        [HttpPost]
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

        //查询id的选手
        [HttpGet]
        public string get_by_playerid(int Id)
        {
            Player player = new Player();
            player = _db.Queryable<Player>().Where(it => it.Id == Id).First();
            if (player != null)
            {
                return JsonSerializer.Serialize(player);
            }
            else
            {
                return "无"; // 或者返回一个适当的错误信息
            }
        }
        //查询id的志愿者
        [HttpGet]
        public string get_by_volunteerid(int Id)
        {
            Volunteer volunteer = new Volunteer();
            volunteer = _db.Queryable<Volunteer>().Where(it => it.Id == Id).First();
            if (volunteer != null)
            {
                return JsonSerializer.Serialize(volunteer);
            }
            else
            {
                return "无"; // 或者返回一个适当的错误信息
            }
        }
        //查询id的摄影师
        [HttpGet]
        public string get_by_photographerid(int Id)
        {
            Photographer photographer = new Photographer();
            photographer = _db.Queryable<Photographer>().Where(it => it.Id == Id).First();
            if (photographer != null)
            {
                return JsonSerializer.Serialize(photographer);
            }
            else
            {
                return "无"; // 或者返回一个适当的错误信息
            }
        }

    }

}