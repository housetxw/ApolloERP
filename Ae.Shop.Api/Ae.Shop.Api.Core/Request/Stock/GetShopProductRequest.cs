using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
  public  class GetShopProductRequest
    {
        public long ShopId { get; set; }

        public string ProductId { get; set; }

        /// <summary>
        /// 1 所有 2总部 3门店
        /// </summary>
        public int Type { get; set; }
    }
}
