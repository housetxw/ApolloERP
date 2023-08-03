using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Client.Response.Activity
{
    public class GetUserRefersResponse
    {
        public  string Id { get; set; }

        public string UserName { get; set; }

        public sbyte ReferrerType { get; set; }

        public string ReferrerUserId { get; set; }

        public string ReferrenUserName { get; set; }

        public int ReferrerShopId { get; set; }

        public sbyte Channel { get; set; }
    }
}
