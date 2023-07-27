using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Client
{
    public class GetPositionClientRequest
    {
        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        public double Latitude { get; set; }
    }
}
