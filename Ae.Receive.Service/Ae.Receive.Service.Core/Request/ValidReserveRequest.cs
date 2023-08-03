using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request
{
    /// <summary>
    /// 查询有效预约
    /// </summary>
    public class ValidReserveRequest
    {
        /// <summary>
        /// 用户手机号
        /// </summary>
        [Required(ErrorMessage = "手机号不能为空")]
        public string UserTel { get; set; }

        /// <summary>
        /// 门店Id
        /// </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "ShopId必须大于0")]
        public int ShopId { get; set; }
    }
}
