using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;

namespace Ae.OrderComment.Service.Dal.Model
{
    [Table("comment_reply")]
    public class CommentReplyDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 回复id
        /// </summary>
        [Column("reply_id")]
        public long ReplyId { get; set; }
        /// <summary>
        /// 若为回复，回复方类型（0未设置 1门店商家 2官方客服 3:用户）
        /// </summary>
        [Column("reply_part_type")]
        public sbyte ReplyPartType { get; set; }
        /// <summary>
        /// 回复类型（0未设置  1回复点评 2客户追评 3回复追评）
        /// </summary>
        [Column("reply_type")]
        public sbyte ReplyType { get; set; }
        /// <summary>
        /// 门店Id
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        [Column("shop_name")]
        public string ShopName { get; set; } = string.Empty;
        /// <summary>
        /// 渠道（0未设置 1平台C端 2平台门店）
        /// </summary>
        [Column("channel")]
        public sbyte Channel { get; set; }
        /// <summary>
        /// 订单类型（0未设置 1轮胎 2保养 3美容）
        /// </summary>
        [Column("order_type")]
        public sbyte OrderType { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        [Column("order_no")]
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 审核状态（0待审核 1审核通过 2审核不通过）
        /// </summary>
        [Column("check_status")]
        public sbyte CheckStatus { get; set; }
        /// <summary>
        /// 审核意见
        /// </summary>
        [Column("check_comment")]
        public string CheckComment { get; set; } = string.Empty;
        /// <summary>
        /// 审核时间
        /// </summary>
        [Column("check_time")]
        public DateTime CheckTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 手写评价内容
        /// </summary>
        [Column("content")]
        public string Content { get; set; } = string.Empty;
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
