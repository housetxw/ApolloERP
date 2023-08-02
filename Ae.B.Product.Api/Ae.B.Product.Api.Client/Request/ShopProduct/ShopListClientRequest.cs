using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.ShopProduct
{
    public class ShopListClientRequest:PageListQuery
    {
        /// <summary>
        /// 门店简单名
        /// </summary>
        public string SimpleName { get; set; }
        /// <summary>
        /// 门店id
        /// </summary>
        public List<long> ShopIds { get; set; }
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

    }
}
