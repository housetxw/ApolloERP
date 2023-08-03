using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request
{
    public class BaseShopRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        [Range(1, long.MaxValue, ErrorMessage = "门店ID必须大于0")]
        public long ShopId { get; set; }
    }
}
