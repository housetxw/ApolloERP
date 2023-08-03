using Ae.C.Login.Api.Core.Enums;
using Ae.C.Login.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.Login.Api.Core.Request
{
    /// <summary>
    /// 登录
    /// </summary>
    public class RgLoginRequest : ReferrerInfo
    {
        /// <summary>
        /// 用户手机
        /// </summary>
        [Required(ErrorMessage = "手机号不能为空")]
        [MinLength(11,ErrorMessage = "手机号格式不正确")]
        [MaxLength(11, ErrorMessage = "手机号格式不正确")]
        public string MobileNumber { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [Required(ErrorMessage = "验证码不能为空")]
        public string VerificationCode { get; set; }

    }





}
