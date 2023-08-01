using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.User.Service.Core.Request
{
    /// <summary>
    /// UserAuthRequest
    /// </summary>
    public class UserAuthRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required(ErrorMessage = "UserId不能为空")]
        public string UserId { get; set; }

        /// <summary>
        /// 认证类型 默认0：二代身份证
        /// </summary>
        public int AuthType { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [Required(ErrorMessage = "AuthName不能为空")]
        public string AuthName { get; set; }

        /// <summary>
        /// 真实身份证
        /// </summary>
        [Required(ErrorMessage = "AuthIdCard不能为空")]
        public string AuthIdCard { get; set; }
    }
}
