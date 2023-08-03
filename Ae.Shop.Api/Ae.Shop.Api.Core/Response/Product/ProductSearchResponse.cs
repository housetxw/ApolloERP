using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Response.Product
{
    public class ProductSearchResponse
    {
        /// <summary>
        /// 商品信息
        /// </summary>
        public List<ProductAllInfoVo> Items { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalItems { get; set; }

    }

    public class ProductAllInfoVo
    {
        /// <summary>
        ///  产品编码
        /// </summary>
        public string ProductCode { get; set; } = string.Empty;
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;


        /// <summary>
        /// 市场价格
        /// </summary>
        public decimal StandardPrice { get; set; }
        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SalesPrice { get; set; }
        /// <summary>
        /// 结算成本价
        /// </summary>
        public decimal CostPrice { get; set; } = 0;
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; } = string.Empty;

        /// <summary>
        /// 类目名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        ///  是否上架  0 否 1 是
        /// </summary>
        public int IsOnSale { get; set; }
    }
}
