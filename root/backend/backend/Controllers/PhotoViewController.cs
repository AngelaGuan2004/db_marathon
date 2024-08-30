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
using MarathonMaster.Models;

using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Drawing;
using Microsoft.AspNetCore.Authorization;


using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Controllers
{
    [Route("/[Controller]/[action]")] // 指定路由模板和控制器的基本配置
    [ApiController]
    [EnableCors("any")]
    //[EnableCors("AllowAllOrigins")]  // 启用跨域请求    
    public class PhotoViewController : ControllerBase
    {

        private readonly SqlSugarClient _db;
        private readonly ILogger<PhotoViewController> _logger; // 声明日志记录器

        public PhotoViewController(ILogger<PhotoViewController> logger)
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

        //搜索照片--1.根据前端传过来的拍摄地址关键字，返回所有匹配的照片拍摄地址list
        [HttpGet]
        public async Task<IActionResult> inquiry_location([FromQuery] string location_key) //收到地址关键字
        {
            _logger.LogInformation("收到照片拍摄地址关键字信息: {@location_key}", location_key); // 记录收到的数据

            try
            {
                List<string> locations = await _db.Queryable<Photo>().Distinct().Where(p => p.Location.Contains(location_key)).Select(p => p.Location).ToListAsync();

                _logger.LogInformation("查找照片拍摄地址关键字对应的地址信息成功: {@location_key}", location_key); // 查找地址关键字对应的地址信息成功
                return Ok(locations); //true表示成功
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "查找照片拍摄地址关键字对应的地址信息失败: {@location_key}", location_key); // 记录错误信息

                return BadRequest(false); //false表示失败
            }
        }

        //搜索照片--2.根据前端传过来的确定拍摄地址，返回所有匹配的照片list
        [HttpGet]
        public async Task<IActionResult> inquiry_photo_by_location([FromQuery] string location) //收到地址
        {
            _logger.LogInformation("收到照片地址信息: {@location}", location); // 记录收到的数据

            try
            {
                List<Photo> photos = await _db.Queryable<Photo>().Where(e => e.Location == location).ToListAsync();

                //var query= await _db.Queryable<Photo>().Where(it => it.Location == location).ToListAsync();

                //var photos = query.ToList();

                if (photos == null || photos.Count == 0)
                {
                    _logger.LogInformation("查找地址对应的照片信息不存在: {@location}", location); // 查找地址对应的照片信息成功
                    return Ok("不存在"); //true表示成功
                }
                else
                {
                    _logger.LogInformation("查找地址对应的照片信息成功: {@location}", location); // 查找地址对应的照片信息成功
                    return Ok(photos); //true表示成功
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "查找地址对应的照片信息失败: {@location}", location); // 记录错误信息

                return BadRequest(false); //false表示失败
            }
        }


        //搜索照片--3.根据前端传过来的赛事名称，返回所有匹配的照片list
        [HttpGet]
        public async Task<IActionResult> inquiry_photo_by_eventname([FromQuery] string event_name) //收到地址
        {
            _logger.LogInformation("收到照片的赛事名称信息: {@event_name}", event_name); // 记录收到的数据

            try
            {
                List<Photo> photos = await _db.Queryable<Photo>().
                    LeftJoin<Event>((p, e) => p.Event_id == e.Id).Where((p,e) => e.Name == event_name).
                    Select(p => p).ToListAsync();

                _logger.LogInformation("查找赛事名称对应的照片信息成功: {@event_name}", event_name); // 查找赛事名称对应的照片信息成功
                return Ok(photos); //true表示成功
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "查找赛事名称对应的照片信息失败: {@event_name}", event_name); // 记录错误信息

                return BadRequest(false); //false表示失败
            }
        }

        //点赞照片
        [HttpPatch]

        public async Task<IActionResult> like_photo([FromQuery] int id) //收到地址
        {
            _logger.LogInformation("收到要点赞的照片id信息: {@id}", id); // 记录收到的数据

            try
            {
                await _db.Updateable<Photo>().SetColumns(it => it.Likes == it.Likes +1).Where(it => it.Id ==id).ExecuteCommandAsync();
                //_logger.LogInformation("查到的照片: {@p.Id}", p.Id); // 更新点赞数成功
                /* var updateObj = new Photo();  //创建一个Photo对象的实例updateObj。虽然这个对象没有被直接用到，但它的类型被用来指定更新的表。
                await _db.Updateable(updateObj)
                          .SetColumns(it => new Photo() { Likes = it.Likes + 1 })
                          .Where(it => it.Id == id)
                          .ExecuteCommandAsync();*/
                //p.Likes = p.Likes+1 ;
                //await _db.Updateable(p).ExecuteCommandAsync();//根据主键更新

                _logger.LogInformation("点赞指定id的照片成功: {@id}", id); // 更新点赞数成功
                return Ok(true); //true表示成功
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "点赞指定id的照片失败: {@id}", id); // 记录错误信息

                return BadRequest(false); //false表示失败
            }
        }

        //搜索照片--4.根据前端传过来的摄影师id，返回所有匹配的照片list
        [HttpGet]
        public async Task<IActionResult> inquiry_photo_by_photographer([FromQuery] int id) //收到地址
        {
            _logger.LogInformation("收到照片的赛事名称信息: {@id}", id); // 记录收到的数据

            try
            {
                List<Photo> photos = await _db.Queryable<Photo>().Where(it => it.Photographer_id == id).ToListAsync();

                _logger.LogInformation("查找摄影师id对应的照片信息成功: {@id}", id); // 查找赛事名称对应的照片信息成功
                return Ok(photos); //true表示成功
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "查找摄影师id对应的照片信息失败: {@id}", id); // 记录错误信息

                return BadRequest(ex); //false表示失败
            }
        }
        //获取所有照片
        [HttpGet]
        public async Task<IActionResult> get_all_photos()
        {
            try
            {

                List<Photo_Information> information_of_photos = await _db.Queryable<Photo>()
               .LeftJoin<Event>((p, e) => p.Event_id == e.Id)//多个条件用&&
               .LeftJoin<Photographer>((p1, e, p2) => p1.Photographer_id == p2.Id)
               .Select((p1, e, p2) => new Photo_Information
               {
                   Id = p1.Id,
                   Event_name = e.Name,
                   Time = p1.Time,
                   Location = p1.Location,
                   Photographer_name = p2.Name,
                   Address = p1.Address,
                   Likes = p1.Likes
               })
               .ToListAsync();


                _logger.LogInformation("查找所有照片成功：{@information_of_photos}", information_of_photos); // 查找赛事名称对应的照片信息成功

                // 使用 Console.WriteLine 在命令行输出照片信息
                foreach (var photo in information_of_photos)
                {
                    Console.WriteLine($"Id: {photo.Id}, Location: {photo.Location}");
                }
                return Ok(information_of_photos); //true表示成功
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "查找所有照片失败"); // 记录错误信息

                return BadRequest(ex); //false表示失败
            }
        }

    }
}
