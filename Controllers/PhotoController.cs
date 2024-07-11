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
    public class PhotoController : Controller
    {
        //查询id的照片
        [HttpGet]
        public int get_by_name(int Id)
        {
            Photo photo = new Photo();
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();//获取数据库连接

            photo = db.Queryable<Photo>().Where(it => it.Id == Id).First();
            if (photo != null)
            {
                return photo.Photographer_Id;
            }
            else
            {
                return -1; // 或者返回一个适当的错误信息
            }
        }

        //查询Photographer_id的照片
        [HttpGet]
        public List<Photo> get_by_photographer_id(int Photographer_Id)
        {
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();//获取数据库连接

            try
            {
                List<Photo> photolist = db.Queryable<Photo>().Where(it => it.Photographer_Id == Photographer_Id).ToList();
                return photolist;
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        //查询Event_id的照片
        [HttpGet]
        public List<Photo> get_by_event_id(int Event_Id)
        {
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();//获取数据库连接

            try
            {
                List<Photo> photolist = db.Queryable<Photo>().Where(it => it.Event_Id == Event_Id).ToList();
                return photolist;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }

}
