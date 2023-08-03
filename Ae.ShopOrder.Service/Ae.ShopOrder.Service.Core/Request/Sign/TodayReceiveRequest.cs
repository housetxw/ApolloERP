using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Sign
{
    /// <summary>
    /// 今日收货进度请求对象
    /// </summary>
    public class TodayReceiveRequest
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        public int ShopId { get; set; }
    }
}
