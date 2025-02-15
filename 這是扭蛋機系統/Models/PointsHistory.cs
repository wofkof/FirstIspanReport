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
            public int PointsChanged { get; set; }
            public decimal CashSpent { get; set; }
            public string PaymentMethod { get; set; }
            public string Description { get; set; }
            public DateTime ChangeDate { get; set; }
        }
    public class TotalPointsResponse
    {
        public int UserID { get; set; }
        public int TotalPoints { get; set; }
    }
    public class TotalCashResponse
    {
        public decimal TotalCashSpent { get; set; }
    }
    public class TopUpHistory
    {
        public DateTime TopUpDate { get; set; }  // 儲值時間
        public decimal AmountSpent { get; set; } // 消費金額
        public string PaymentMethod { get; set; } // 付款方式
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
}

