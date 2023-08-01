using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Dal.Model
{
    [Table("bao_yang_property_adaptation")]
    public class BaoYangPropertyAdaptationDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 五级车型tid
        /// </summary>
        [Column("tid")]
        public string Tid { get; set; }

        /// <summary>
        /// 配件类型
        /// </summary>
        [Column("part_name")]
        public string PartName { get; set; } = string.Empty;

        /// <summary>
        /// 配件类型code
        /// </summary>
        [Column("part_name_abbr")]
        public string PartNameAbbr { get; set; } = string.Empty;

        /// <summary>
        /// 五级属性
        /// </summary>
        [Column("property")]
        public string Property { get; set; } = string.Empty;

        /// <summary>
        /// 五级属性值
        /// </summary>
        [Column("property_value")]
        public string PropertyValue { get; set; } = string.Empty;

        /// <summary>
        /// 图片地址
        /// </summary>
        [Column("image_url")]
        public string ImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// oe号
        /// </summary>
        [Column("oe_part_code")]
        public string OePartCode { get; set; } = string.Empty;

        /// <summary>
        /// 配件号
        /// </summary>
        [Column("part_code")]
        public string PartCode { get; set; } = string.Empty;

        /// <summary>
        /// 标记删除
        /// </summary>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; }

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
    }
}
