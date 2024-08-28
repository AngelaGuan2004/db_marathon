using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using SqlSugar;

namespace MarathonMaster.Models
{
    [SugarTable("PLAYER")]
    public class Player
    {
        //主码需要特殊说明
        [SugarColumn(IsPrimaryKey = true)]
        public int Id { get; set; }             //选手id
        public string Name { get; set; }        //选手名字
        public string? Gender { get; set; }      //选手性别
        public int? Age { get; set; }            //选手年龄
        public string Id_Number { get; set; }   //选手身份证号
        public string? Region { get; set; }      //选手国籍
        public string? Telephone_Number { get; set; }//选手电话
        public string Password { get; set; }        //登录密码

    }

    public class PlayerLogin
    {
        public string Name { get; set; }        //选手名字
        public string Gender { get; set; }      //选手性别
        public string Id_Number { get; set; }   //选手身份证号
        public string Password { get; set; }        //登录密码

    }
}
