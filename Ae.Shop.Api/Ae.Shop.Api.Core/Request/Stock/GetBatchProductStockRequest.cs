using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
  public  class GetBatchProductStockRequest
    {
        public long LocationId { get; set; }

        public List<string> ProductIds { get; set; } = new List<string> { };
    }

    public class GetProductStockRequest
    {
        public long LocationId { get; set; }

        /// <summary>
        /// 产品来源(总部产品(Company)，外采产品(Shop))
        /// </summary>
        public string SourceType { get; set; } = string.Empty;

        public List<string> ProductIds { get; set; } = new List<string> { };

        /// <summary>
        /// 查询关键字
        /// </summary>
        public string SearchKey { get; set; } = string.Empty;

        /// <summary>
        /// 数量类型(0全部，1：大于0，-1：小于0，2：非0)
        /// </summary>
        public int NumType { get; set; } = 0;
    }
}
