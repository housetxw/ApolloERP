using Ae.Shop.Api.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Request
{
    /// <summary>
    /// 评论明细
    /// </summary>
    public class ReplyDetailClientRequest : BaseGetRequest
    {
        /// <summary>
        /// 评论Id
        /// </summary>
        public long CommentId { get; set; }

        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }
    }
}
