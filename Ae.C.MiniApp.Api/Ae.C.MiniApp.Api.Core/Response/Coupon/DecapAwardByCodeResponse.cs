using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response.Coupon
{
    /// <summary>
    /// 
    /// </summary>
    public class DecapAwardByCodeResponse
    {
        /// <summary>
        /// 可领取
        /// </summary>
        public bool CanDraw { get; set; }

        /// <summary>
        /// 错误提示
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Code对应的Id
        /// </summary>
        public long AwardId { get; set; }
    }
}
