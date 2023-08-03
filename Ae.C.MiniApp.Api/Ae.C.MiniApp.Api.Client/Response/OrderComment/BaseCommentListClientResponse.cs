using Ae.C.MiniApp.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response
{
    public class BaseCommentListClientResponse
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
        /// 会员等级Code
        /// </summary>
        public int MemberGrade { get; set; }
        /// <summary>
        /// 会员等级显示名称
        /// </summary>
        public string MemberLevelDisplayName { get; set; }
        /// <summary>
        /// 评分内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 图片地址集合
        /// </summary>
        public List<string> ImageUrls { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; }
        /// <summary>
        /// 服务车型
        /// </summary>
        public string CarName { get; set; }
        public string CarId { get; set; }
        /// <summary>
        /// 被点赞喜欢数
        /// </summary>
        public int LikeNum { get; set; }
        /// <summary>
        /// 是否精华点评
        /// </summary>
        public bool IsBest { get; set; }
        /// <summary>
        /// 评论回复集合
        /// </summary>
        public List<CommentListReplyInfoDTO> ReplyInfos { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int ProductNum { get; set; }
    }
}
