using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Api.Core.Request.ShopProduct
{
    /// <summary>
    /// 删除商品
    /// </summary>
    public class DeleteShopProductRequest
    {
        /// <summary>
        /// 商品Pid
        /// </summary>
        [Required(ErrorMessage = "商品Pid不能为空")]
        public string ProductCode { get; set; }

        public long ShopId { get; set; }
    }
}
