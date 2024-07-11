using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using SqlSugar;

namespace db_marathon.Models
{
    [SugarTable("RESULT_TO_PRIZE")]
    public class Result_To_Prize
    {
        //主码需要特殊说明
        [SugarColumn(IsPrimaryKey = true)]
        public int Event_Id { get; set; }       //赛事id
        [SugarColumn(IsPrimaryKey = true)]
        public int Gender_Rank { get; set; }    //性别排名
        public int Prize_Id { get; set; }       //奖品编号
    }
}
