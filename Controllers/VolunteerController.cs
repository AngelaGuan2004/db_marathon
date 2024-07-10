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
    public class VolunteerController : Controller
    {
        //添加volunteer(注册时候输入Name，Telephone_Number，Password，平台分配id、Supplypoint_Id、Medicalpoint_Id)
        [HttpPost]
        public bool add_volunteer(int Id,string Name, string Telephone_Number,string Password)
        {
            //获取数据库连接
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();

            Volunteer volunteer = new Volunteer();
            volunteer.Id = Id;
            volunteer.Name = Name;
            volunteer.Telephone_Number = Telephone_Number;
            volunteer.Password = Password;
            volunteer.Medicalpoint_Id = null;
            volunteer.Supplypoint_Id = null;//注册的时候置为空

            int count = db.Insertable(volunteer).ExecuteCommand();
            if (count == 1)
                return true;
            else
                return false;
        }

        //登录
        [HttpGet]
        public bool login(string Name, string Telephone_Number, string Password)
        {
            Volunteer volunteer = new Volunteer();
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();//获取数据库连接

            volunteer = db.Queryable<Volunteer>().Where(it => it.Name == Name && it.Telephone_Number == Telephone_Number && it.Password == Password).First();
            if (volunteer != null)
            {
                return true;
            }
            else
            {
                return false; // 或者返回一个适当的错误信息
            }
        }

        //查询name的志愿者
        [HttpGet]
        public int get_by_name(string Name)
        {
            Volunteer volunteer = new Volunteer();
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();//获取数据库连接

            volunteer = db.Queryable<Volunteer>().Where(it => it.Name == Name).First();
            if (volunteer != null)
            {
                return volunteer.Id;
            }
            else
            {
                return -1; // 或者返回一个适当的错误信息
            }
        }

        //查询id的志愿者
        [HttpGet]
        public string get_by_id(int Id)
        {
            Volunteer volunteer = new Volunteer();
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();//获取数据库连接

            volunteer = db.Queryable<Volunteer>().Where(it => it.Id == Id).First();
            if (volunteer != null)
            {
                return volunteer.Name;
            }
            else
            {
                return "无"; // 或者返回一个适当的错误信息
            }
        }

        //分配Supplypoint和Medicalpoint
        [HttpPatch]
        public bool volunteer_supplypoint(int Id, int Supplypoint_Id)
        {
            Volunteer volunteer = new Volunteer();
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();//获取数据库连接

            volunteer = db.Queryable<Volunteer>().Where(it => it.Id == Id).First();
            if (volunteer != null && volunteer.Medicalpoint_Id == null)
            {
                volunteer.Supplypoint_Id= Supplypoint_Id;
                return true;
            }
            else
            {
                return false; // 或者返回一个适当的错误信息
            }
        }

        [HttpPatch]
        public bool volunteer_medicalpoint(int Id, int Medicalpoint_Id)
        {
            Volunteer volunteer = new Volunteer();
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();//获取数据库连接

            volunteer = db.Queryable<Volunteer>().Where(it => it.Id == Id).First();
            if (volunteer != null && volunteer.Supplypoint_Id == null)
            {
                volunteer.Medicalpoint_Id = Medicalpoint_Id;
                return true;
            }
            else
            {
                return false; // 或者返回一个适当的错误信息
            }
        }

        
        
    }

}
