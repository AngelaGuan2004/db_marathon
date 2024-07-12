using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using SqlSugar;

namespace MarathonMaster.Models
{
    [SugarTable("SHUTTLECAR")]
    public class Shuttlecar
    {
        //主码需要特殊说明
        [SugarColumn(IsPrimaryKey = true)]
        public int Id { get; set; }                 //班车id
        public int Event_Id { get; set; }           //赛事id
        public string Departure_Time { get; set; }  //出发时间
        public string Arrival_Time { get; set; }    //到达时间
    }
}
