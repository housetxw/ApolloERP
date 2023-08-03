using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Response.Order
{
    /// <summary>
    /// 得到技师的返回对象
    /// </summary>
    public class GetDispatchTechniciansResponse
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// UserId
        /// </summary>
        public string UserId { get; set; }
    }
}
