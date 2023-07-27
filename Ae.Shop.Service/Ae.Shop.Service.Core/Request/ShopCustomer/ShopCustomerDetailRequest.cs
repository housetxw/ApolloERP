using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request.ShopCustomer
{
    /// <summary>
    /// 门店客户详情
    /// </summary>
    public class ShopCustomerDetailRequest
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        [Range(1,Int32.MaxValue,ErrorMessage ="门店Id必须大于0")]
        public int ShopId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [Required(ErrorMessage ="用户Id不能为空")]
        public string UserId { get; set; }
    }
}
