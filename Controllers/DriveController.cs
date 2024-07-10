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
    public class DriveController : Controller
    {
        //添加drive
        public bool add_drive(int Volunteer_Id, int Shuttlecar_Id)
        {
            //获取数据库连接
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();

            Drive drive = new Drive();
            drive.Volunteer_Id = Volunteer_Id;
            drive.Shuttlecar_Id = Shuttlecar_Id;

            int count = db.Insertable(drive).ExecuteCommand();
            if (count == 1)
                return true;
            else
                return false;
        }

        //查询volunteer_id的Shuttlecar_Id
        [HttpGet]
        public int get_by_volunteer_id(int Volunteer_Id)
        {
            Drive drive = new Drive();
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();//获取数据库连接

            drive = db.Queryable<Drive>().Where(it => it.Volunteer_Id == Volunteer_Id).First();
            if (drive != null)
            {
                return drive.Shuttlecar_Id;
            }
            else
            {
                return -1; // 或者返回一个适当的错误信息
            }
        }

        //查询Shuttlecar_Id的volunteer_id
        [HttpGet]
        public int get_by_shuttlecar_Id(int Shuttlecar_Id)
        {
            Drive drive = new Drive();
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();//获取数据库连接

            drive = db.Queryable<Drive>().Where(it => it.Shuttlecar_Id == Shuttlecar_Id).First();
            if (drive != null)
            {
                return drive.Volunteer_Id;
            }
            else
            {
                return -1; // 或者返回一个适当的错误信息
            }
        }

        //删除drive
        [HttpDelete]
        public bool delete_drive(int Volunteer_Id, int Shuttlecar_Id)
        {
            //获取数据库连接
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();
            //删除
            try
            {
                int count = db.Deleteable<Drive>().Where(it => it.Volunteer_Id == Volunteer_Id && it.Shuttlecar_Id == Shuttlecar_Id).ExecuteCommand();
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
