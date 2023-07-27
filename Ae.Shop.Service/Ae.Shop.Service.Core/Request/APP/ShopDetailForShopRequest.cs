using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request.APP
{
    /// <summary>
    /// 
    /// </summary>
    public class ShopDetailForShopRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "门店ID必须大于0")]
        public long ShopId { get; set; }

        /// <summary>
        /// 当前门店ID
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "门店ID必须大于0")]
        public long CurrentShopId { get; set; }
    }
}
