using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Dal.Model.Extend
{
    public class ReplyCountDo
    {
        public long ParentId { get; set; }

        public int Count { get; set; }
    }
}
