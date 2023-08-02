using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Activity.Api.Client.Request.Vehicle
{
 public   class GetVehicleNianByPaiLiangClientRequest
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
