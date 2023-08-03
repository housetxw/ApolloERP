using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Client.Response.Product
{
    public class GetShopProductByCodeResponse
    {
        public long Id { get; set; }

        public long ShopId { get; set; }

        public long CategoryId { get; set; }

        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public decimal StandardPrice { get; set; }

        public decimal SalesPrice { get; set; }

        /// <summary>
        /// 采购价
        /// </summary>
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// 采购成本价格
        /// </summary>
        public decimal PurchaseCost { get; set; }

        public int SortType { get; set; }

        public int IsTop { get; set; }

        public int OnSale { get; set; }

        public string OnSaleTime { get; set; }

        public string Icon { get; set; }

        public string Remark { get; set; }

        public int IsDeleted { get; set; }

        public decimal DiscountRate { get; set; }
        /// <summary>
        /// 规格型号
        /// </summary>
        public string Specification { get; set; } = string.Empty;

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; } = string.Empty;

        /// <summary>
        /// 原厂编号
        /// </summary>
        public string OeNumber { get; set; } = string.Empty;

    }
}
