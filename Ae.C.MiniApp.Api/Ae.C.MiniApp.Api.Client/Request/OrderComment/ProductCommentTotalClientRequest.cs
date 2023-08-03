using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request.OrderComment
{
    public class ProductCommentTotalClientRequest
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public List<string> ProductIds { get; set; }
    }
}
