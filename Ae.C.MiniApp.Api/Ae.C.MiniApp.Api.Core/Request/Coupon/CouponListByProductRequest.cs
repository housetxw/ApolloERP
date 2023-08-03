using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.Coupon
{
    /// <summary>
    /// CouponListByProductRequest
    /// </summary>
    public class CouponListByProductRequest
    {
        /// <summary>
        /// 产品pid
        /// </summary>
        [Required(ErrorMessage = "产品pid不能为空")]
        public string Pid { get; set; }
    }
}
