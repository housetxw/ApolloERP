using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
    public class AcceptCompanyStockRequest
    {
        public long LocationId { get; set; }

        public string UpdateBy { get; set; }

        public int LocationType { get; set; }

        public List<VenderStockDTO> Products { get; set; } = new List<VenderStockDTO>();
    }

    public class VenderStockDTO
    {
        /// <summary>
        /// 公司/门店编号
        /// </summary>
        public long LocationId { get; set; }

        /// <summary>
        /// 公司/门店名称
        /// </summary>
        public string LocationName { get; set; }

        public string ProductName { get; set; }

        public string ProductId { get; set; }

        public int Num { get; set; }


        public string RelationId { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public string TaskType { get; set; }

        public DateTime HandelTime { get; set; }
    }
}
