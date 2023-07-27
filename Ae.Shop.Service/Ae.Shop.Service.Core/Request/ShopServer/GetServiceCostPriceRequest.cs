using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class GetServiceCostPriceRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// PID集合
        /// </summary>
        public List<string> ProductIds { get; set; }
    }
}
