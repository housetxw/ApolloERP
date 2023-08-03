using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ae.ConsumerOrder.Service.Dal.Model.SharingPromotin
{
    [Table("order_user_share_detail")]
    public class OrderUserShareDetailDO
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 源用户Id
        /// </summary>
        [Column("source_user_id")]
        public string SourceUserId { get; set; } = string.Empty;
        /// <summary>
        /// 订单号（RGC或RGS前缀）
        /// </summary>
        [Column("order_no")]
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 创建用户Id
        /// </summary>
        [Column("destination_user_id")]
        public string DestinationUserId { get; set; } = string.Empty;
        /// <summary>
        /// 兑换状态(0：未兑换 1：已兑换 ）
        /// </summary>
        [Column("exchange_status")]
        public ushort ExchangeStatus { get; set; }
        /// <summary>
        /// 兑换优惠卷Id
        /// </summary>
        [Column("coupon_id")]
        public long CouponId { get; set; }
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
