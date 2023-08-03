using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Response.Shop
{
    public class GetShopServiceTypeAsyncResponse
    {
        public int ShopServiceTypeId { get; set; }

        public string ImgeUrl { get; set; }

        public string Description { get; set; }

        public string DisplayName { get; set; }

        public string ServiceType { get; set; }

        public string RouteUrl { get; set; }
    }
}
