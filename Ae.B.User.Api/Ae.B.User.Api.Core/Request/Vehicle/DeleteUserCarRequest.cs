using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.User.Api.Core.Request.Vehicle
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteUserCarRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required(ErrorMessage = "UserId不能为空")]
        public string UserId { get; set; }

        /// <summary>
        /// 车辆Id
        /// </summary>
        [Required(ErrorMessage = "CarId不能为空")]
        public string CarId { get; set; }
    }
}
