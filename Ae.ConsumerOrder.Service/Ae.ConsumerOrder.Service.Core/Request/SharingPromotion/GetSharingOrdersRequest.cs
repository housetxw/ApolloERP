using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Request.SharingPromotion
{
    public class GetSharingOrdersRequest : BasePageRequest
    {
        public string UserId { get; set; }

        /// <summary>
        /// 兑换状态(0：全部 1：只查未兑换 ）
        /// </summary>
        public bool ExchangeStatus { get; set; }
    }
}
