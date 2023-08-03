using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Api.Core.Request.Reserve
{
    /// <summary>
    /// 手机号搜索用户
    /// </summary>
    public class UserDetailByUserTelRequest
    {
        /// <summary>
        /// 用户手机号
        /// </summary>
        [Required(ErrorMessage = "用户手机号不能为空")]
        public string UserTel { get; set; }
    }
}
