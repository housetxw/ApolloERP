using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class GetShopSimpleInfoByIdsRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public List<long> ShopIds { get; set; }
    }
}
