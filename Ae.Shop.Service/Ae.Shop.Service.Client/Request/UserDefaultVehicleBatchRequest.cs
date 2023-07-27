using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Client.Request
{
    public class UserDefaultVehicleBatchRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public List<string> UserIdList { get; set; }
    }
}
