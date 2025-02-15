using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 這是扭蛋機系統
{
    public class PointsHistory
    {
        public int HistoryID { get; set; }
        public int UserID { get; set; }
        public int PointsChanged { get; set; } // 積分變動
        public DateTime ChangeDate { get; set; }
        public string Description { get; set; }
    }
}
