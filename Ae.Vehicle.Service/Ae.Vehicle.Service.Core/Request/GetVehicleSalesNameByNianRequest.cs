using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Vehicle.Service.Core.Request
{
    /// <summary>
    /// 根据生产年份查询款型
    /// </summary>
    public class GetVehicleSalesNameByNianRequest
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
