using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;

namespace Ae.OrderComment.Service.Dal.Model
{
    public class GetCommentListDO
    {
        /// <summary>
        /// 主键
        /// </summary>

        public long Id { get; set; }

        /// <summary>
        /// 门店评价得分
        /// </summary>
        public int ShopScore { get; set; }
        /// <summary>
        /// 订单Id
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>

        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 渠道（0未设置 1平台C端 2平台门店）
        /// </summary>

        public sbyte Channel { get; set; }
        /// <summary>
        /// 订单类型（0未设置 1轮胎 2保养 3美容）
        /// </summary>

        public sbyte OrderType { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>

        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// 用户头像
        /// </summary>

        public string HeadUrl { get; set; } = string.Empty;
        /// <summary>
        /// 用户昵称
        /// </summary>

        public string NickName { get; set; } = string.Empty;
        /// <summary>
        /// 车辆Id
        /// </summary>

        public string CarId { get; set; } = string.Empty;
        /// <summary>
        /// 门店Id
        /// </summary>

        public long ShopId { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>

        public string ShopName { get; set; } = string.Empty;
        /// <summary>
        /// 手写评价内容
        /// </summary>

        public string Content { get; set; } = string.Empty;
        /// <summary>
        /// 是否匿名（客户选择，对技师展示时默认匿名不由此控制）
        /// </summary>

        public sbyte IsAnonymous { get; set; }
        /// <summary>
        /// 审核状态（0待审核 1审核通过 2审核不通过）
        /// </summary>

        public sbyte CheckStatus { get; set; }
        /// <summary>
        /// 审核意见
        /// </summary>

        public string CheckComment { get; set; } = string.Empty;
        /// <summary>
        /// 是否置顶
        /// </summary>

        public sbyte IsTop { get; set; }
        /// <summary>
        /// 是否精华点评
        /// </summary>

        public sbyte IsBest { get; set; }
        /// <summary>
        /// 被点赞喜欢数
        /// </summary>

        public int LikeNum { get; set; }
        /// <summary>
        /// 门店商家回复类型（0：未回复 1：商家回复 ）
        /// </summary>

        public sbyte ShopReplyType { get; set; }
        /// <summary>
        /// 客服官方回复（0：客服未回复 1：客服回复）
        /// </summary>

        public sbyte OfficeType { get; set; }
        /// <summary>
        /// 用户回复（0：客户未追评 1：客户追评论）
        /// </summary>

        public sbyte UserReplyType { get; set; }
        /// <summary>
        /// 删除标识
        /// </summary>

        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>

        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>

        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>

        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>

        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
