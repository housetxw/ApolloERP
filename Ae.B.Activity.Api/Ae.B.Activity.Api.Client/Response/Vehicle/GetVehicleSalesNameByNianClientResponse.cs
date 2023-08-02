using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Activity.Api.Client.Response.Vehicle
{
   public class GetVehicleSalesNameByNianClientResponse
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
