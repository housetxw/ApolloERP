using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Client.Request
{
    public class ShopScoreClientRequest
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        public List<long> ShopIdList { get; set; }
    }
}
