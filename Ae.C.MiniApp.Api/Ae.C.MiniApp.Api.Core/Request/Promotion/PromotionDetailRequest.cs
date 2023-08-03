using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.Promotion
{
    /// <summary>
    /// 促销详情
    /// </summary>
    public class PromotionDetailRequest
    {
        /// <summary>
        /// 活动Id
        /// </summary>
        [Required(ErrorMessage = "活动Id不能为空")]
        public string ActivityId { get; set; }

        [Required(ErrorMessage = "商品Id不能为空")]
        public string Pid { get; set; }
    }
}
