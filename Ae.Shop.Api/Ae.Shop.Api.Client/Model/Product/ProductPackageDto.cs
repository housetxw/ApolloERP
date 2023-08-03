using Ae.Shop.Api.Client.Response.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Model.Product
{
    public class ProductPackageDto
    {
        /// <summary>
        ///  套餐信息
        /// </summary>
        public PackageInfoDto PackageInfo { get; set; }

        /// <summary>
        /// 套餐明细
        /// </summary>

        public List<ProductPackageDetailDto> Details { get; set; }
    }

    /// <summary>
    /// PackageInfoDto
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
