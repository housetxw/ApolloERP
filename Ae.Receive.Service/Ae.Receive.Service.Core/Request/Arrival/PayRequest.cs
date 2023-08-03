using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    /// <summary>
    /// 收款
    /// </summary>
    public class PayRequest : BaseGetRequest
    {

        /// <summary>
        /// 到店记录
        /// </summary>
        public string ArrivalId { get; set; }

        /// <summary>
        /// shopId 
        /// </summary>
        public int ShopId { get; set; }

    }
}
