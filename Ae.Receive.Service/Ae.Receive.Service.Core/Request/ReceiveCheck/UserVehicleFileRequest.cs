using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request.ReceiveCheck
{
    /// <summary>
    /// 用户车辆档案
    /// </summary>
    public class UserVehicleFileRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required(ErrorMessage = "用户Id不能为空")]
        public string UserId { get; set; }

        /// <summary>
        /// 车Id
        /// </summary>
        [Required(ErrorMessage = "车辆Id不能为空")]
        public string CarId { get; set; }
    }
}
