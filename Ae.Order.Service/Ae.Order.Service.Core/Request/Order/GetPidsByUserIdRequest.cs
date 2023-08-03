using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Order.Service.Core.Request.Order
{
    public class GetPidsByUserIdRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required(ErrorMessage = "用户ID不可为空")]
        public string UserId { get; set; }
    }
}
