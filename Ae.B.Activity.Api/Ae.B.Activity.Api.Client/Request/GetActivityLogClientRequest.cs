using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Activity.Api.Client.Request
{
    public class GetActivityLogClientRequest
    {
        public long Id { get; set; }
        public string ObjectId { get; set; }

        public string ObjectType { get; set; }
    }
}
