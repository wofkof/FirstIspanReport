using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 這是扭蛋機系統
{
    public class OrderDetail
    {
        //訂單明細ID
        public int OrderDetailID { get; set; }
        //訂單ID
        public int OrderID { get; set; }
        //會員ID
        public int ProductID { get; set; }
        //商品ID
        public int? SubProductID { get; set; }
        //購買數量
        public int Quantity { get; set; }
        //商品點數
        public int Points { get; set; }
        // 產品名稱
        public string ProductName { get; set; }
        // 小商品名稱
        public string SubProductName { get; set; } 
    }
}
