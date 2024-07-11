using System;
using System.Data; //to user the plug to introduce the 'using'
using Oracle.ManagedDataAccess.Client;
using System.Data.SqlClient;
using SqlSugar;

namespace WebApplication1
{
    public class dbORM   // 数据库ORM类，用于配置和获取数据库实例
    {
        public SqlSugarClient getInstance() //sqlsuger是一种ORM框架
        {
            SqlSugarClient db = new SqlSugarClient(connect_info);   // 创建SqlSugarClient对象并返回
            return db;
        }

        public static ConnectionConfig connect_info = new ConnectionConfig()  // 数据库连接配置信息
        {
            ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=100.78.236.219)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=hr;Password=hr;",
            DbType = SqlSugar.DbType.Oracle, // 指定数据库类型为Oracle
            IsAutoCloseConnection = true,  // 自动关闭连接
            InitKeyType = SqlSugar.InitKeyType.Attribute  // 初始化键类型为属性
        };
    }
    public class UserDB   
    {
        // 数据库连接字符串
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=106.14.193.8)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=normal;Password=TJUsoftware2020;";
        public OracleConnection con;  // Oracle数据库连接对象

        // 连接到Oracle数据库的方法
        public bool connectOracle()
        {
            try
            {
                // 建立SqlConnection对象
                con = new OracleConnection(connString);
                // 打开连接
                con.Open();
                return true; // 如果连接成功，返回true

            }
            catch (Exception ex)
            {
                return false;   // 如果连接失败，返回false
            }
        }
    }
}
