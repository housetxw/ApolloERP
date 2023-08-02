using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Dal.Model.Promotion
{
    [Table("promotion_activity_product")]
    public class PromotionActivityProductDo
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
        /// 商品Id
        /// </summary>
        [Column("product_code")]
        public string ProductCode { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        [Column("product_name")]
        public string ProductName { get; set; }
        /// <summary>
        /// 促销价
        /// </summary>
        [Column("promotion_price")]
        public decimal PromotionPrice { get; set; }
        /// <summary>
        /// 限购数量
        /// </summary>
        [Column("limit_quantity")]
        public int LimitQuantity { get; set; }
        /// <summary>
        /// 已售数量
        /// </summary>
        [Column("sold_quantity")]
        public int SoldQuantity { get; set; }
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
