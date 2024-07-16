using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using SqlSugar;

namespace db_marathon.Models
{
    [SugarTable("REWARD")]
    public class Reward
    {
        //主码需要特殊说明
        [SugarColumn(IsPrimaryKey = true)]
        public int Photo_Id { get; set; }       //照片id
        [SugarColumn(IsPrimaryKey = true)]
        public int Player_Id { get; set; }      //选手id
    }
}
