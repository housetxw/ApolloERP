using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
    public class GetShopProductStocksRequest
    {
        public long ShopId { get; set; }
        public string SourceWarehouse { get; set; } = string.Empty;

        public List<string> Pids { get; set; } = new List<string>();

        /// <summary>
        /// 查询关键字
        /// </summary>
        public string SearchKey { get; set; } = string.Empty;
    }
}
