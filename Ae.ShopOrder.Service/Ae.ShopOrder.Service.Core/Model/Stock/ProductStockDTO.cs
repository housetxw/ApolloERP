using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Model.Stock
{
    public class ProductStockDTO
    {
        public string CategoryName { get; set; }

        public string ProductId { get; set; }

        public string ProductName { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public long StockNum { get; set; }

        /// <summary>
        /// 领取数量
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 应收数量
        /// </summary>
        public int ReceiveNum { get; set; }
        /// <summary>
        /// 实收数量
        /// </summary>
        public int ActualNum { get; set; }
        /// <summary>
        /// 货损数量
        /// </summary>
        public int ExceptionNum { get; set; }
        /// <summary>
        /// 缺少数量
        /// </summary>
        public int LessNum { get; set; }


    }
}
