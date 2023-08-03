using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Request
{
   public class GetOrderCommentForProductRequest: GetOrderCommentBaseRequest
    {
        public string ProductName { get; set; } = string.Empty;

    }
}
