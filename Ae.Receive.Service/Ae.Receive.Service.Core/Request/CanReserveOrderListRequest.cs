using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Ae.Receive.Service.Core.Request
{
    public class CanReserveOrderListRequest : BasePageRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required(ErrorMessage = "用户ID不能为空")]
        public string UserId { get; set; }
    }
}
