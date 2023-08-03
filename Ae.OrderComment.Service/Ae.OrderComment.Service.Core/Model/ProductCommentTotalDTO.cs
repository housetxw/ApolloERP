using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Model
{
    public class ProductCommentTotalDTO
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// 评论数量
        /// </summary>
        public int CommentCount { get; set; }
    }
}
