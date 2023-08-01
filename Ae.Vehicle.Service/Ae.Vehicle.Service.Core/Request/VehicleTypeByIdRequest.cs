using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Vehicle.Service.Core.Request
{
    /// <summary>
    /// 根据车系查车系详情
    /// </summary>
    public class VehicleTypeByIdRequest
    {
        /// <summary>
        /// 车系Id
        /// </summary>
        [Required(ErrorMessage = "VehicleId不能为空")]
        public string VehicleId { get; set; }
    }
}
