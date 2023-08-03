using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.BaoYang
{
    /// <summary>
    /// AdditionalPriceByServiceIdRequest
    /// </summary>
    public class AdditionalPriceByServiceIdRequest
    {
   
        public List<string> ServiceId { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        public VehicleRequest Vehicle { get; set; }
    }
}
