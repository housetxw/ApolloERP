using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Api.Core.Request.Reserve
{
    /// <summary>
    /// 获取用户有效预约
    /// </summary>
    public class ValidReserveRequest
    {
        /// <summary>
        /// 用户手机号
        /// </summary>
        [Required(ErrorMessage = "手机号不能为空")]
        public string UserTel { get; set; }
    }
}
