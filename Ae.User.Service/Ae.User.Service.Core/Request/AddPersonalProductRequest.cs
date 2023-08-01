﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.User.Service.Core.Request
{
    /// <summary>
    /// 关注商品
    /// </summary>
    public class AddPersonalProductRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required(ErrorMessage = "UserId不能为空")]
        public string UserId { get; set; }

        /// <summary>
        /// 商品pid
        /// </summary>
        [Required(ErrorMessage = "商品不能为空")]
        [MinLength(1, ErrorMessage = "商品不能为空")]
        public List<string> PidList { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        [Required(ErrorMessage = "来源不能为空")]
        public string Source { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        [Required(ErrorMessage = "提交人不能为空")]
        public string SubmitBy { get; set; }
    }
}
