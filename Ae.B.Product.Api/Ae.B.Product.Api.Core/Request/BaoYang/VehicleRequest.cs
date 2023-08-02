using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.BaoYang
{
    /// <summary>
    /// 车型请求
    /// </summary>
    public class VehicleRequest
    {
        /// <summary>
        /// 车系Id
        /// </summary>
        [Required(ErrorMessage = "VehicleId不能为空")]
        public string VehicleId { get; set; }

        /// <summary>
        /// 排量
        /// </summary>
        [Required(ErrorMessage = "PaiLiang不能为空")]
        public string PaiLiang { get; set; }

        /// <summary>
        /// 生产年份
        /// </summary>
        [Required(ErrorMessage = "Nian不能为空")]
        public string Nian { get; set; }

        /// <summary>
        /// 五级车型Tid
        /// </summary>
        public string Tid { get; set; }

        /// <summary>
        /// 五级属性
        /// </summary>
        public List<VehicleProperty> Properties { get; set; }

        /// <summary>
        /// 里程数
        /// </summary>
        public int Distance { get; set; }

        /// <summary>
        /// 上路时间（Eg:2018-09）
        /// </summary>
        public string OnRoadTime { get; set; }

        /// <summary>
        /// 轮胎尺寸
        /// </summary>
        public string TireSize { get; set; }
    }

    /// <summary>
    /// 车型五级属性
    /// </summary>
    public class VehicleProperty
    {
        /// <summary>
        /// 属性名
        /// </summary>
        [Required(ErrorMessage = "PropertyKey不能为空")]
        public string PropertyKey { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        [Required(ErrorMessage = "PropertyValue不能为空")]
        public string PropertyValue { get; set; }
    }
}
