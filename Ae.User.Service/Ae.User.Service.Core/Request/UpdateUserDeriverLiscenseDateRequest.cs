using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.User.Service.Core.Request
{
    public class UpdateUserDeriverLiscenseDateRequest
    {
        public string UserId { get; set; }

        /// <summary>
        /// 驾驶证到期日
        /// </summary>
        public string DriverLicenseExpiry { get; set; }
    }
}
