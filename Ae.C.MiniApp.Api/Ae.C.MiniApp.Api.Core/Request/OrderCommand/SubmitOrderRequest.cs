using Ae.C.MiniApp.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
    /// <summary>
    /// 提交订单请求参数
    /// </summary>
    public class SubmitOrderRequest : BaseSubmitOrderInfoVO
    {
        /// <summary>
        /// 核销码
        /// </summary>
        public string Code { get; set; }
    }
}
