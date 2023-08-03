using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request
{
    /// <summary>
    /// 历史到店Request
    /// </summary>
    public class HistoryReceiveRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required(ErrorMessage = "UserId不能为空")]
        public string UserId { get; set; }
    }
}
