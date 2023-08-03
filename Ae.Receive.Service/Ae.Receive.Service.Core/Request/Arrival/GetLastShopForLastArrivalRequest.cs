using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    /// <summary>
    /// 得到上一次到点记录ShopId请求
    /// </summary>
    public class GetLastShopForLastArrivalRequest: BaseGetRequest
    {
        /// <summary>
        /// UsrId
        /// </summary>
        public string UserId { get; set; }
    }
}
