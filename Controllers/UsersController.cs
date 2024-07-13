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
using WebApplication1.Models;
using System.Drawing;
using Microsoft.AspNetCore.Authorization;


namespace WebApplication1.Controllers
{
    [Route("/[Controller]/[action]")] // 指定路由模板和控制器的基本配置
    [ApiController]
    //[EnableCors("any")]
    [EnableCors("AllowAllOrigins")]  // 启用跨域请求    
    public class UsersController : ControllerBase
    {
        [HttpPost] //响应post请求
        //[Authorize]  //经过认证才能访问此地
        public string AddUser(string user_name,string id, string password)
        {
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();//获取数据库连接

            //检查是否存在指定身份证号的用户
            bool have_u = db.Queryable<Users>().Any(it => it.id== id);
        
            if (!have_u)  //如果不存在指定身份证号的用户
            {

                Users new_user = new Users();
                new_user.user_name = user_name;
                new_user.id = id;        
                new_user.password = password;

                // 将new_user对象插入到数据库中，并返回新插入记录的自增主键值
                long re_id = db.Insertable(new_user).ExecuteReturnBigIdentity();

                // 将自增主键值转换为字符串返回
                string resdata = re_id + "";
                return resdata;

            }
            else  //如果存在指定身份证号的用户
            {
                return "already exist";
            }




        }
    }
}
