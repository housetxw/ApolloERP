using Ae.Shop.Api.Core.Model.ShopCost;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Response.ShopCost
{
    public class GetShopCostTypeListResponse
    {
        public List<ShopCostTypeVO> TypeList { get; set; }
    }
}
