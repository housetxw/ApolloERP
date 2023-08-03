using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ae.ConsumerOrder.Service.Dal.Model.SharingPromotin
{
    [Table("order_user_share")]
    public class OrderUserShareDO
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        [Column("user_id")]
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// 下单人姓名
        /// </summary>
        [Column("user_name")]
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// 分享有效总订单数量
        /// </summary>
        [Column("share_valid_sum_order_num")]
        public int ShareValidSumOrderNum { get; set; }
        /// <summary>
        /// 可兑换订单数量
        /// </summary>
        [Column("exchange_sum_order_num")]
        public int ExchangeSumOrderNum { get; set; }
        /// <summary>
        /// 已经兑换得优惠卷得数量
        /// </summary>
        [Column("exchanged_coupon_num")]
        public int ExchangedCouponNum { get; set; }
        /// <summary>
        /// 删除标识（0否 1是）
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
