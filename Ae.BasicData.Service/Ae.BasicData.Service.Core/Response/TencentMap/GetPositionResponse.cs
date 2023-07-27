using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BasicData.Service.Core.Response
{
    public class GetPositionResponse
    {
        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        public string District { get; set; }
        /// <summary>
        /// 街道
        /// </summary>
        public string StreetNumber { get; set; }
        /// <summary>
        /// 省ID
        /// </summary>
        public string ProvinceId { get; set; }
        /// <summary>
        /// 市ID
        /// </summary>
        public string CityId { get; set; }
        /// <summary>
        /// 区Id
        /// </summary>
        public string DistrictId { get; set; }
    }
}
