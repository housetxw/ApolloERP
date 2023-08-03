using Ae.Receive.Service.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request
{
    /// <summary>
    /// 取消预约Request
    /// </summary>
    public class CancelReserveRequest
    {
        /// <summary>
        /// 预约ID
        /// </summary>
        public long ReserveId { get; set; }

        /// <summary>
        /// 取消原因
        /// </summary>
        public string CancelReason { get; set; } = string.Empty;

        /// <summary>
        /// 修改人
        /// </summary>
        [Required(ErrorMessage = "操作人不能为空")]
        public string UpdateBy { get; set; }
    }
}
