using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Vehicle.Service.Core.Request
{
    /// <summary>
    /// 查排量请求实体
    /// </summary>
    public class GetPaiLiangByVehicleIdRequest
    {
        /// <summary>
        /// 车系Id
        /// </summary>
        public string VehicleId { get; set; }
    }
}
