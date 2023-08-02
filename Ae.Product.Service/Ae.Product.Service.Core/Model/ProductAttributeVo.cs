using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Model
{
    /// <summary>
    /// ProductAttributeVo
    /// </summary>
    public class ProductAttributeVo
    {
        /// <summary>
        /// 产品编码
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 商品属性 List
        /// </summary>
        public List<ProductAttributevalueVo> AttributeValues { get; set; } = new List<ProductAttributevalueVo>();
    }
}
