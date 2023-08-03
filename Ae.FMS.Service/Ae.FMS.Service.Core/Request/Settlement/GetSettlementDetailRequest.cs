using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.FMS.Service.Core.Request.Settlement
{
    /// <summary>
    /// 结算单明细请求对象
    /// </summary>
    public class GetSettlementDetailRequest
    {
        /// <summary>
        /// 结算单批次号
        /// </summary>
        [Required]
        public string SettlementBatchNo { get; set; }
    }
}
