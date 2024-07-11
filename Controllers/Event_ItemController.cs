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
    public class Event_ItemController : Controller
    {
        //添加event_item
        [HttpPost]
        public bool add_event_item(int Event_Id, int Item_Id,string KIND)
        {
            //获取数据库连接
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();

            Event_Item event_item = new Event_Item();
            event_item.Event_Id = Event_Id;
            event_item.Item_Id = Item_Id;
            event_item.KIND = KIND;

            int count = db.Insertable(event_item).ExecuteCommand();
            if (count == 1)
                return true;
            else
                return false;
        }

        //查询Event_Id的物品
        [HttpGet]
        public List<Event_Item> get_by_event_id(int Event_Id)
        {
            Event_Item event_item = new Event_Item();
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();//获取数据库连接

            try
            {
                List<Event_Item> event_itemlist = db.Queryable<Event_Item>().Where(it => it.Event_Id == Event_Id).ToList();
                return event_itemlist;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //查询Item_Id的KIND
        [HttpGet]
        public string get_by_item_Id(int Item_Id)
        {
            Event_Item event_item = new Event_Item();
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();//获取数据库连接

            event_item = db.Queryable<Event_Item>().Where(it => it.Item_Id == Item_Id).First();
            if (event_item != null)
            {
                return event_item.KIND;
            }
            else
            {
                return "无"; // 或者返回一个适当的错误信息
            }
        }

        //删除event_item
        [HttpDelete]
        public bool delete_event_item(int Event_Id, int Item_Id, string KIND)
        {
            //获取数据库连接
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();
            //删除
            try
            {
                int count = db.Deleteable<Event_Item>().Where(it => it.Event_Id == Event_Id&& it.Item_Id== Item_Id && it.KIND== KIND).ExecuteCommand();
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
