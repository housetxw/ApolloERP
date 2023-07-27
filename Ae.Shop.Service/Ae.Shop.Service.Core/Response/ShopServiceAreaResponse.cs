using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Response
{
    /// <summary>
    /// 门店区域配置
    /// </summary>
    public class ShopServiceAreaResponse
    {
        /// <summary>
        /// 三级省市区
        /// </summary>
        public List<ShopServiceRegion> Regions { get; set; }
    }

    /// <summary>
    /// 三级省市区
    /// </summary>
    public class ShopServiceRegion
    {
        /// <summary>
        /// 
        /// </summary>
        public string ProvinceId { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string CityId { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string DistrictId { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string Province { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string District { get; set; } = string.Empty;
    }
}
