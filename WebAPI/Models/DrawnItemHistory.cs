using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 這是扭蛋機系統
{
    public class DrawnItemsHistory
    {
        public int DrawID { get; set; }  // 抽取 ID（自動遞增）
        public int UserID { get; set; }  // 會員 ID
        public int ProductID { get; set; }  // 商品 ID
        public int SubProductID { get; set; }  // 小商品 ID
        public string SubProductName { get; set; }  // 小商品名稱
        public int PointsUsed { get; set; }  // 花費的 G 幣
        public DateTime DrawDate { get; set; } = DateTime.Now;  // 抽取時間
    }

    public class DrawnItemsHistoryDTO
    {
        public string Name { get; set; }  // 會員名稱
        public DateTime DrawDate { get; set; }
        public int PointsUsed { get; set; }
        public string SubProductName { get; set; }
    }
}
