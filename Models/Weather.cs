using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using SqlSugar;

namespace db_marathon.Models
{
    [SugarTable("WEATHER")]
    public class Weather
    {
        //主码需要特殊说明
        [SugarColumn(IsPrimaryKey = true)]
        public int Id { get; set; }                 //天气id,强实体集event的主码
        public int Time { get; set; }               //时间  类型暂时还不确定
        public int Temperature { get; set; }        //温度
        public string Condition { get; set; }       //天气情况
        public int Whether_To_Proceed { get; set; } //是否进行
    }
}
