using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.FMS.Service.Core.Request.Settlement
{
    /// <summary>
    /// 得到订单提现记录列表请求对象
    /// </summary>
    public class GetWithdrawalOrderRecordListRequest:BasePageRequest
    {
        /// <summary>
        /// 提现批次
        /// </summary>
        [Required]
        public string BatchNo { get; set; }

        /// <summary>
        /// 门店Id
        /// </summary>
        [Required]
        public int ShopId { get; set; }
    }
}
