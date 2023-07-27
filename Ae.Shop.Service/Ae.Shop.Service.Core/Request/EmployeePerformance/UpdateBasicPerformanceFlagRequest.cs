using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
   public class UpdateBasicPerformanceFlagRequest
    {
        [Required(ErrorMessage = "门店ID不能为空")]
        public long ShopId { get; set; }

        public string UpdateBy { get; set; } = string.Empty;

        public int ConfigFlag { get; set; }
    }
}
