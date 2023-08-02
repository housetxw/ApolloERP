using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model
{
    /// <summary>
    /// 套餐信息
    /// </summary>
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
}
