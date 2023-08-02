using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Activity.Api.Core.Request
{
 public   class GetVehicleNianByPaiLiangRequest
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
