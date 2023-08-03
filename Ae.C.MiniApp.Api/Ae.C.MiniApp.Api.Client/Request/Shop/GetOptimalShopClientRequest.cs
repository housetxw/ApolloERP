using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request
{
    public class GetOptimalShopClientRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// 市ID
        /// </summary>
        public string CityId { get; set; }
    }
}
