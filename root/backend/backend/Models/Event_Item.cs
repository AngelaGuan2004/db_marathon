using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using SqlSugar;

namespace db_marathon.Models
{
    [SugarTable("EVENT_ITEM")]
    public class Event_Item
    {
        //主码需要特殊说明
        [SugarColumn(IsPrimaryKey = true)]
        public int Event_Id { get; set; }          //赛事id
        [SugarColumn(IsPrimaryKey = true)]
        public int Item_Id { get; set; }           //物品id
        public string KIND { get; set; }           //物资类型
    }
}
