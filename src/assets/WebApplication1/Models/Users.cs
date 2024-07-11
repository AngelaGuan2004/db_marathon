
using System;
using System.Data; //to user the plug to introduce the 'using'
using Oracle.ManagedDataAccess.Client;
using System.Data.SqlClient;
using SqlSugar;
namespace WebApplication1.Models
{
    [SugarTable("users")]  // 使用SqlSugar框架进行ORM映射，指定数据库表名为“player”
    public class Users
    {

        public string? user_name { get; set; }

        [SugarColumn(IsPrimaryKey = true)]
        public string? id { get; set; }  // 指定id_number为数据库表的主键,?表示可以非空

      

        public string? password { get; set; }

    }
}
