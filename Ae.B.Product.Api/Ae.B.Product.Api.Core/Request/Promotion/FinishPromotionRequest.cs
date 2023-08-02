using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.Promotion
{
    /// <summary>
    /// 结束活动
    /// </summary>
    public class FinishPromotionRequest
    {
        /// <summary>
        /// 活动Id
        /// </summary>
        [Required(ErrorMessage = "活动Id不能为空")]
        public string ActivityId { get; set; }
    }
}
