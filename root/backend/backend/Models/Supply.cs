using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using SqlSugar;

namespace MarathonMaster.Models
{
    [SugarTable("SUPPLY")]
    public class Supply
    {
        //主码需要特殊说明
        [SugarColumn(IsPrimaryKey = true)]
        public int Item_Id { get; set; }            //物资id
        [SugarColumn(IsPrimaryKey = true)]
        public string Supplypoint_Id { get; set; }     //物资点id
        public int Amount { get; set; }             //量
    }

    public class Supply_With_Name
    {
        public string Supplypoint_Id { get; set; }     //物资点id
        public int Id { get; set; }             //物品id
        public string Name { get; set; }        //物品名称
        public int Amount { get; set; }             //量
    }
}
