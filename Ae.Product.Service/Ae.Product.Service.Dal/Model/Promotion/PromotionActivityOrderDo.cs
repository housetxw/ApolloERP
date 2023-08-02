using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Dal.Model.Promotion
{
    [Table("promotion_activity_order")]
    public class PromotionActivityOrderDo
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 活动Id
        /// </summary>
        [Column("activity_id")]
        public Guid ActivityId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        [Column("order_no")]
        public string OrderNo { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        [Column("user_id")]
        public Guid UserId { get; set; }
        /// <summary>
        /// 商品Pid
        /// </summary>
        [Column("product_code")]
        public string ProductCode { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [Column("num")]
        public int Num { get; set; }
        /// <summary>
        /// 订单状态0 有效  1取消
        /// </summary>
        [Column("order_status")]
        public int OrderStatus { get; set; }
        /// <summary>
        /// 删除
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
