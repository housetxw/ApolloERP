using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    /// <summary>
    /// 未消费离店原因请求对象
    /// </summary>
    public class LeaveShopReasonSaveRequest
    {
        /// <summary>
        /// 到店记录Id
        /// </summary>
        public string ArrivalId { get; set; }

        /// <summary>
        /// 离店类型
        /// </summary>
        public string ReasonType { get; set; }

        /// <summary>
        /// 原因
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 去掉订单UserId
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// ShopId 
        /// </summary>
        public int ShopId { get; set; }

    }
}
