using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Vehicle.Service.Core.Request
{
    /// <summary>
    /// 根据品牌查车系请求实体
    /// </summary>
    public class GetVehicleListByBrandRequest
    {
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }
    }
}
