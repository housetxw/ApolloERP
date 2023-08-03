using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
    public class AllotProductDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 铺货单号
        /// </summary>
        public long TaskId { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        public int AvailableNum { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 状态(1新建 2已审核 3已取消 4已发出 5已驳回)
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// 批次
        /// </summary>
        public long BatchId { get; set; }
        /// <summary>
        /// 外联批次
        /// </summary>        
        public long RefBatchId { get; set; }
        /// <summary>
        /// 货主
        /// </summary>
        public long OwnerId { get; set; }
        /// <summary>
        /// 成本
        /// </summary>
        public decimal CostPrice { get; set; }
        /// <summary>
        /// 库存单号
        /// </summary>
        public long StockId { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; } = string.Empty;
        public sbyte IsDeleted { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 驳回人
        /// </summary>
        public string RejectBy { get; set; } = string.Empty;
        /// <summary>
        /// 驳回理由
        /// </summary>
        public string RejectReason { get; set; } = string.Empty;
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