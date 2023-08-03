using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Api.Core.Request.Promotion
{
    /// <summary>
    /// 
    /// </summary>
    public class PromotionActivityDetailRequest
    {
        /// <summary>
        /// 活动Id
        /// </summary>
        [Required(ErrorMessage = "活动Id不能为空")]
        public string ActivityId { get; set; }
    }
}
