using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class GetShopListByIdsRequest
    {
        /// <summary>
        /// 门店id
        /// </summary>
        public List<long> ShopIds { get; set; }
    }
}
