using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Client.Model.WMS
{
    /// <summary>
    /// 
    /// </summary>
    public class GetWareHouseTransferProductClientDTO
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
        /// 转移子单号
        /// </summary>
        public long TransferProductId { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 库存单号
        /// </summary>
        public long StockId { get; set; }
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
        /// 周期
        /// </summary>
        public string WeekYear { get; set; } = string.Empty;
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
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
