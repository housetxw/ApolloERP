using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Request.Shop
{
    /// <summary>
    /// 门店列表查询
    /// </summary>
    public class ShopListByIdsAsyncClientRequest
    {
        public List<long> ShopIds { get; set; }
    }
}
