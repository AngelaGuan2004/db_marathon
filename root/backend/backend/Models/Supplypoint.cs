using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using SqlSugar;

namespace MarathonMaster.Models
{
    [SugarTable("SUPPLYPOINT")]
    public class Supplypoint
    {
        //主码需要特殊说明
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }            //物资点id
        public string Place { get; set; }     //物资点位置
        public string Kind { get; set; }      //物资点类型
    }
}
