using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.Arrival
{
    /// <summary>
    /// 快速排队请求对象
    /// </summary>
    public class QuickQueueRequest:BaseGetRequest
    {
        /// <summary>
        /// 扫码扫出的门店Id
        /// </summary>
        [Required]
        public int ShopId { get; set; }

        /// <summary>
        /// UserId(不需要传输)
        /// </summary>
        public string UserId { get; set; }
    }
}
