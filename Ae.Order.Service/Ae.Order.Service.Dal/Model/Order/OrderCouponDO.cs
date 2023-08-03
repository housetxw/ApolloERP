using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ae.Order.Service.Dal.Model.Order
{
    [Table("order_coupon")]
    public class OrderCouponDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 订单Id
        /// </summary>
        [Column("order_id")]
        public long OrderId { get; set; }
        /// <summary>
        /// 用户使用的优惠券Id
        /// </summary>
        [Column("user_coupon_id")]
        public long UserCouponId { get; set; }
        /// <summary>
        /// 使用规则Id
        /// </summary>
        [Column("coupon_rule_id")]
        public long CouponRuleId { get; set; }
        /// <summary>
        /// 优惠券标题名称
        /// </summary>
        [Column("coupon_name")]
        public string CouponName { get; set; } = string.Empty;
        /// <summary>
        /// 优惠券金额
        /// </summary>
        [Column("coupon_amount")]
        public decimal CouponAmount { get; set; }
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
