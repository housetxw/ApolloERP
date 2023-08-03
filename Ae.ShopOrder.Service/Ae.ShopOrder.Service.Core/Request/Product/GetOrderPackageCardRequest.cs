using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Product
{
    public class GetOrderPackageCardRequest
    {
        /// <summary>
        /// 购买套餐卡订单集合
        /// </summary>
        public List<string> SourceOrderNos { get; set; }

        /// <summary>
        /// 使用套餐卡订单集合
        /// </summary>
        public List<string> VerifyOrderNos { get; set; }

    }
}
