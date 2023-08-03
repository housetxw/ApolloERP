using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Request
{
    public class GetCarByOrderNoRequest : BaseGetRequest
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
    }
}
