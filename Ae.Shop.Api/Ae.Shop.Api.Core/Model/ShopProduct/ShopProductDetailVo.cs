using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model.ShopProduct
{
    public class ShopProductDetailVo
    {
        /// <summary>
        /// 商品Pid
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 商品型号
        /// </summary>
        public string Specification { get; set; }

        /// <summary>
        /// 产品类目
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 类目Id
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SalesPrice { get; set; }

        /// <summary>
        /// 采购单价
        /// </summary>
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 原厂编号
        /// </summary>
        public string OeNumber { get; set; }

        public long ShopId { get; set; }
    }
}
