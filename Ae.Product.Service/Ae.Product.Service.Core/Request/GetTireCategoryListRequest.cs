using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Product.Service.Core.Request
{
    /// <summary>
    /// 轮胎适配分类Request
    /// </summary>
    public class GetTireCategoryListRequest
    {
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
    }
}
