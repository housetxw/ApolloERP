﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.User.Service.Core.Request
{
    /// <summary>
    /// 用户关注
    /// </summary>
    public class UserAttentionProductRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required(ErrorMessage = "用户Id不能为空")]
        public string UserId { get; set; }    
    }
}
