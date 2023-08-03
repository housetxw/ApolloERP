using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
    public class GetShopRequest : BaseGetRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        [Required(ErrorMessage = "门店ID不能为空")]
        public long ShopId { get; set; }
    }
}
