using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Client.Response
{
    public class GetCoordinateClientResponse
    {
        /// <summary>
        /// 经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        /// 区Id
        /// </summary>
        public string DistrictId { get; set; }
    }
}
