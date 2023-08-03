using Ae.Shop.Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Response
{
    /// <summary>
    /// 点评明细
    /// </summary>
    public class ReplyDetailResponse
    {
        /// <summary>
        /// 评论Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 评论Id
        /// </summary>
        public long CommentId => Id;
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
        /// 显示创建的时间
        /// </summary>
        public string ShowCreateTime => CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
        /// <summary>
        /// 创建时间
        /// </summary>

        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 门店评分值
        /// </summary>
        public int ShopScore { get; set; }

        /// <summary>
        /// 评分内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 图片地址集合
        /// </summary>
        public List<ImgVO> ImgList { get; set; }
        /// <summary>
        /// 评论回复集合
        /// </summary>
        public List<CommentListReplyInfoVO> ReplyInfos { get; set; }
    }

    public class ImgVO
    {
        /// <summary>
        /// 图片路径
        /// </summary>
        public string Url { get; set; }
    }

    /// <summary>
    /// 评论列表回复信息
    /// </summary>
    public class CommentListReplyInfoVO
    {
        /// <summary>
        /// 评论Id
        /// </summary>
        public long CommentId { get; set; }
        /// <summary>
        /// 点评类型（使用到的枚举值：ReplyComment CustomerAppendComment ReplyAppendComment）
        /// </summary>
        public CommentTypeEnum CommentType { get; set; }
        /// <summary>
        /// 显示标题，示例：商家回复（1天后）
        /// </summary>
        public string DisplayTitle { get; set; }
        /// <summary>
        /// 回复内容
        /// </summary>
        public string ReplyContent { get; set; }
    }
}
