using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using SqlSugar;

namespace db_marathon.Models
{
    [SugarTable("SCHEDULE")]
    public class Schedule
    {
        //主码需要特殊说明
        [SugarColumn(IsPrimaryKey = true)]
        public int Event_Id { get; set; }           //赛事id
        [SugarColumn(IsPrimaryKey = true)]
        public int Volunteer_Id { get; set; }      //志愿者id
        public string Job_Category { get; set; }   //工作种类
    }
}
