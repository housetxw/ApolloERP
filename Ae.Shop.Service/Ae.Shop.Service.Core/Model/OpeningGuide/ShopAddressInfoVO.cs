using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model.OpeningGuide
{
    public class ShopAddressInfoVO
    {
        public string UpdateBy { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string ProvinceId { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string CityId { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        public string DistrictId { get; set; }
        /// <summary>
        /// 省名称
        /// </summary>
        public string Province { get; set; } = string.Empty;
        /// <summary>
        /// 市名称
        /// </summary>
        public string City { get; set; } = string.Empty;
        /// <summary>
        /// 区名称
        /// </summary>
        public string District { get; set; } = string.Empty;
        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; } = string.Empty;
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
