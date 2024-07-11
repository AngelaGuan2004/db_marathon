using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using SqlSugar;

namespace db_marathon.Models
{
    [SugarTable("PHOTO")]
    public class Photo
    {
        //主码需要特殊说明
        [SugarColumn(IsPrimaryKey = true)]
        public int Id { get; set; }             //照片id
        public string Time { get; set; }        //拍摄时间
        public string Location { get; set; }    //拍摄地点
        public string Photographer_id { get; set; }//摄影师id
    }
}
