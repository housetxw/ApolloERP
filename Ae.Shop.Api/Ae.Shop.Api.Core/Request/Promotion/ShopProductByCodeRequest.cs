using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Api.Core.Request.Promotion
{
    /// <summary>
    /// pid
    /// </summary>
    public class ShopProductByCodeRequest
    {
        /// <summary>
        /// pid
        /// </summary>
        [Required(ErrorMessage = "ProductCode不能为空")]
        public string ShopProductCode { get; set; }
    }
}
