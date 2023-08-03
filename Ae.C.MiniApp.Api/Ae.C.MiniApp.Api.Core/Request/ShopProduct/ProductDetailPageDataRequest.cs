using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.ShopProduct
{
    /// <summary>
    /// 商品详情页Request （自营商品、门店服务项目）
    /// </summary>
    public class ProductDetailPageDataRequest
    {
        /// <summary>
        /// 商品Pid
        /// </summary>
        [Required(ErrorMessage = "商品Pid不能为空")]
        public string ProductCode { get; set; }

        /// <summary>
        /// shopId
        /// </summary>
        public int ShopId { get; set; }
    }
}
