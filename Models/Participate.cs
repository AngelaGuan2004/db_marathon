using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using SqlSugar;

namespace db_marathon.Models
{
    [SugarTable("PARTICIPATE")]
    public class Participate
    {
        //主码需要特殊说明
        public string? Number_ { get; set; }         //参赛号码id 
        public string Role_ { get; set; }           //比赛种类包括pacer、unparticipate、elite、first_aid、normal、charity
        [SugarColumn(IsPrimaryKey = true)]
        public int Player_Id { get; set; }          //选手id
        [SugarColumn(IsPrimaryKey = true)]
        public int Event_Id { get; set; }           //比赛id
    }
}
