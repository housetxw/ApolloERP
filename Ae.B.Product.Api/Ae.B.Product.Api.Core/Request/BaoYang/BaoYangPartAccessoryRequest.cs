using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.BaoYang
{
    /// <summary>
    /// 
    /// </summary>
    public class BaoYangPartAccessoryRequest
    {
        /// <summary>
        /// 车系Id
        /// </summary>
        public string VehicleId { get; set; }

        /// <summary>
        /// 排量
        /// </summary>
        public string PaiLiang { get; set; }

        /// <summary>
        /// 年
        /// </summary>
        public string Nian { get; set; }

        /// <summary>
        /// 款型Id
        /// </summary>
        public string Tid { get; set; }
    }
}
