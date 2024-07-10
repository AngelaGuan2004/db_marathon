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
    public class ItemController : Controller
    {
        //添加item
        public bool add_item(int Id, string Name)
        {
            //获取数据库连接
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();

            Item item = new Item();
            item.Id = Id;
            item.Name = Name;

            int count = db.Insertable(item).ExecuteCommand();
            if (count == 1)
                return true;
            else
                return false;
        }

        //查询id的物资
        [HttpGet]
        public string get_by_id(int Id)
        {
            Item item = new Item();
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();//获取数据库连接

            item = db.Queryable<Item>().Where(it => it.Id == Id).First();
            if (item != null)
            {
                return item.Name;
            }
            else
            {
                return "无"; // 或者返回一个适当的错误信息
            }
        }

        //查询name的物资
        [HttpGet]
        public int get_by_name(string Name)
        {
            Item item = new Item();
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();//获取数据库连接

            item = db.Queryable<Item>().Where(it => it.Name == Name).First();
            if (item != null)
            {
                return item.Id;
            }
            else
            {
                return -1; // 或者返回一个适当的错误信息
            }
        }

        //删除物资
        [HttpDelete]
        public bool delete_item(int Id, string Name)
        {
            //获取数据库连接
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();
            //删除
            try
            {
                int count = db.Deleteable<Item>().Where(it => it.Id == Id && it.Name == Name).ExecuteCommand();
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
