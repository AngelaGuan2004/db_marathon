using System;
using System.Data; 
using Oracle.ManagedDataAccess.Client;
using System.Data.SqlClient;
using SqlSugar;

namespace backend
{
    public class dbORM
    {
        public SqlSugarClient getInstance()
        {
            SqlSugarClient db = new SqlSugarClient(connect_info);
            return db;
        }
        public static ConnectionConfig connect_info = new ConnectionConfig()
        {
            ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=system;Password=oracle;",
            DbType = SqlSugar.DbType.Oracle,
            IsAutoCloseConnection = true,
            InitKeyType = SqlSugar.InitKeyType.Attribute
        };
    }
    public class UserDB
    {
        public static string connString = "DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));Persist Security Info=True;User ID=system;Password=oracle;";
        public OracleConnection con;
        public bool connectOracle()
        {
            try
            {
                // 建立SqlConnection对象
                con = new OracleConnection(connString);
                // 打开连接
                con.Open();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
