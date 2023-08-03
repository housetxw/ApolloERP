using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Sign
{
    /// <summary>
    /// 签收码验证
    /// </summary>
    public class ValidateWaitingSignOrderOrPackageRequest
    {
        /// <summary>
        /// 门店id
        /// </summary>
        [Required]
        public int ShopId { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        [Required]
        public string Content { get; set; }
    }
}
