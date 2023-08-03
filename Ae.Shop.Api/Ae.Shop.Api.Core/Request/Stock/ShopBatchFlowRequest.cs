using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
    public class ShopBatchFlowRequest: BasePageRequest
    {

        public long ShopId { get; set; }

        public string ProductId { get; set; } = string.Empty;

        /// <summary>
        /// 出入库编号
        /// </summary>
        public string StockId { get; set; } = string.Empty;

        /// <summary>
        /// 1.全部 2.入库 3.出库
        /// </summary>
        public string OperationType { get; set; } = string.Empty;

        public List<string> Times { get; set; } = new List<string>();

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string SourceType { get; set; } = string.Empty;

        /// <summary>
        /// 来源单号
        /// </summary>
        public string SourceInventoryNo { get; set; } = string.Empty;
    }
}
