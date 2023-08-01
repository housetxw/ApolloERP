using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Vehicle.Service.Core.Request
{
    /// <summary>
    /// 查用户所有车型request
    /// </summary>
    public class AllUserVehiclesRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required(ErrorMessage = "UserId不能为空")]
        public string UserId { get; set; }
    }
}
