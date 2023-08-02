using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Response.Vehicle
{
    /// <summary>
    /// 年份查款型
    /// </summary>
    public class VehicleSalesNameByNianResponse
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
