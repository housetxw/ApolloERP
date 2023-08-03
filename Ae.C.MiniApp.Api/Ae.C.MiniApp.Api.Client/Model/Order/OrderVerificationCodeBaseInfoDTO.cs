using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Model.Order
{
    /// <summary>
    /// 订单核销码基本信息
    /// </summary>
    public class OrderVerificationCodeBaseInfoDTO
    {
        /// <summary>
        /// 核销码（RGHX*****）
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 状态（0未使用 1已使用 2已过期）
        /// </summary>
        public sbyte Status { get; set; }
    }
}
