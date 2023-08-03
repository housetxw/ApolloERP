using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Response.Order
{
    public class GetShopOccupyMappingsByRelationIdResponse
    {
        public long ShopId { get; set; }

        public string ShopName { get; set; }

       public long RelationShopId { get; set; }

        public string RelationShopName { get; set; }


    }
}
