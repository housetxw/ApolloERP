using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Client.Request
{
    public class BaseUserClientRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required(ErrorMessage = "用户ID不能为空")]
        public string UserId { get; set; }
    }
}
