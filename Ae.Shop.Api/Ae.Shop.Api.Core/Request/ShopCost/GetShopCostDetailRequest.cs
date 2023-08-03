using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Api.Core.Request.ShopCost
{
    public class GetShopCostDetailRequest: BasePageRequest
    {
        [Required(ErrorMessage = "费用Id不能为空")]
        [Range(1, Int32.MaxValue, ErrorMessage = "费用Id必须大于0")]
        public long Id { get; set; }
    }
}
