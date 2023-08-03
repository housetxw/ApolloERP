using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Core.Request.Settlement
{
    public class GetPurchaseSettlementListRequest : BasePageRequest
    {
        public long ShopId { get; set; }

        /// <summary>
        /// 付款状态
        /// </summary>
        public sbyte PayStatus { get; set; } = -1;

        /// <summary>
        ///结算批次
        /// </summary>
        public string SettlementBatchNo { get; set; }


        /// <summary>
        /// 开始结算日期
        /// </summary>
        public string StartSettlementTime { get; set; }

        /// <summary>
        /// 结束结算日期
        /// </summary>
        public string EndSettlementTime { get; set; }


    }
}
