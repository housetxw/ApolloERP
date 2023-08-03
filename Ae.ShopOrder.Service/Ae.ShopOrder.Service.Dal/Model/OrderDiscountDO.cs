using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ae.ShopOrder.Service.Dal.Model
{
    [Table("order_discount")]
    public class OrderDiscountDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        [Column("order_no")]
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 实际金额
        /// </summary>
        [Column("actual_amount")]
        public decimal ActualAmount { get; set; }
        /// <summary>
        /// 折扣金额
        /// </summary>
        [Column("discount_amount")]
        public decimal DiscountAmount { get; set; }
        /// <summary>
        /// 折扣类型（0：未设置 1：积分会员）
        /// </summary>
        [Column("discount_type")]
        public sbyte DiscountType { get; set; }
        /// <summary>
        /// 折扣率
        /// </summary>
        [Column("discount_rate")]
        public float DiscountRate { get; set; }
        /// <summary>
        /// 计算公式
        /// </summary>
        [Column("formula")]
        public string Formula { get; set; } = string.Empty;
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
