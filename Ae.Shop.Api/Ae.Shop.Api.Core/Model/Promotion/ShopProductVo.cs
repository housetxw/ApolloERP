using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model.Promotion
{
    public class ShopProductVo
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }

        /// <summary>
        ///  产品编码
        /// </summary>
        public string ProductCode { get; set; } = string.Empty;
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 产品显示名称
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// 产品描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// 市场价格
        /// </summary>
        public decimal StandardPrice { get; set; }
        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SalesPrice { get; set; }

        /// <summary>
        /// 上下架
        /// </summary>
        public int OnSale { get; set; }

        /// <summary>
        /// 缩率图
        /// </summary>
        public string Icon { get; set; } = string.Empty;
    }
}
