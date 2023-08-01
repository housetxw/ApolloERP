using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Vehicle.Service.Core.Request
{
    /// <summary>
    /// 车系搜搜
    /// </summary>
    public class VehicleSearchByNameRequest
    {
        /// <summary>
        /// 搜索名
        /// </summary>
        [Required(ErrorMessage = "搜索条件不能为空")]
        public string Name { get; set; }
    }
}
