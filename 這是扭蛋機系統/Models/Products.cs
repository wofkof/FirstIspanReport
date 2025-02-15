using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 這是扭蛋機系統
{ 
    public class Products
    {
        //商品ID
        public int P_ID { get; set; }
        //商品名稱
        public string P_Name { get; set; }
        //商品點數
        public int P_Points { get; set; }
        //商品圖片
        public string P_Image{ get; set; }
        //商品分類
        public string P_Class { get; set; }
    }
    public class CartItem
    {
        public int ProductID { get; set; }   // 商品 ID
        public string ProductName { get; set; } // 商品名稱
        public string ImagePath { get; set; }  // 圖片路徑
        public int Quantity { get; set; }   // 選擇的數量
        public int Price { get; set; }   // 單價
        public int TotalPrice => Quantity * Price; // 小計
    }
}
