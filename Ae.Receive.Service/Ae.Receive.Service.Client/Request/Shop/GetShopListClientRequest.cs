using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Request
{
    public class GetShopListClientRequest
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
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 1;
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}
