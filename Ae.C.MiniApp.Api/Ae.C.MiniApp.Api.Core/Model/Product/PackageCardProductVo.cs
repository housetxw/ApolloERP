using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model.Product
{
    /// <summary>
    /// PackageCardProductVo
    /// </summary>
    public class PackageCardProductVo
    {
        /// <summary>
        ///  产品编码
        /// </summary>
        public string ProductCode { get; set; } = string.Empty;

        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 产品显示名称
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; } = string.Empty;

        /// <summary>
        /// 基准价格
        /// </summary>
        public decimal StandardPrice { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SalesPrice { get; set; }

        /// <summary>
        ///  单位
        /// </summary>
        public string Unit { get; set; } = string.Empty;

        /// <summary>
        /// 主图连接
        /// </summary>
        public string Image1 { get; set; } = string.Empty;

        /// <summary>
        /// 广告促销语
        /// </summary>
        public string Advertisement { get; set; } = string.Empty;
    }
}
