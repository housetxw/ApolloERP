using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    /// <summary>
    /// 到店记录详情请求对象
    /// </summary>
    public class GetArrivalDetailRequest : BaseGetRequest
    {
        /// <summary>
        /// 到店记录Id
        /// </summary>
        public long ArrivalId { get; set; }
    }
}
