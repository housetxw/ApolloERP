using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class GetNearShopListRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public List<long> ShopIds { get; set; } = new List<long>();
        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// 省ID
        /// </summary>
        public string ProvinceId { get; set; }
        /// <summary>
        /// 市ID
        /// </summary>
        public long CityId { get; set; }
        /// <summary>
        /// 区ID
        /// </summary>
        public long DistrictId { get; set; }
        /// <summary>
        /// 排序 1 正序,2 倒序
        /// </summary>
        public int OrderBy { get; set; }
        /// <summary>
        /// 门店类型 0默认，4上门养车
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "页码必须大于0")]
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页大小
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "页大小必须大于0")]
        public int PageSize { get; set; } = 10;
    }
}
