using Ae.C.MiniApp.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response
{
    public class AppendCommentResponse
    {
        /// <summary>
        /// 商品集合
        /// </summary>
        public List<OrderCommentDetailProductFormVO> Products { get; set; }
        /// <summary>
        /// 点评ID
        /// </summary>
        public long CommentId { get; set; }
        /// <summary>
        /// 点评内容
        /// </summary>
        public string CommentContent { get; set; }
        /// <summary>
        /// 点评图片
        /// </summary>
        public List<string> Imgs { get; set; }
        /// <summary>
        /// 点评时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 服务车型
        /// </summary>
        public string CarName { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; }
        /// <summary>
        /// 被点赞喜欢数
        /// </summary>
        public int LikeNum { get; set; }
    }
}
