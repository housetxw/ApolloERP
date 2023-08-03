using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Api.Core.Request.Promotion
{
    /// <summary>
    /// GrouponProductDetailRequest
    /// </summary>
    public class GrouponProductDetailRequest
    {
        /// <summary>
        /// 产品pid
        /// </summary>
        [Required(ErrorMessage = "产品pid不能为空")]
        public string Pid { get; set; }
    }
}
