using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request.Shop
{
    /// <summary>
    /// 获取门店区域配置
    /// </summary>
    public class ShopServiceAreaRequest
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        [Range(1, Int64.MaxValue, ErrorMessage = "ShopId必须大于0")]
        public int ShopId { get; set; }
    }
}
