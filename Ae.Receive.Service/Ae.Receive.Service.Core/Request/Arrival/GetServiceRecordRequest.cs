using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    /// <summary>
    /// 得到服务记录请求对象
    /// </summary>
    public class GetServiceRecordRequest : BaseGetRequest
    {
        /// <summary>
        /// 车辆Id
        /// </summary>
        public string CarId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
    }
}
