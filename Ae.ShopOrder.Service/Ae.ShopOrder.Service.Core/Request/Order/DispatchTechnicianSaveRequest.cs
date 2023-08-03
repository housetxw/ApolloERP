using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Order
{
    /// <summary>
    /// 派工技师保存请求
    /// </summary>
    public class DispatchTechnicianSaveRequest
    {
        /// <summary>
        /// 到店ID
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreateBy { get; set; }

        public long ShopId { get; set; }
    }
}
