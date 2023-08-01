using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Vehicle.Service.Core.Request
{
    /// <summary>
    /// 获取生产年份请求实体
    /// </summary>
    public class GetVehicleNianByPaiLiangRequest
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
