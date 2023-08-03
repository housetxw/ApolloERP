using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.Login.Api.Core.Request
{
    /// <summary>
    /// 刷新token
    /// </summary>
    public class RefreshTokenRequest
    {
        /// <summary>
        /// 刷新token
        /// </summary>
        [Required(ErrorMessage = "RefreshToken不能为空")]
        public string RefreshToken { get; set; }
    }
}
