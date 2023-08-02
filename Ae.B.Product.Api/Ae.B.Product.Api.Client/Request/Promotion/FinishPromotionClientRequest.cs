using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.Promotion
{
    public class FinishPromotionClientRequest
    {
        /// <summary>
        /// 活动Id
        /// </summary>
        public string ActivityId { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitBy { get; set; }
    }
}
