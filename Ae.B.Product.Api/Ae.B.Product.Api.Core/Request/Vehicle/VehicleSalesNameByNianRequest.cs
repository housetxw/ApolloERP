using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.Vehicle
{
    public class VehicleSalesNameByNianRequest : BaseGetRequest
    {
        /// <summary>
        /// 车系
        /// </summary>
        [Required(ErrorMessage = "车系Id不能为空")]
        public string VehicleId { get; set; }

        /// <summary>
        /// 排量
        /// </summary>
        [Required(ErrorMessage = "排量不能为空")]
        public string PaiLiang { get; set; }

        /// <summary>
        /// 生产年份
        /// </summary>
        [Required(ErrorMessage = "生产年份不能为空")]
        public string Nian { get; set; }
    }
}
