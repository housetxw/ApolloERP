using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Response
{
    public class GetVerificationCodeOrderDetailResponse
    {
        /// <summary>
        /// 服务码
        /// </summary>
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// 状态（0未使用 1已使用 2已过期）
        /// </summary>
        public sbyte Status { get; set; }
        /// <summary>
        /// 购买人手机号
        /// </summary>
        public string UserPhone { get; set; } = string.Empty;
        /// <summary>
        /// 截止有效期
        /// </summary>
        public DateTime EndValidTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 服务项目商品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 券码价值
        /// </summary>
        public decimal MarketingPrice { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; } = 1;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
    }
}
