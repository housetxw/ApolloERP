using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Vehicle.Service.Dal.Model
{
    [Table("bao_yang_parts")]
    public class BaoYangPartsDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 五级车型Tid
        /// </summary>
        [Column("tid")]
        public string Tid { get; set; }
        /// <summary>
        /// 配件名称
        /// </summary>
        [Column("part_name")]
        public string PartName { get; set; }
        /// <summary>
        /// 配件号
        /// </summary>
        [Column("part_code")]
        public string PartCode { get; set; }
        /// <summary>
        /// OE号
        /// </summary>
        [Column("oe_part_code")]
        public string OePartCode { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        [Column("source")]
        public string Source { get; set; }
        /// <summary>
        /// 产品品牌
        /// </summary>
        [Column("brand")]
        public string Brand { get; set; }
        /// <summary>
        /// 是否通过验证
        /// </summary>
        [Column("validated")]
        public bool Validated { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        [Column("validated_by")]
        public string ValidatedBy { get; set; } = string.Empty;
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
        /// 验证时间
        /// </summary>
        [Column("validated_time")]
        public DateTime ValidatedTime { get; set; }
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
