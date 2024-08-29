using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using SqlSugar;

namespace MarathonMaster.Models
{
    [SugarTable("Volunteer_Supplypoint")]
    public class VolunteerSupplypoint
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int volunteer_id { get; set; }             //志愿者id

        [SugarColumn(IsPrimaryKey = true)]
        public string supplypoint_id { get; set; }             //医疗点id
    }
}
