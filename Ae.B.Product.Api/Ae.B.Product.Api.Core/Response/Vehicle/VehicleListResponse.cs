using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Response.Vehicle
{
    /// <summary>
    /// 品牌查车系
    /// </summary>
    public class VehicleListResponse
    {
        /// <summary>
        /// 车系Id
        /// </summary>
        public string VehicleId { get; set; }

        /// <summary>
        /// 车系（EG:A4L-一汽大众奥迪）
        /// </summary>
        public string Vehicle { get; set; }
    }
}
