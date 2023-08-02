using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client
{
    public class ProductAttributeImportDto
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        public string ProductId { get; set; }
        /// 属性名
        /// </summary>
        public string AttributeName { get; set; }
        /// 属性名
        /// </summary>
        public int AttributeNameId { get; set; }
        /// <summary>
        /// 商品属性值
        /// </summary>
        public string AttributeValue { get; set; }
    }
}
