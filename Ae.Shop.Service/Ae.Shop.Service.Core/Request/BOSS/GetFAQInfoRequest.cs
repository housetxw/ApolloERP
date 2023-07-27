using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class GetFAQInfoRequest
    {
        /// <summary>
        /// FAQId
        /// </summary>
        [Range(1, long.MaxValue, ErrorMessage = "FAQId必须大于0")]
        public long FAQId { get; set; }
    }
}
