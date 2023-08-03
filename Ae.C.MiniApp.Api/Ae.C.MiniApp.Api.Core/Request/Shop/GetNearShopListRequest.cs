using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
    public class GetNearShopListRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public List<long> ShopIds { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        [Required(ErrorMessage = "经度不能为空")]
        public double Longitude { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        [Required(ErrorMessage = "维度不能为空")]
        public double Latitude { get; set; }
        /// <summary>
        /// 市ID
        /// </summary>
        [Required(ErrorMessage = "市ID不能为空")]
        public long CityId { get; set; }
        /// <summary>
        /// 区ID
        /// </summary>
        public long DistrictId { get; set; }
        /// <summary>
        /// 排序 1 asc,2 desc
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
        public int PageSize { get; set; } = 20;
    }
}
