using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Product.Api.Core.Request
{
    /// <summary>
    /// 防伪码查询
    /// </summary>
    public class SecurityCodeRequest
    {
        /// <summary>
        /// 防伪码
        /// </summary>
        [Required(ErrorMessage = "请输入防伪码")]
        public string SecurityCode { get; set; }
    }
}
