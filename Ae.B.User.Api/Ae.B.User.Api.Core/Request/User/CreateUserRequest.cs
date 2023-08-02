using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.User.Api.Core.Request.User
{
    /// <summary>
    /// 创建用户Request
    /// </summary>
    public class CreateUserRequest
    {
        /// <summary>
        /// 客户姓名
        /// </summary>
        [Required(ErrorMessage = "客户姓名不能为空")]
        public string UserName { get; set; }

        /// <summary>
        /// 客户手机号
        /// </summary>
        [Required(ErrorMessage = "客户手机号不能为空")]
        public string UserTel { get; set; }

        /// <summary>
        /// 性别（0未设置 1男 2女）
        /// </summary>
        public int Gender { get; set; }
    }
}
