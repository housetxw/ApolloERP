using Ae.Shop.Service.Core.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request.Shop
{
    /// <summary>
    /// 更新门店服务配置
    /// </summary>
    public class UpdateShopServiceAreaRequest
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        [Range(1, Int64.MaxValue, ErrorMessage = "ShopId必须大于0")]
        public int ShopId { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public List<ShopServiceRegion> Regions { get; set; } = new List<ShopServiceRegion>();

        /// <summary>
        /// 提交人
        /// </summary>
        [Required(ErrorMessage = "提交人不能为空")]
        public string SubmitBy { get; set; }
    }

    /// <summary>
    /// 省
    /// </summary>
    public class ProvinceRequest
    {
        /// <summary>
        /// 省Id
        /// </summary>
        public string ProvinceId { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public List<CityRequest> Cities { get; set; } = new List<CityRequest>();
    }

    /// <summary>
    /// 市
    /// </summary>
    public class CityRequest
    {
        /// <summary>
        /// 城市Id
        /// </summary>
        public string CityId { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        public List<DistrictRequest> Districts { get; set; } = new List<DistrictRequest>();
    }

    /// <summary>
    /// 区
    /// </summary>
    public class DistrictRequest
    {
        /// <summary>
        /// 区Id
        /// </summary>
        public string DistrictId { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        public string District { get; set; }
    }
}
