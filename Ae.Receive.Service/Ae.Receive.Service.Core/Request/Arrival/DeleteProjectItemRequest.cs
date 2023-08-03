using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    /// <summary>
    /// 删除项目请求对象
    /// </summary>
    public class DeleteProjectItemRequest
    {
        /// <summary>
        /// 到店记录Id
        /// </summary>
        public string ArrivalId { get; set; }

        /// <summary>
        /// 项目Id
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// UserId
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// UserName 不需要传输
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// ShopId
        /// </summary>
        public int ShopId { get; set; }
    }
}
