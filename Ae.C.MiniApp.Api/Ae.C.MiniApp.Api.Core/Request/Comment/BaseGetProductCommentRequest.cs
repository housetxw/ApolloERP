using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
    public class BaseGetProductCommentRequest : BaseGetRequest
    {
        /// <summary>
        /// 产品Id
        /// </summary>
        public string ProductId { get; set; }
    }
}
