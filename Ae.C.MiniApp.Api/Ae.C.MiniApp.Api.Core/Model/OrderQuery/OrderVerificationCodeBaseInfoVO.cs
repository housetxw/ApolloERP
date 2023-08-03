using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model
{
    /// <summary>
    /// 订单核销码基本信息
    /// </summary>
    public class OrderVerificationCodeBaseInfoVO
    {
        /// <summary>
        /// 核销码（RGHX*****）
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 二维码图片Base64字符串
        /// </summary>
        public string QRCodeBase64String { get; set; }
        /// <summary>
        /// 状态（0未使用 1已使用 2已过期）
        /// </summary>
        public sbyte Status { get; set; }
    }
}
