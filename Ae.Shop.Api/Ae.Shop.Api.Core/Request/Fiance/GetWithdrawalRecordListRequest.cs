using Ae.Shop.Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Api.Core.Request.Fiance
{
    /// <summary>
    /// 提现记录列表请求对象
    /// </summary>
    public class GetWithdrawalRecordListRequest : BasePageRequest
    {
        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        public SettlementBatchStatusEnum Status { get; set; }

        /// <summary>
        /// 门店Id
        /// </summary>
        public int ShopId { get; set; }

        /// <summary>
        /// 结算批次
        /// </summary>
        public string SettlementBatchNo { get; set; }

        /// <summary>
        /// 提现申请时间
        /// </summary>
        public string ApplyStartTime { get; set; }

        /// <summary>
        /// 提现申请时间
        /// </summary>
        public string ApplyEndTime { get; set; }




    }
}
