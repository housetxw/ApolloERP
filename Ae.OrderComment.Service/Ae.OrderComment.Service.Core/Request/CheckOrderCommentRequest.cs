using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Request
{
   public class CheckOrderCommentRequest
    {
        public long CommentId { get; set; }

        public string CheckComment { get; set; }

        public sbyte CheckStatus { get; set; }

        public string UpdateBy { get; set; }

    }
}
