using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.OrderComment.Service.Dal.Model
{
    [Table("comment_detail_product")]
    public class CommentDetailProductDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 评论Id
        /// </summary>
        [Column("comment_id")]
        public long CommentId { get; set; }
        /// <summary>
        /// 订单商品Id
        /// </summary>
        [Column("order_product_id")]
        public long OrderProductId { get; set; }
        /// <summary>
        /// 商品Id
        /// </summary>
        [Column("product_id")]
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 商品显示名称
        /// </summary>
        [Column("product_display_name")]
        public string ProductDisplayName { get; set; } = string.Empty;
        /// <summary>
        /// 商品图片
        /// </summary>
        [Column("product_image_url")]
        public string ProductImageUrl { get; set; } = string.Empty;
        /// <summary>
        /// 商品价格
        /// </summary>
        [Column("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        [Column("number")]
        public int Number { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [Column("unit")]
        public string Unit { get; set; } = string.Empty;
        /// <summary>
        /// 五级分值
        /// </summary>
        [Column("score")]
        public int Score { get; set; }
        /// <summary>
        /// 是否匿名
        /// </summary>
        [Column("is_anonymous")]
        public sbyte IsAnonymous { get; set; }
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