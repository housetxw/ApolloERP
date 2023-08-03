using Ae.ConsumerOrder.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Request
{
    /// <summary>
    /// 提交订单请求参数
    /// </summary>
    public class SubmitOrderRequest : BaseSubmitOrderInfoDTO
    {
        /// <summary>
        /// 下单操作人
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// 预约时间
        /// </summary>
        public string ReserverTime { get; set; }

        /// <summary>
        /// 安装方式(0:未设置 1:到店安装 2：上门安装 3：无需安装）
        /// </summary>
        public sbyte InstallType { get; set; }

        /// <summary>
        /// 核销码
        /// </summary>
        public string Code { get; set; }
    }
}
