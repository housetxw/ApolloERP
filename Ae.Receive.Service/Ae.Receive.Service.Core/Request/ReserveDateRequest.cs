using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request
{
    /// <summary>
    /// 预约看板时间Request
    /// </summary>
    public class ReserveDateRequest
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "门店Id必须大于0")]
        public int ShopId { get; set; }
    }
}
