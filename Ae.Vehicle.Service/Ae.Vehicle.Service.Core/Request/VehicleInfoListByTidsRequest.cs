using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Vehicle.Service.Core.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class VehicleInfoListByTidsRequest
    {
        /// <summary>
        /// 五级彻心tid
        /// </summary>
        [Required(ErrorMessage = "tid不能为空")]
        [MinLength(1,ErrorMessage = "tid不能为空")]
        public List<string> Tids { get; set; }
    }
}
