using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Model.Order
{
   public  class OrderVerificationCodeInfoDTO
    {
        /// <summary>
        /// 核销码（RGHX*****）
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 状态（0未使用 1已使用 2已过期）
        /// </summary>
        public sbyte Status { get; set; }

        /// <summary>
        /// 核销规则信息
        /// </summary>
        public VerificationRuleInfoDTO VerificationRuleInfo { get; set; }
    }
}
