using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Api.Core.Request.Vehicle
{
    /// <summary>
    /// 
    /// </summary>
    public class VehicleListByBrandRequest
    {
        /// <summary>
        /// 品牌
        /// </summary>
        [Required(ErrorMessage = "品牌不能为空")]
        public string Brand { get; set; }
    }
}
