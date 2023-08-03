using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
    public class CreateReverseOrderBaseRequest
    {
        /// <summary>
        /// 订单号
        /// </summary>
        [Required(ErrorMessage = "必须选择订单")]
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 原因ID
        /// </summary>
        [Required(ErrorMessage = "必须选择原因")]
        public long ReasonId { get; set; }
        /// <summary>
        /// 选填具体说明
        /// </summary>
        public string Instruction { get; set; } = string.Empty;
    }
}
