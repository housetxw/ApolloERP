using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    /// <summary>
    /// 门店积分Request
    /// </summary>
    public class ShopPointRequest
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "shopId必须大于0")]
        public int ShopId { get; set; }
    }
}
