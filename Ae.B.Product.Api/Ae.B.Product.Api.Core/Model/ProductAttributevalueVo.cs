using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Model
{
    /// <summary>
    /// 商品属性
    /// </summary>
    public class ProductAttributevalueVo
    {
        /// <summary>
        /// 从表Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 商品
        /// </summary>
        public string ProductId { get; set; }

        /// 属性名id
        /// </summary>
        public int AttributeNameId { get; set; }
        /// 属性名
        /// </summary>
        public string AttributeName { get; set; }
        /// <summary>
        /// 商品属性值
        /// </summary>
        public string AttributeValue { get; set; }

        /// <summary>
        /// 是否 删除 0 否  1是  
        /// </summary>
        public sbyte IsDeleted { get; set; } = 0;

        /// <summary>
        /// 属性显示名称
        /// </summary>
        public string AttributeDisplayName { get; set; }
        /// <summary>
        /// 属性值
        /// </summary>
        public string AttributeValues { get; set; }
        /// <summary>
        /// 控件类型
        /// </summary>
        public string ControlType { get; set; }
        /// <summary>
        /// 属性类型
        /// </summary>
        public string DataType { get; set; }
        /// <summary>
        /// 属性类型
        /// </summary>
        public string MinValues { get; set; }
        /// <summary>
        /// 属性类型
        /// </summary>
        public string MaxValues { get; set; }
        /// <summary>
        /// 属性类型
        /// </summary>
        public string MaxLength { get; set; }
        /// <summary>
        /// 是否必填
        /// </summary>
        public string IsRequired { get; set; }
    }
}
