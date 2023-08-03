using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Model
{
    public class StockTranctionClientDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 关联单号
        /// </summary>
        public string TransferObjectNo { get; set; }
        /// <summary>
        /// 关联类型(订单号,采购单号,退货单号 调拨单)
        /// </summary>
        public string TransferType { get; set; } = string.Empty;
        /// <summary>
        /// 转移起点
        /// </summary>
        public long TransferFrom { get; set; }
        /// <summary>
        /// 转移起点名称
        /// </summary>
        public string TransferFromName { get; set; } = string.Empty;
        /// <summary>
        /// 转移终点
        /// </summary>
        public long TransferTo { get; set; }
        /// <summary>
        /// 转移终点名称
        /// </summary>
        public string TransferToName { get; set; } = string.Empty;
        /// <summary>
        /// 转移库存起点
        /// </summary>
        public long FromStockId { get; set; }
        /// <summary>
        /// 转移库存终点
        /// </summary>
        public long ToStockId { get; set; }
        /// <summary>
        /// 转移数量
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 转移操作人
        /// </summary>
        public string TransferOperator { get; set; } = string.Empty;
        /// <summary>
        /// 转移操作时间
        /// </summary>
        public DateTime TransferTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);

        public string Remark { get; set; } = string.Empty;
    }

}
