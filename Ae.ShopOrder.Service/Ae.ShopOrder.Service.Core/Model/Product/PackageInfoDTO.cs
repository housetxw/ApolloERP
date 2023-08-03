using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Model.Product
{
    /// <summary>
    /// 套餐ixnx
    /// </summary>
    public class PackageInfoDTO
    {
        /// <summary>
        ///  套餐Id
        /// </summary>
        public string PackageId { get; set; }

        /// <summary>
        ///  产品编码
        /// </summary>
        public string ProductCode { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 基准价格
        /// </summary>
        public decimal ListPrice { get; set; }
        /// <summary>
        /// 销售价
        /// </summary>
        public decimal MarketingPrice { get; set; }
    }
}
