using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.User
{
    /// <summary>
    /// 验证当前手机号
    /// </summary>
    public class VerifyCurrentMobileRequest
    {
        /// <summary>
        /// 手机号
        /// </summary>
        [Required(ErrorMessage = "手机号不能为空")]
        public string MobilePhone { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        [Required(ErrorMessage = "验证码不能为空")]
        public string SecurityCode { get; set; }
    }
}
