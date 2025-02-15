using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 這是扭蛋機系統
{
    public class SubProductUpdateRequest
    {
        public int SP_Amount { get; set; }
    }
    public class SubProducts
    {
        //小商品ID
        public int SP_ID { get; set; }
        //商品ID
        public int P_ID { get; set; }
        //小商品名稱
        public string SP_Name { get; set; }
        //小商品數量
        public int SP_Amount { get; set; }
        //小商品圖片
        public string SP_Image { get; set; }
    }
}
