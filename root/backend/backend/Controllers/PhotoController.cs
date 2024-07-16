using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using SqlSugar;
using WebApplication1;

using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using MarathonMaster.Models;
using System.Drawing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;

namespace MarathonMaster.Controllers
{
    [Route("/[Controller]/[action]")] // 指定路由模板和控制器的基本配置
    [ApiController]
    //[EnableCors("any")]
    [EnableCors("AllowAllOrigins")]  // 启用跨域请求    
    public class PhotoController : ControllerBase
    {

        private readonly SqlSugarClient _db;
        private readonly ILogger<PhotoController> _logger; // 声明日志记录器

        public PhotoController(ILogger<PhotoController> logger)
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

        //上传照片
        [HttpPost]
        public async Task<IActionResult> upload_photo([FromBody] int id, string time, string location, int photographer_id, IFormFile file)  //IFormFile file 参数表示上传的文件，这里指photo
        {
            _logger.LogInformation("收到要上传的照片id: {@id}", id); // 记录收到的数据

            if (file == null || file.Length == 0)  //检查文件是否为空或文件大小是否为零
                return Unauthorized("No file uploaded.");

         
            var uploads = Path.Combine("/myapp/photo", "uploads");
            //var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            //意味着上传文件将被存储在'_environment.WebRootPath/uploads'目录下

            if (!Directory.Exists(uploads))  //如果目录不存在，则创建目录
            {
                Directory.CreateDirectory(uploads);
            }

            var filePath = Path.Combine(uploads, file.FileName); //生成文件的完整路径

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }  //使用filestream创建一个新文件

            var photo = new Photo
            {
                Id = id,
                Time = time,
                Location = location,
                Photographer_id = photographer_id,
                Address = filePath
            };  //创建要插入的photo信息，并赋值


            try
            {
                await _db.Insertable(photo).ExecuteCommandAsync();
                _logger.LogInformation("成功上传照片数据: {@id}", id); // 照片信息插入成功
                return Ok(true); //true表示成功
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "上传照片数据失败: {@id}", id); // 记录错误信息

                return BadRequest(false); //false表示失败
            }
        }

        //删除照片：根据前端传来的照片id在数据库中删除对应的照片信息
        [HttpDelete]
        public async Task<IActionResult> delete_photo([FromBody] int id) 
        {
            _logger.LogInformation("收到要删除的照片id: {@id}", id); // 记录收到的数据

            try
            {
                await _db.Deleteable<Photo>(new Photo() { Id = id }).ExecuteCommandAsync(); //删除id匹配的照片数据
                _logger.LogInformation("成功删除照片数据: {@id}", id); // 照片信息删除成功
                return Ok(true); //true表示成功
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "删除照片数据失败: {@id}", id); // 记录错误信息

                return BadRequest(false); //false表示失败
            }
        }
    }
}
