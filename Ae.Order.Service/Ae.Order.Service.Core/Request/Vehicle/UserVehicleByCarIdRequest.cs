using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Order.Service.Core.Request.Vehicle
{
    /// <summary>
    /// 获取车型信息Request
    /// </summary>
    public class UserVehicleByCarIdRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required(ErrorMessage = "UserId不能为空")]
        public string UserId { get; set; }

        /// <summary>
        /// 车Id
        /// </summary>
        [Required(ErrorMessage = "CarId不能为空")]
        public string CarId { get; set; }
    }
}
