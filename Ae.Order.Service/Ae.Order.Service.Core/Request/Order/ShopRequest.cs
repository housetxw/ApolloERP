using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Request.Order
{
    /// <summary>
    /// 门店请求集合
    /// </summary>
    public class ShopRequest
    {
        /// <summary>
        /// 门店集合
        /// </summary>
        public List<long> ShopIds { get; set; }
    }
}
