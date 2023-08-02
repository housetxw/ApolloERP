using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Activity.Api.Core.Response
{
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
