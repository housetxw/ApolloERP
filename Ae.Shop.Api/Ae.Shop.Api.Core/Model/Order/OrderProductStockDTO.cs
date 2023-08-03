using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
    public class OrderProductStockDTO
    {
        public string OrderNo { get; set; }

        /// <summary>
        /// 订单产品单号
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductId { get; set; } = string.Empty;

        /// <summary>
        /// 是否套餐产品
        /// </summary>
        public sbyte IsPackageProduct { get; set; }

        /// <summary>
        /// 套餐父产品ID
        /// </summary>
        public long? PackageId { get; set; }

        public bool IsOccupy { get; set; }

        /// <summary>
        /// 产品数量
        /// </summary>
        public int Number { get; set; }

        public string OccupyRemark { get; set; }

        public string ProductName { get; set; }


        //订单产品占用明细
        public List<OrderProductStockDetailDTO> StockDetailDTOs { get; set; } = new List<OrderProductStockDetailDTO>();

    }

    /// <summary>
    /// 子产品占用明细
    /// </summary>
    public class OrderProductStockDetailDTO
    {
        public long StockId { get; set; }
        public int OccupyNum { get; set; }

        public string WeekYear { get; set; }

        public int OwnerId { get; set; }

        public int BatchId { get; set; }

        public decimal Cost { get; set; }

        public sbyte StockType { get; set; }
    }
}
