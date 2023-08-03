using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.FMS.Service.Core.Request.Settlement
{
    public class SavePurchaseSettlementOrderRequest
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreateUser { get; set; }

        /// <summary>
        /// 开始结算日期
        /// </summary>
        [Required]
        public string StartSettelementDate { get; set; }

        /// <summary>
        /// 结束结算日期
        /// </summary>
        [Required]
        public string EndSettlementDate { get; set; }
    }
}
