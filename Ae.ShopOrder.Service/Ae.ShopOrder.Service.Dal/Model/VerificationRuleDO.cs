using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ae.ShopOrder.Service.Dal.Model
{
    [Table("verification_rule")]
    public class VerificationRuleDO
    {
        /// <summary>
        /// 自增主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 规则名称
        /// </summary>
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 是否有效（0无效 1有效）
        /// </summary>
        [Column("is_valid")]
        public sbyte IsValid { get; set; }
        /// <summary>
        /// 是否按产品Id
        /// </summary>
        [Column("is_by_pid")]
        public sbyte IsByPid { get; set; }
        /// <summary>
        /// 是否按门店类型
        /// </summary>
        [Column("is_by_shop_type")]
        public sbyte IsByShopType { get; set; }
        /// <summary>
        /// 适用门店类型（位或运算结果）
        /// </summary>
        [Column("shop_type")]
        public int ShopType { get; set; }
        /// <summary>
        /// 是否按门店Id
        /// </summary>
        [Column("is_by_shop_id")]
        public sbyte IsByShopId { get; set; }
        /// <summary>
        /// 使用规则描述
        /// </summary>
        [Column("use_rule_desc")]
        public string UseRuleDesc { get; set; } = string.Empty;
        /// <summary>
        /// 有效期开始类型（0未设置 1领取当天生效 2指定开始日期）
        /// </summary>
        [Column("valid_start_type")]
        public sbyte ValidStartType { get; set; }
        /// <summary>
        /// 有效期结束类型（0未设置 1持续指定天数 2指定截止日期）
        /// </summary>
        [Column("valid_end_type")]
        public sbyte ValidEndType { get; set; }
        /// <summary>
        /// 有效时长天数
        /// </summary>
        [Column("valid_days")]
        public int ValidDays { get; set; }
        /// <summary>
        /// 最早使用日期
        /// </summary>
        [Column("earliest_use_date")]
        public DateTime EarliestUseDate { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 最晚使用日期
        /// </summary>
        [Column("latest_use_date")]
        public DateTime LatestUseDate { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 删除标识
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
        /// 修改人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
