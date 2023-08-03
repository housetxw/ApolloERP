using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Api.Core.Request.Vehicle
{
    public class VehicleNianByPaiLiangRequest
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
