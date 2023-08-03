using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Dal.Model
{
    public class WarehouseTransferProductDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 转移单号
        /// </summary>
        public long TransferId { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 已收货数量
        /// </summary>
        public int ReceiveNum { get; set; }
        /// <summary>
        /// 移库费用
        /// </summary>
        public decimal TransferFee { get; set; }
        /// <summary>
        /// 成本
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }
        
        /// <summary>
        /// 供应商简称
        /// </summary>
        public string VenderShortName { get; set; } = string.Empty;
        /// <summary>
        /// 供应商编号
        /// </summary>
        public long VenderId { get; set; }
        /// <summary>
        /// 入库批次
        /// </summary>
        public long BatchId { get; set; }

        /// <summary>
        /// 货主
        /// </summary>
        public int OwnerId { get; set; }
        /// <summary>
        /// 货主名称
        /// </summary>
        public string OwnerName { get; set; } = string.Empty;

        public string UpdateBy { get; set; }
    }
}
