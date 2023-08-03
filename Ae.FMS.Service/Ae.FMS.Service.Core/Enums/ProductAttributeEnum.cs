using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Core.Enums
{
    /// <summary>
    /// 产品属性
    /// </summary>
    public enum ProductAttributeEnum
    {
        /// <summary>
        /// 0实物产品
        /// </summary>
        RealProduct = 0,
        /// <summary>
        /// 套餐产品
        /// </summary>
        PackageProduct = 1,
        /// <summary>
        /// 服务产品
        /// </summary>
        ServiceProduct = 2,
        /// <summary>
        /// 数字产品
        /// </summary>
        DigitProduct = 3,
        /// <summary>
        /// 项目产品
        /// </summary>
        ItemProduct=4,
        /// <summary>
        /// 门店外采
        /// </summary>
        ShopPurchase = 5

    }
}
