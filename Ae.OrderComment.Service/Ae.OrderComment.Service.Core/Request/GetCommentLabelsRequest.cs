using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Request
{
    public class GetCommentLabelsRequest
    {
        /// <summary>
        /// 评论明细类型（0未设置 1技师 2门店 3商品）
        /// </summary>
        public byte CommentDetailType { get; set; }
        /// <summary>
        /// 五级分值
        /// </summary>
        public int Score { get; set; }
    }
}
