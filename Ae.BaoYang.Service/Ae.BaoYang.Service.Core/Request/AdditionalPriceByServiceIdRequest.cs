using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.BaoYang.Service.Core.Request
{
    /// <summary>
    /// AdditionalPriceByServiceIdRequest
    /// </summary>
    public class AdditionalPriceByServiceIdRequest
    {
        /// <summary>
        /// 服务pid
        /// </summary>
        [Required(ErrorMessage = "服务不能为空")]
        [MinLength(1, ErrorMessage = "服务不能为空")]
        public List<string> ServiceId { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        public VehicleRequest Vehicle { get; set; }
    }
}
