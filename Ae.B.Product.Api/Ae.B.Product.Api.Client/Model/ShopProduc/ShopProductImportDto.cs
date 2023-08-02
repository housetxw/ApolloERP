using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model.ShopProduc
{
    public class ShopProductImportDto
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        public int ShopId { get; set; }
        /// <summary>
        /// 类目Id
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 小程序显示名称
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// 产品描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SalesPrice { get; set; }

        /// <summary>
        /// 缩率图
        /// </summary>
        public string Icon { get; set; } = string.Empty;
    }
}
