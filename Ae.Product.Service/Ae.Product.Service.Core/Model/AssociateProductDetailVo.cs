using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Model
{
    public class AssociateProductDetailVo
    {
        /// <summary>
        /// 商品编码
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 销售价格
        /// </summary>
        public decimal SalesPrice { get; set; }

        /// <summary>
        /// 属性名称
        /// </summary>
        public string AttributeName { get; set; }

        /// <summary>
        /// 属性显示名称
        /// </summary>
        public string AttributeDisplayName { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        public string AttributeValue { get; set; }
    }
}
