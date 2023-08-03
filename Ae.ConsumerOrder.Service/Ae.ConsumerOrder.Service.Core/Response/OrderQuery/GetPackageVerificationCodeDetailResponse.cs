using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Response.OrderQuery
{
    public class GetPackageVerificationCodeDetailResponse
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        ///产品Id
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 开始核销时间
        /// </summary>
        public DateTime StartValidTime { get; set; }

        /// <summary>
        /// 结束核销时间
        /// </summary>
        public DateTime EndValidTime { get; set; }

        /// <summary>
        /// 二维码图片Base64字符串
        /// </summary>
        public string QRCodeBase64String { get; set; }

        /// <summary>
        /// 核销码
        /// </summary>
        public string Code { get; set; }
    }
}
