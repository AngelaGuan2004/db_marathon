using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using SqlSugar;

namespace MarathonMaster.Models
{
    [SugarTable("RESULT_")]
    public class Result_
    {
        //主码需要特殊说明
        [SugarColumn(IsPrimaryKey = true)]
        public int Id { get; set; }              //成绩id
        public int Gun_Result { get; set; }      //枪声成绩
        public int Net_Result { get; set; }      //净成绩
        public int Player_Id { get; set; }       //选手编号
        public int Event_Id { get; set; }        //赛事编号
        public int Rank { get; set; }            //排名
        public int Gender_Rank { get; set; }     //同性排名
    }
}
