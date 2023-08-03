using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Shop
{
    public class GetShopServiceListWithPIDRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "门店ID必须大于0")]
        public long ShopId { get; set; }
        /// <summary>
        /// PID集合
        /// </summary>
        public List<string> ProductIds { get; set; }
    }
}
