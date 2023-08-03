using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Response.Vehicle
{
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
