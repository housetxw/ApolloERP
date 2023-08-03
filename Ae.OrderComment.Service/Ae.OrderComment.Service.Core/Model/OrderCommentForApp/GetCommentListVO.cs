using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Model.OrderCommentForApp
{
    public class GetCommentListVO
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
        public string ShowCreateTime => CreateTime.ToString("yyyy-MM-dd HH:mm:dd");
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

        /// <summary>
        /// 回复按钮集合
        /// </summary>
        public List<UserOperationVO> ReplyButtons { get; set; }
    }
}
