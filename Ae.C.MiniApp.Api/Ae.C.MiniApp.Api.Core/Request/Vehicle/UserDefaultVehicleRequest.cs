using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.Vehicle
{
    /// <summary>
    /// 获取用户默认车型
    /// </summary>
    public class UserDefaultVehicleRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
    }
}
