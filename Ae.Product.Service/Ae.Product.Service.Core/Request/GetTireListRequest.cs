using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Product.Service.Core.Request
{
    /// <summary>
    /// 轮胎适配列表Request
    /// </summary>
    public class GetTireListRequest
    {
        /// <summary>
        /// 分类类型
        /// </summary>
        public string CategoryType { get; set; }

        /// <summary>
        /// 省Id
        /// </summary>
        public int ProvinceId { get; set; }

        /// <summary>
        /// 市Id
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// 车系Id
        /// </summary>
        [Required(ErrorMessage = "车系不能为空")]
        public string VehicleId { get; set; }

        /// <summary>
        /// 五级车型Tid
        /// </summary>
        public string Tid { get; set; }

        /// <summary>
        /// 轮胎尺寸
        /// </summary>
        public string TireSize { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 步长
        /// </summary>
        public int PageSize { get; set; }
    }
}
