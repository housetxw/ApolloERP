using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Api.Core.Request.ShopCustomer
{
    /// <summary>
    /// 客户详情Request
    /// </summary>
    public class ShopCustomerDetailRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required(ErrorMessage = "用户Id不能为空")]
        public string UserId { get; set; }
    }
}
