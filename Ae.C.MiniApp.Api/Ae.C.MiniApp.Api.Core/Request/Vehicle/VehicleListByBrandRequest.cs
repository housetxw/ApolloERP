using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
    /// <summary>
    /// 品牌查车系Request
    /// </summary>
    public class VehicleListByBrandRequest: BaseGetRequest
    {
        /// <summary>
        /// 品牌
        /// </summary>
        [Required(ErrorMessage = "品牌不能为空")]
        public string Brand { get; set; }
    }
}
