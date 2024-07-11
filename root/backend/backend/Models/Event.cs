using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using SqlSugar;

namespace db_marathon.Models
{
    [SugarTable("EVENT")]
    public class Event
    {
        //主码需要特殊说明
        [SugarColumn(IsPrimaryKey = true)]
        public int Id { get; set; }             //赛事id
        public string Category { get; set; }    //赛事类型（全马）
        public string Name { get; set; }        //赛事名称
        public string Start_Date { get; set; }  //报名开始时间
        public string End_Date { get; set; }    //报名结束时间
        public string Date { get; set; }        //比赛时间
    }
}
