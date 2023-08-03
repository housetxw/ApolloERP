using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Request.Vehicle
{
    /// <summary>
    /// 得到用户默认车辆
    /// </summary>
    public class UserDefaultVehicleClientRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
    }
}
