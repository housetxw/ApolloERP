using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Request
{
    public class GetCommentImageRequest
    {
        public List<long> CommentIds { get; set; }

        public int RelationType { get; set; } = 1;

    }
}
