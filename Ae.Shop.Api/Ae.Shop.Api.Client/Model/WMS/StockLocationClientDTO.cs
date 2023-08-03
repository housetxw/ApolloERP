using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Model
{
    public class StockLocationClientDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 仓库编号
        /// </summary>
        public long LocationId { get; set; }

        /// <summary>
        /// 类型(1 仓库 2门店 3在途 4客户)
        /// </summary>
        public int LocationType { get; set; }

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
        /// 库存数量
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 入库批次
        /// </summary>
        public long BatchId { get; set; }
        /// <summary>
        /// 成本单价
        /// </summary>
        public decimal CostPrice { get; set; }
        /// <summary>
        /// 成本总价
        /// </summary>
        public decimal TotalCost { get; set; }
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
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);

        public long FromStockId { get; set; }

    }

}
