using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Activity.Api.Client.Request.Vehicle
{
   public class GetVehicleSalesNameByNianClientRequest
    {
        /// <summary>
        /// 车系
        /// </summary>
        public string VehicleId { get; set; }

        /// <summary>
        /// 排量
        /// </summary>
        public string PaiLiang { get; set; }

        /// <summary>
        /// 生产年份
        /// </summary>
        public string Nian { get; set; }

    }
}
