using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 這是扭蛋機系統
{
    public class OrderPointsHistory
    {
        //紀錄ID
        public int HistoryID { get; set; }
        //訂單ID
        public int OrderID { get; set; }
        //使用積分數量
        public int PointsUsed { get; set; }
        //使用日期
        public DateTime DateUsed { get; set; }
    }
}
