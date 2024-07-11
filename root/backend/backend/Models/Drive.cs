using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using SqlSugar;

namespace db_marathon.Models
{
    [SugarTable("DRIVE")]
    public class Drive
    {
        //主码需要特殊说明
        [SugarColumn(IsPrimaryKey = true)]
        public int Volunteer_Id { get; set; }   //志愿者id
        [SugarColumn(IsPrimaryKey = true)]
        public int Shuttlecar_Id { get; set; }//班车id
    }
}
