using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Request
{
    public class GetProductCommentTotal
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public List<string> ProductIds { get; set; }
    }
}
