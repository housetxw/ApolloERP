using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Request
{
    public class GetVerificationCodeOrderDetailRequest
    {
        /// <summary>
        /// 核销码
        /// </summary>
        [Required(ErrorMessage = "核销码不可为空")]
        public string Code { get; set; }
    }
}
