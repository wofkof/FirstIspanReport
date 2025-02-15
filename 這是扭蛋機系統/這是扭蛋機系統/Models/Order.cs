using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 這是扭蛋機系統 
{
    public class Order
    {
        //訂單ID
        public int OrderID { get; set; }
        //會員ID
        public int UserID { get; set; }
        //訂單日期
        public DateTime OrderDate { get; set; }
        //使用的點數量
        public int TotalPoints { get; set; }
    }
}



