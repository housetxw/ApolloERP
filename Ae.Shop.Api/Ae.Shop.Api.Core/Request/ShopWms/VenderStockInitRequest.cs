using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
    public class VenderStockInitRequest
    {
        public long CompanyId { get; set; }

        public string UpdateBy { get; set; }

        public List<VenderProductStockDTO> Stocks { get; set; } = new List<VenderProductStockDTO>();
    }

    public class VenderProductStockDTO
    {
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        public int Num { get; set; } = 0;
    }

    public class GetCompanyStocksRequest
    {
        /// <summary>
        /// 待发货:1  已发货:2  待收货:3  已收货:4
        /// </summary>
        public int SearchType { get; set; }

        public long LocationId { get; set; }

        /// <summary>
        /// 公司:0 门店:1
        /// </summary>
        public int LocationType { get; set; }
    }

    public class CancelTaskRequest
    {
        public string RelationId { get; set; }

        public string TaskType { get; set; }

        public string UpdateBy { get; set; }
    }

    public class VenderStockResponse
    {
        public List<VenderProductStock> Stocks { get; set; } = new List<VenderProductStock>();
    }

    public class VenderProductStock
    {
        public string LocationName { get; set; } = string.Empty;

        public List<VenderStockDTO> ProductStocks { get; set; } = new List<VenderStockDTO>();
    }

}
