using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Dal.Model.ReceiveCheck
{
    [Table("shop_receive_sub_item_compute_rule")]
    public class ShopReceiveSubItemComputeRuleDo
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 规则code
        /// </summary>
        [Column("code")]
        public string Code { get; set; }
        /// <summary>
        /// 规则名
        /// </summary>
        [Column("ruleDes")]
        public string RuleDes { get; set; }
        /// <summary>
        /// 最小
        /// </summary>
        [Column("min_value")]
        public decimal MinValue { get; set; }
        /// <summary>
        /// 最大
        /// </summary>
        [Column("max_value")]
        public decimal MaxValue { get; set; }
        /// <summary>
        /// 检查结果项id
        /// </summary>
        [Column("sub_item_id")]
        public long SubItemId { get; set; }
        /// <summary>
        /// 建议
        /// </summary>
        [Column("suggestion")]
        public string Suggestion { get; set; }
        /// <summary>
        /// 删除
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }
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
        /// 更新
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; }
    }
}
