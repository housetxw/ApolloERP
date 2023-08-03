using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Request.Vehicle
{
    public class UserCarFileClientRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 车辆Id
        /// </summary>
        public string CarId { get; set; }
    }
}
