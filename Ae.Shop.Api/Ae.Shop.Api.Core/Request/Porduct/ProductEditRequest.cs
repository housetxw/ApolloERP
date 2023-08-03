using Ae.Shop.Api.Core.Model.ShopProduct;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request.Porduct
{
    /// <summary>
    /// ProductEditRequest
    /// </summary>
    public class ProductEditRequest
    {
        /// <summary>
        /// 商品信息
        /// </summary>
        public ProductAllInfoVo Product { get; set; }

        /// <summary>
        /// 商品属性 List
        /// </summary>
        public List<ProductAttributeValueVo> Attributevalues { get; set; }

        /// <summary>
        /// 套餐明细 List
        /// </summary>
        public List<ProductPackageDetailVo> PackageDetails { get; set; }

        /// <summary>
        /// 是否编辑
        /// </summary>
        public bool IsEdit { get; set; }
    }
}
