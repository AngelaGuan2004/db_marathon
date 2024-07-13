using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using SqlSugar;

namespace MarathonMaster.Models
{
    [SugarTable("VOLUNTEER")]
    public class Volunteer
    {
        //主码需要特殊说明
        [SugarColumn(IsPrimaryKey = true)]
        public int Id { get; set; }                 //志愿者id
        public string Telephone_Number { get; set; }//志愿者电话
        public string Name { get; set; }            //志愿者名字
        public string? Supplypoint_Id { get; set; }    //物资点id 可空
        public string? Medicalpoint_Id { get; set; }   //医疗点id 可空
        public string Password { get; set; }//志愿者登录密码
    }
}
