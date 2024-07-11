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
    public class MedicalpointController : Controller
    {
        //添加Medicalpoint
        [HttpPost]
        public bool add_medicalpoint(int Id, string Place)
        {
            //获取数据库连接
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();

            Medicalpoint medicalpoint = new Medicalpoint();
            medicalpoint.Id = Id;
            medicalpoint.Place = Place;

            int count = db.Insertable(medicalpoint).ExecuteCommand();
            if (count == 1)
                return true;
            else
                return false;
        }

        //查询id的医疗点位置
        [HttpGet]
        public string get_by_id(int Id)
        {
            Medicalpoint medicalpoint = new Medicalpoint();
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();//获取数据库连接

            medicalpoint = db.Queryable<Medicalpoint>().Where(it => it.Id == Id).First();
            if (medicalpoint != null)
            {
                return medicalpoint.Place;
            }
            else
            {
                return "无"; // 或者返回一个适当的错误信息
            }
        }

        //删除医疗点
        [HttpDelete]
        public bool delete_medicalpoint(int Id, string Place)
        {
            //获取数据库连接
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();
            //删除
            try
            {
                int count = db.Deleteable<Medicalpoint>().Where(it => it.Id == Id && it.Place == Place).ExecuteCommand();
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
