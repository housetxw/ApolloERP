using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    /// <summary>
    /// 快速排队请求对象
    /// </summary>
    public class QuickQueueRequest : BaseGetRequest
    {
        /// <summary>
        /// 扫码扫出的门店Id
        /// </summary>
        public int ShopId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
    }
}
