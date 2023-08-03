using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Request
{
    public class SubmitUseVerificationCodeOrderRequest
    {
        /// <summary>
        /// 核销码
        /// </summary>
        [Required(ErrorMessage = "核销码不可为空")]
        public string Code { get; set; }
        /// <summary>
        /// 核销门店ID
        /// </summary>
        [Required(ErrorMessage = "核销门店ID不可为空")]
        public long ShopId { get; set; }
    }
}
