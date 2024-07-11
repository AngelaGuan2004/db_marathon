using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using SqlSugar;

namespace db_marathon.Models
{
    [SugarTable("SUPPLY")]
    public class Supply
    {
        //主码需要特殊说明
        [SugarColumn(IsPrimaryKey = true)]
        public int Item_Id { get; set; }            //物资id
        [SugarColumn(IsPrimaryKey = true)]
        public int Supplypoint_Id { get; set; }     //物资点id
        public int Amount { get; set; }             //量
    }
}
