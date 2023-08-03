using Ae.C.MiniApp.Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.Product
{
    public class ProductDetailRequest
    {
        /// <summary>
        ///  产品编码
        /// </summary>
        [Required(ErrorMessage = "产品编码 必填")]
        public string ProductCode { get; set; }

        /// <summary>
        /// 省份Id
        /// </summary>
        // [Required(ErrorMessage = "省份Id 必填")]
        public string ProvinceId { get; set; }

        /// <summary>
        /// 城市Id
        /// </summary>
        // [Required(ErrorMessage = " 城市Id 必填")]
        public string CityId { get; set; }

        /// <summary>
        /// 产品入口
        /// </summary>
        public ProductEntranceEnum ProductEntrance { get; set; }
    }
}
