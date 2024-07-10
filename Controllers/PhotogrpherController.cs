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
    public class PhotographerController : Controller
    {
        //登录
        [HttpGet]
        public bool login(string Name, string Password)
        {
            Photographer photographer = new Photographer();
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();//获取数据库连接

            photographer = db.Queryable<Photographer>().Where(it => it.Name == Name && it.Password == Password).First();
            if (photographer != null)
            {
                return true;
            }
            else
            {
                return false; // 或者返回一个适当的错误信息
            }
        }

        //查询name的摄影师
        [HttpGet]
        public int get_by_name(string Name)
        {
            Photographer photographer = new Photographer();
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();//获取数据库连接

            photographer = db.Queryable<Photographer>().Where(it => it.Name == Name).First();
            if (photographer != null)
            {
                return photographer.Id;
            }
            else
            {
                return -1; // 或者返回一个适当的错误信息
            }
        }

        //查询id的摄影师
        [HttpGet]
        public string get_by_id(int Id)
        {
            Photographer photographer = new Photographer();
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();//获取数据库连接

            photographer = db.Queryable<Photographer>().Where(it => it.Id == Id).First();
            if (photographer != null)
            {
                return photographer.Name;
            }
            else
            {
                return "无"; // 或者返回一个适当的错误信息
            }
        }
        
    }

}
