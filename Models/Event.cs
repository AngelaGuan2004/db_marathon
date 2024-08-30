using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using SqlSugar;

namespace MarathonMaster.Models
{
    [SugarTable("EVENT")]
    public class Event
    {
        //主码需要特殊说明
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }             //赛事id
        public string Category { get; set; }    //赛事类型（全马）
        public string Name { get; set; }        //赛事名称
        public int Scale { get; set; }          //赛事规模
        public string Start_Date { get; set; }  //报名开始时间
        public string End_Date { get; set; }    //报名结束时间
        public string Event_Date { get; set; }        //比赛时间
        public string? Is_Drawn { get; set; }    //是否报名
    }
}
