using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using SqlSugar;

namespace MarathonMaster.Models
{
    [SugarTable("MEDICAL_SERVICE")]
    public class Medical_Service
    {
        //主码需要特殊说明
        [SugarColumn(IsPrimaryKey = true)]
        public int Medicalpoint_Id { get; set; }   //医疗点id
        [SugarColumn(IsPrimaryKey = true)]
        public int Player_Id { get; set; }         //选手名字
    }
}
