using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Request.ShopManage
{
    public class GetShopListByIdsRequest
    {
        /// <summary>
        /// 门店id
        /// </summary>
        public List<long> ShopIds { get; set; }
    }
}
