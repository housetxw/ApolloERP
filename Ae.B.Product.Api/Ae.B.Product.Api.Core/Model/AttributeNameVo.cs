using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Model
{
    public class AttributeNameVo
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 属性名称
        /// </summary>
        public string AttributeName { get; set; } = string.Empty;
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// 控件类型（0 文本框  1 radio  2 单选  3 多选 ）
        /// </summary>
        public int ControlType { get; set; }
        /// <summary>
        /// 数据类型 0 文本 1 数值 
        /// </summary>
        public int DataType { get; set; }
        /// <summary>
        /// 最小Int值
        /// </summary>
        public int MinValue { get; set; }
        /// <summary>
        /// 最大Int值
        /// </summary>
        public int MaxValue { get; set; }
        /// <summary>
        /// 字符串长度
        /// </summary>
        public int MaxLength { get; set; }
        /// <summary>
        /// 是否必填
        /// </summary>
        public sbyte IsRequired { get; set; }
        /// <summary>
        /// 标记删除
        /// </summary>
        public sbyte IsDeleted { get; set; }

        /// <summary>
        /// 属性类型 0 通用属性 1 普通属性  2 销售属性 
        /// </summary>
        public int AttributeType { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int Sort { get; set; }
    }
}
