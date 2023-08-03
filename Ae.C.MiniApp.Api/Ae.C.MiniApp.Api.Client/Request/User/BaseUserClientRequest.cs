using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request
{
    /// <summary>
    /// 基本用户请求参数
    /// </summary>
    public class BaseUserClientRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        //[Required(ErrorMessage = "用户ID不能为空")]
        public string UserId { get; set; }
    }
}
