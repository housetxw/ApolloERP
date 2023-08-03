using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Dal.Model
{
    public class BaseCommentListDO
    {
        /// <summary>
        /// 评论Id
        /// </summary>
        public long CommentId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadUrl { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 评分值
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// 评分内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; }
        /// <summary>
        /// 车ID
        /// </summary>
        public string CarId { get; set; }

        /// <summary>
        /// 服务车型
        /// </summary>
        public string CarName { get; set; }
        /// <summary>
        /// 被点赞喜欢数
        /// </summary>
        public int LikeNum { get; set; }
        /// <summary>
        /// 是否精华点评
        /// </summary>
        public bool IsBest { get; set; }

        /// <summary>
        /// 是否匿名
        /// </summary>
        public bool IsAnonymous { get; set; }
    }
}
