using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.User.Api.Core.Request.User
{
    /// <summary>
    /// 
    /// </summary>
    public class UserInfoByUserTelRequest
    {
        /// <summary>
        /// 手机号
        /// </summary>
        [Required(ErrorMessage = "手机号不能为空")]
        public string UserTel { get; set; }
    }
}
