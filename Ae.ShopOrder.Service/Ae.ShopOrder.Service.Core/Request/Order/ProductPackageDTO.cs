﻿using Ae.ShopOrder.Service.Core.Model.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Order
{
    /// <summary>
    /// 套餐信息
    /// </summary>
    public class ProductPackageDTO
    {
        /// <summary>
        ///  套餐信息
        /// </summary>
        public PackageInfoDTO PackageInfo { get; set; }

        /// <summary>
        /// 套餐明细
        /// </summary>

        public List<ProductPackageDetailDTO> Details { get; set; }
    }
}
