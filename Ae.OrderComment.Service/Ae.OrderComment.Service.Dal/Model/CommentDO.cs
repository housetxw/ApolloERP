using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.OrderComment.Service.Dal.Model
{
    [Table("comment")]
    public class CommentDO
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
        /// 订单号
        /// </summary>
        [Column("order_no")]
        public string OrderNo { get; set; } = string.Empty;
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
        /// 用户Id
        /// </summary>
        [Column("user_id")]
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// 用户头像
        /// </summary>
        [Column("head_url")]
        public string HeadUrl { get; set; } = string.Empty;
        /// <summary>
        /// 用户昵称
        /// </summary>
        [Column("user_nick_name")]
        public string UserNickName { get; set; } = string.Empty;
        /// <summary>
        /// 车辆Id
        /// </summary>
        [Column("car_id")]
        public string CarId { get; set; } = string.Empty;

        /// <summary>
        /// 车系
        /// </summary>
        [Column("vehicle")]
        public string Vehicle { get; set; } = string.Empty;

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
        /// 手写评价内容
        /// </summary>
        [Column("content")]
        public string Content { get; set; } = string.Empty;
        /// <summary>
        /// 是否匿名（客户选择，对技师展示时默认匿名不由此控制）
        /// </summary>
        [Column("is_anonymous")]
        public sbyte IsAnonymous { get; set; }
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
        /// 是否置顶
        /// </summary>
        [Column("is_top")]
        public sbyte IsTop { get; set; }
        /// <summary>
        /// 是否精华点评
        /// </summary>
        [Column("is_best")]
        public sbyte IsBest { get; set; }
        /// <summary>
        /// 被点赞喜欢数
        /// </summary>
        [Column("like_num")]
        public int LikeNum { get; set; }
        /// <summary>
        /// 门店商家回复类型（0：未回复 1：商家回复 ）
        /// </summary>
        [Column("shop_reply_type")]
        public sbyte ShopReplyType { get; set; }
        /// <summary>
        /// 客服官方回复（0：客服未回复 1：客服回复）
        /// </summary>
        [Column("office_type")]
        public sbyte OfficeType { get; set; }
        /// <summary>
        /// 用户回复（0：客户未追评 1：客户追评论）
        /// </summary>
        [Column("user_reply_type")]
        public sbyte UserReplyType { get; set; }
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