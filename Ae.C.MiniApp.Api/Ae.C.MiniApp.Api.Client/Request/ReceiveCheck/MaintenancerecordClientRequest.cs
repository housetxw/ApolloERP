using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request.ReceiveCheck
{
    public class MaintenancerecordClientRequest
    {
        /// <summary>
        /// 车Id
        /// </summary>
        public string CarId { get; set; }

        /// <summary>
        /// 服务类型
        /// </summary>
        public string ServiceCode { get; set; }
    }
}
