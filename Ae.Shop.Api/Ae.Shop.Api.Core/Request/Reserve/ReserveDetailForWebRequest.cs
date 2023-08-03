using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Api.Core.Request.Reserve
{
    /// <summary>
    /// 预约详情Request
    /// </summary>
    public class ReserveDetailForWebRequest
    {
        /// <summary>
        /// 预约Id
        /// </summary>
        [Required(ErrorMessage = "预约Id不能为空")]
        [Range(1, Int32.MaxValue, ErrorMessage = "预约Id必须大于0")]
        public long ReserveId { get; set; }
    }
}
