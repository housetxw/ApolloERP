using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Client.Request
{
    public class GetOrderCountByShopIdClientRequest
    {
        /// <summary>
        /// 门店ID集合
        /// </summary>
        public List<long> ShopIds { get; set; }
    }
}
