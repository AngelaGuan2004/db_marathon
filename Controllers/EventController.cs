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
    [Route("/[Controller]/[action]")]
    [ApiController]
    [EnableCors("any")]
    public class EventController : Controller
    {
        //上传Event
        [HttpPost]
        public bool add_event(int Id, string Category, string Name,string Start_Date, string End_Date, string Event_Date, int Scale)
        {
            //获取数据库连接
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();

            Event event1 = new Event();
            event1.Id=Id;
            event1.Category=Category;
            event1.Name=Name;
            event1.Start_Date=Start_Date;
            event1.End_Date=End_Date;
            event1.Event_Date = Event_Date;
            event1.Scale=Scale;


            int count = db.Insertable(event1).ExecuteCommand();
            if (count == 1)
                return true;
            else
                return false;
        }

        //查询对应id的赛事
        [HttpGet]
        public string get_by_id(int Id)
        {
            Event event1 = new Event();
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();//获取数据库连接

            event1 = db.Queryable<Event>().Where(it => it.Id == Id).First();
            return JsonSerializer.Serialize(event1);
        }

        //查询对应种类的赛事
        [HttpGet]
        public List<Event> get_by_category(string Category)
        {
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();//获取数据库连接

            try
            {
                List<Event> eventlist = db.Queryable<Event>().Where(it => it.Category == Category).ToList();
                return eventlist;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //查询对应名称的赛事
        [HttpGet]
        public string get_by_name(string Name)
        {
            Event event1 = new Event();
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();//获取数据库连接

            event1 = db.Queryable<Event>().Where(it => it.Name == Name).First();
            return JsonSerializer.Serialize(event1);
        }

        //删除赛事
        [HttpDelete]
        public bool delete_event(int Id)
        {
            //获取数据库连接
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();
            //删除
            try
            {
                int count = db.Deleteable<Event>().Where(it => it.Id == Id ).ExecuteCommand();
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
