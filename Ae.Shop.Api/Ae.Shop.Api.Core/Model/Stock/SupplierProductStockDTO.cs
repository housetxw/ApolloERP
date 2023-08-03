using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
    public class SupplierProductStockDTO
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        /// <summary>
        /// 采购量
        /// </summary>
        public int PurchaseNum { get; set; }

        /// <summary>
        /// 库存量
        /// </summary>
        public int StockNum { get; set; }

        /// <summary>
        /// 销售量
        /// </summary>
        public int SaleNum { get; set; }

        /// <summary>
        /// 其他库存量
        /// </summary>
        public int OtherNum { get; set; }

        public long LocationId { get; set; }

        public string LocationName { get; set; }

    }
}
