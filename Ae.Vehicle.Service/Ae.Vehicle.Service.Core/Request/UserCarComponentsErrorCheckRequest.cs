﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Vehicle.Service.Core.Request
{
    /// <summary>
    /// 车辆部位异常记录
    /// </summary>
    public class UserCarComponentsErrorCheckRequest
    {
        /// <summary>
        /// 车辆Id
        /// </summary>
        [Required(ErrorMessage = "车辆Id不能为空")]
        public string CarId { get; set; }

        /// <summary>
        /// 部件Code
        /// </summary>
        [Required(ErrorMessage = "部件Code不能为空")]
        public string KeyName { get; set; }
    }
}
