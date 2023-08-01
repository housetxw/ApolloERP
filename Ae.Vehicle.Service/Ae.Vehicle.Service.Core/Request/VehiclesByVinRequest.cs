using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Vehicle.Service.Core.Request
{
    /// <summary>
    /// Vin识别车型
    /// </summary>
    public class VehiclesByVinRequest
    {
        /// <summary>
        /// Vin码
        /// </summary>
        [Required(ErrorMessage = "Vin码不能为空")]
        public string VinCode { get; set; }
    }
}
