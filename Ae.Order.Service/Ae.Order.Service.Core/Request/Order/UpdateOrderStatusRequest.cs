using Ae.Order.Service.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Request
{
    public class UpdateOrderStatusRequest
    {

        /// <summary>
        /// 订单Id
        /// </summary>
        public List<string> OrderNos { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string UpdateBy { get; set; }

        /// <summary>
        /// 更新订单状态类型
        /// </summary>
        public UpdateOrderStatusTypeEnum UpdateStatusType { get; set; }
    }
}
