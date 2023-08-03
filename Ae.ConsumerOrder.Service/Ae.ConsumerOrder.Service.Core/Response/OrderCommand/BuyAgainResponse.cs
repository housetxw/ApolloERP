using Ae.ConsumerOrder.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Response
{
    /// <summary>
    /// 再次购买返回参数
    /// </summary>
    public class BuyAgainResponse
    {
        /// <summary>
        /// 新订单号
        /// </summary>
        public string OrderNo { get; set; }
    }
}
