using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Request.Shop
{
    /// <summary>
    /// 门店列表
    /// </summary>
    public class ShopListPageRequest : BasePageRequest
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        public int ShopId { get; set; }

        /// <summary>
        /// 省ID
        /// </summary>
        public string ProvinceId { get; set; }

        /// <summary>
        /// 市ID
        /// </summary>
        public string CityId { get; set; }

        /// <summary>
        /// 区ID
        /// </summary>
        public string DistrictId { get; set; }

        /// <summary>
        /// 服务类型6大类
        /// </summary>
        public string ServiceType { get; set; }
    }
}
