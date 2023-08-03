using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Response
{
    public class ProductStockResponse
    {
        /// <summary>
        /// 仓库编号
        /// </summary>
        public long LocationId { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string Location { get; set; } = string.Empty;
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// 可用库存
        /// </summary>
        public int AvailableNum { get; set; }


        /// <summary>
        /// 入库批次(源批次)
        /// </summary>
        public long BatchId { get; set; }

        /// <summary>
        /// 门店批次
        /// </summary>
        public long RefBatchId { get; set; }

        /// <summary>
        /// 成本单价
        /// </summary>
        public decimal CostPrice { get; set; }


        /// <summary>
        /// 周期
        /// </summary>
        public string WeekYear { get; set; }
        /// <summary>
        /// 货主
        /// </summary>
        public int OwnerId { get; set; }
        /// <summary>
        /// 良品类型(1良品 2不良品)
        /// </summary>
        public sbyte StockType { get; set; }

        /// <summary>
        /// 库存单位
        /// </summary>
        public string StockUnit { get; set; } = string.Empty;
        /// <summary>
        /// 货主名称
        /// </summary>
        public string OwnerName { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;

    }
}
