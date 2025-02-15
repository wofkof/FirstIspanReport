using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 這是扭蛋機系統
{
    public class PointsHistory
    {
        public int HistoryID { get; set; }      // 記錄 ID
        public int UserID { get; set; }         // 會員 ID
        public int PointsChanged { get; set; }  // 變動的代幣數量
        public decimal CashSpent { get; set; }  // 會員花費金額
        public DateTime ChangeDate { get; set; } = DateTime.Now; // 交易時間
        public string? PaymentMethod { get; set; } // 付款方式
        public string Description { get; set; } = "會員儲值"; // 交易描述
        public int PointsToDeduct { get; set; } // ✅ 這裡是 "點數變化量"，但存到資料庫時會變成 `PointsChanged`
        public int ProductID { get; set; }
    }
    public class PointsHistoryDto
    {
        public int HistoryID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; } // ✅ 確保有 Name
        public decimal CashSpent { get; set; }
        public DateTime ChangeDate { get; set; }
        public string PaymentMethod { get; set; }
        public string Description { get; set; }
    }

    public class TopUpHistory
    {
        public DateTime TopUpDate { get; set; }  // 儲值時間
        public decimal AmountSpent { get; set; } // 消費金額
        public string PaymentMethod { get; set; } // 付款方式
    }
}