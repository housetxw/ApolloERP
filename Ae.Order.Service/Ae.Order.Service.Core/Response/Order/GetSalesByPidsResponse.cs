using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Response.Order
{
    public class GetSalesByPidsResponse
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public string Pid { get; set; }
        /// <summary>
        /// 销量
        /// </summary>
        public int Sales { get; set; }
    }
}
