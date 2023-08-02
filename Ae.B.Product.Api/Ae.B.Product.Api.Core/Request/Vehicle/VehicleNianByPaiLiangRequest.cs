using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.Vehicle
{
    /// <summary>
    /// 排量查年份
    /// </summary>
    public class VehicleNianByPaiLiangRequest : BaseGetRequest
    {
        /// <summary>
        /// 车系Id
        /// </summary>
        [Required(ErrorMessage = "车系Id不能为空")]
        public string VehicleId { get; set; }

        /// <summary>
        /// 排量
        /// </summary>
        [Required(ErrorMessage = "排量不能为空")]
        public string PaiLiang { get; set; }
    }
}
