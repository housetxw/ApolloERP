using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
    public class ShopStockRequest:BasePageRequest
    {
        public string ProductId { get; set; }

        /// <summary>
        /// 1.全部 2.外采 3.自采
        /// </summary>
        public string SourceType { get; set; }

        public long LocationId { get; set; }

        /// <summary>
        /// false ->无货
        /// </summary>
        public bool isStock { get; set; }
    }
}
