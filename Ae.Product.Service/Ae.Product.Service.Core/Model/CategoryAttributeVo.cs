using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Model
{
    /// <summary>
    /// 类目属性
    /// </summary>
    public class CategoryAttributeVo
    {
        public int Id { get; set; }
        /// <summary>
        /// 属性名Id
        /// </summary>
        public int AttributeNameId { get; set; }
        /// <summary>
        /// 属性名
        /// </summary>
        public string AttributeName { get; set; }
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

        /// <summary>
        /// 标记删除
        /// </summary>
        public sbyte IsDeleted { get; set; }
    }
}
