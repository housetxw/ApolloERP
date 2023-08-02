using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.FMS.Api.Core.Request.Settlement
{
    public class GetPurchaseSettlementDetailRequest : BaseGetRequest
    {
        /// <summary>
        /// 结算单批次号
        /// </summary>
        [Required]
        public string SettlementBatchNo { get; set; }
    }
}
