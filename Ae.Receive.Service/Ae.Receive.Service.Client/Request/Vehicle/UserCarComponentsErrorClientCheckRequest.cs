using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Request.Vehicle
{
    public class UserCarComponentsErrorClientCheckRequest
    {
        /// <summary>
        /// 车辆Id
        /// </summary>
        public string CarId { get; set; }

        /// <summary>
        /// 部件Code
        /// </summary>
        public string KeyName { get; set; }
    }
}
