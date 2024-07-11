using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using SqlSugar;
using db_marathon.Models;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Security.Cryptography;


namespace db_marathon.Controllers
{
    [Route("/[controller]/[action]")]
    [ApiController]
    [EnableCors("any")]
    public class PlayerController : Controller
    {
        //添加Player（注册需要手动输入Name、Id_Number、Password、Gender、Age、Region、Region、Telephone_Number 。平台分配id）
        [HttpPost]
        public bool add_player(int Id, string Name, string Gender, int Age, string Id_Number, string Region, string Telephone_Number, string Password)
        {
            //获取数据库连接
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();

            Player player = new Player();
            player.Id = Id;
            player.Name = Name;
            player.Gender = Gender;
            player.Age = Age;
            player.Id_Number = Id_Number;
            player.Region = Region;
            player.Telephone_Number = Telephone_Number;
            player.Password = Password;

            int count = db.Insertable(player).ExecuteCommand();
            if (count == 1)
                return true;
            else
                return false;
        }

        //登录
        [HttpGet]
        public bool login(string Name,string Id_Number, string Password)
        {
            Player player = new Player();
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();//获取数据库连接

            player = db.Queryable<Player>().Where(it => it.Name == Name && it.Id_Number == Id_Number && it.Password == Password).First();
            if (player != null)
            {
                return true;
            }
            else
            {
                return false; // 或者返回一个适当的错误信息
            }
        }

        //查询id的选手
        [HttpGet]
        public string get_by_id(int Id)
        {
            Player player = new Player();
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();//获取数据库连接

            player = db.Queryable<Player>().Where(it => it.Id == Id).First();
            if (player != null)
            {
                return JsonSerializer.Serialize(player);
            }
            else
            {
                return "无"; // 或者返回一个适当的错误信息
            }
        }

        //删除选手
        [HttpDelete]
        public bool delete_player(int Id ,string Name)
        {
            //获取数据库连接
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();
            //删除
            try
            {
                int count = db.Deleteable<Player>().Where(it => it.Id == Id && it.Name == Name ).ExecuteCommand();
                if (count == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}
