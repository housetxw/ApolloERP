using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Vehicle.Service.Core.Response
{
    /// <summary>
    /// 根据生产年份查询款型返回实体
    /// </summary>
    public class GetVehicleSalesNameByNianResponse
    {
        /// <summary>
        /// 五级车型tid
        /// </summary>
        public string Tid { get; set; }

        /// <summary>
        /// 款型
        /// </summary>
        public string SalesName { get; set; }
    }
}
