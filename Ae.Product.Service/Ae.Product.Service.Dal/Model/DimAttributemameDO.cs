using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Dal.Model
{

    [Table("dim_attributemame")]
    public class DimAttributemameDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 属性名称
        /// </summary>
        [Column("attribute_name")]
        public string AttributeName { get; set; } = string.Empty;
        /// <summary>
        /// 显示名称
        /// </summary>
        [Column("display_name")]
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        [Column("Description")]
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// 控件类型（0 文本框  1 radio  2 单选  3 多选 ）
        /// </summary>
        [Column("control_type")]
        public int ControlType { get; set; }
        /// <summary>
        /// 数据类型 0 文本 1 数值 
        /// </summary>
        [Column("data_type")]
        public int DataType { get; set; }
        /// <summary>
        /// 最小Int值
        /// </summary>
        [Column("min_value")]
        public int MinValue { get; set; }
        /// <summary>
        /// 最大Int值
        /// </summary>
        [Column("max_value")]
        public long MaxValue { get; set; }
        /// <summary>
        /// 字符串长度
        /// </summary>
        [Column("max_length")]
        public int MaxLength { get; set; }
        /// <summary>
        /// 是否必填
        /// </summary>
        [Column("is_required")]
        public sbyte IsRequired { get; set; }
        /// <summary>
        /// 标记删除
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 更新人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 属性类型 0 通用属性 1 普通属性  2 销售属性 
        /// </summary>
        [Column("attribute_type")]
        public int AttributeType { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        [Column("sort")]
        public int Sort { get; set; }
    }

}
