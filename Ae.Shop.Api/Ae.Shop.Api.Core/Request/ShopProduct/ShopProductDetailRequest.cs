using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Api.Core.Request.ShopProduct
{
    /// <summary>
    /// 商品详情
    /// </summary>
    public class ShopProductDetailRequest
    {
        /// <summary>
        /// 商品Pid
        /// </summary>
        [Required(ErrorMessage ="商品Id不能为空")]
        public string ProductCode { get; set; }

        public long ShopId { get; set; }
    }
}
