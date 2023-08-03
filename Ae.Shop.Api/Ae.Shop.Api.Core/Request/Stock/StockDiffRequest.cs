using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
   public class StockDiffRequest:BasePageRequest
    {
        public string ProductId { get; set; }

        public string PlanNo { get; set; }
        public long PlanId { get; set; }
        public long WarehouseId { get; set; }

        public List<string> Times { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string Status { get; set; }

        /// <summary>
        /// 1 盘亏/盈  2.盘亏  3.盘盈
        /// </summary>
        public int Type { get; set; }

        public long ShopId { get; set; }

        public string SourceType { get; set; }
    }
}
