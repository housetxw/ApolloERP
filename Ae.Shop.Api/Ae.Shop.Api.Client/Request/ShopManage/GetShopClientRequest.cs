using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Api.Client.Request
{
    /// <summary>
    /// 获取门店信息
    /// </summary>
    public class GetShopClientRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        [Required(ErrorMessage = "门店ID不能为空")]
        public long ShopId { get; set; }
    }
}
