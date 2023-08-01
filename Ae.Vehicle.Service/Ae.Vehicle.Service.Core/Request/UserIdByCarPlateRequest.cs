using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Vehicle.Service.Core.Request
{
    /// <summary>
    /// 车牌
    /// </summary>
    public class UserIdByCarPlateRequest : BasePageRequest
    {
        /// <summary>
        /// 车牌号
        /// </summary>
        [Required(ErrorMessage = "车牌号不能为空")]
        public string CarPlate { get; set; }
    }
}
