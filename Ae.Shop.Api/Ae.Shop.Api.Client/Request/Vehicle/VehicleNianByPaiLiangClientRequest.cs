using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Request.Vehicle
{
    /// <summary>
    /// 
    /// </summary>
    public class VehicleNianByPaiLiangClientRequest
    {
        /// <summary>
        /// 车系Id
        /// </summary>
        public string VehicleId { get; set; }

        /// <summary>
        /// 排量
        /// </summary>
        public string PaiLiang { get; set; }
    }
}
