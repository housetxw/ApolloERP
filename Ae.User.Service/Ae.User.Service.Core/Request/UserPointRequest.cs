using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.User.Service.Core.Request
{
    /// <summary>
    /// 用户积分
    /// </summary>
    public class UserPointRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required(ErrorMessage = "用户Id不能为空")]
        public string UserId { get; set; }

        /// <summary>
        /// 只查当前积分
        /// </summary>
        public bool OnlyCurrentPoint { get; set; } = false;

    }
}
