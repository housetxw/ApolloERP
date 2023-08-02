using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Product.Service.Core.Request.Promotion
{
    /// <summary>
    /// 商品促销详情Request
    /// </summary>
    public class ProductPromotionDetailRequest
    {
        /// <summary>
        /// 活动Id
        /// </summary>
        [Required(ErrorMessage = "活动Id不能为空")]
        public string ActivityId { get; set; }

        /// <summary>
        /// 商品Pid
        /// </summary>
        [Required(ErrorMessage = "商品Id不能为空")]
        public string Pid { get; set; }
    }
}
