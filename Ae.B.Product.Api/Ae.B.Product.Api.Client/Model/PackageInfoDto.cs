using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model
{
    // <summary>
    /// 套餐信息
    /// </summary>
    public class PackageInfoDto
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
        public decimal StandardPrice { get; set; }
        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SalesPrice { get; set; }
    }
}
