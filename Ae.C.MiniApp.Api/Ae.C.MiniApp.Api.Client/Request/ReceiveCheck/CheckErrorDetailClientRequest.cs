using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request.ReceiveCheck
{
    public class CheckErrorDetailClientRequest
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
