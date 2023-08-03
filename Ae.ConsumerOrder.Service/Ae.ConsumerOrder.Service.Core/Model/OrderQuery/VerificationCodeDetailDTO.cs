using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Model
{
    /// <summary>
    /// APP核销码详情
    /// </summary>
    public class VerificationCodeDetailDTO
    {
        /// <summary>
        /// 服务码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 状态（0未使用 1已使用 2已过期）
        /// </summary>
        public sbyte Status { get; set; }
        /// <summary>
        /// 购买人手机号
        /// </summary>
        public string UserPhone { get; set; }
        /// <summary>
        /// 截止有效期
        /// </summary>
        public DateTime EndValidTime { get; set; }
        /// <summary>
        /// 服务项目商品名称
        /// </summary>
        public string ServiceProductName { get; set; }
        /// <summary>
        /// 券码价值
        /// </summary>
        public decimal ProductAmount { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
