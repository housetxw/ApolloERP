using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.User.Api.Client.Request.Vehicle
{
    public class UserVehicleByCarIdClientRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 车Id
        /// </summary>
        public string CarId { get; set; }
    }
}
