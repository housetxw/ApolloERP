using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Request.ShopManage
{
    public class ShopCustomerDetailClientRequest
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        public int ShopId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
    }
}
