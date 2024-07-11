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

namespace db_marathon.Controllers
{
    [Route("/[controller]/[action]")]
    [ApiController]
    [EnableCors("any")]
    public class PrizeController : Controller
    {
        // 添加prize
        [HttpPost]
        public bool add_prize(int Id, string Item)
        {
            // 获取数据库连接
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();

            Prize prize = new Prize();
            prize.Id = Id;
            prize.Item = Item;

            int count = db.Insertable(prize).ExecuteCommand();
            return count == 1;
        }

        // 查询Id的奖品
        [HttpGet]
        public ActionResult<Prize> get_by_id(int Id)
        {
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance(); // 获取数据库连接

            Prize prize = db.Queryable<Prize>().Where(it => it.Id == Id).First();
            if (prize != null)
            {
                return prize;
            }
            else
            {
                return NotFound();
            }
        }

        // 查询所有奖品
        [HttpGet]
        public List<Prize> get_all()
        {
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance(); // 获取数据库连接

            try
            {
                List<Prize> prizeList = db.Queryable<Prize>().ToList();
                return prizeList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // 删除prize
        [HttpDelete]
        public bool delete_prize(int Id)
        {
            // 获取数据库连接
            dbORM dborm = new dbORM();
            SqlSugarClient db = dborm.getInstance();

            try
            {
                int count = db.Deleteable<Prize>().Where(it => it.Id == Id).ExecuteCommand();
                return count == 1;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}