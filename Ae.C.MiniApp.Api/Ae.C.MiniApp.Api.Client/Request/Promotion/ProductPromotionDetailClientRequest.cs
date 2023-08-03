using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request.Promotion
{
    public class ProductPromotionDetailClientRequest
    {
        /// <summary>
        /// 活动Id
        /// </summary>
        public string ActivityId { get; set; }

        /// <summary>
        /// 商品Pid
        /// </summary>
        public string Pid { get; set; }
    }
}
