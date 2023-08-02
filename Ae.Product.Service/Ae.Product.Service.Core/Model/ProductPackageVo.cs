using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Model
{
    /// <summary>
    /// 套餐信息
    /// </summary>
    public class ProductPackageVo
    {
        /// <summary>
        ///  套餐信息
        /// </summary>
        public PackageInfoVo PackageInfo { get; set; }

        /// <summary>
        /// 套餐明细
        /// </summary>

        public List<ProductPackageDetailVo> Details { get; set; }
    }
}
