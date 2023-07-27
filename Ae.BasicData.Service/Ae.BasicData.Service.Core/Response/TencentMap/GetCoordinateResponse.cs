using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BasicData.Service.Core.Response
{
    public class GetCoordinateResponse
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
