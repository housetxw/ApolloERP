using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
    public class OccupyItemDTO
    {
        /// <summary>
        /// 序号
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 占用queueId
        /// </summary>
        public long OccupyQueueId { get; set; }
        /// <summary>
        /// 来源单据Id
        /// </summary>
        public long SourceInventoryId { get; set; }
        /// <summary>
        /// 关联单号
        /// </summary>
        public string SourceInventoryNo { get; set; } = string.Empty;
        /// <summary>
        /// 订单产品单号
        /// </summary>
        public long OrderProductId { get; set; }
        /// <summary>
        /// 产品pid
        /// </summary>
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 库存id
        /// </summary>
        public long InventoryId { get; set; }
        /// <summary>
        /// 仓库id
        /// </summary>
        public long LocationId { get; set; }
        /// <summary>
        /// 占库数量
        /// </summary>
        public long Num { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNo { get; set; } = string.Empty;
        /// <summary>
        /// 外联批次单号
        /// </summary>
        public string RefBatchNo { get; set; } = string.Empty;
        /// <summary>
        /// 成本
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// 占用类型(订单，退货的应该分开)
        /// </summary>
        public string OccupyType { get; set; } = string.Empty;
        /// <summary>
        /// 货主
        /// </summary>
        public long OwnId { get; set; }
        /// <summary>
        /// 货主类型 (仓库、门店、供应商(Company,Shop))
        /// </summary>
        public string OwnType { get; set; } = string.Empty;
        /// <summary>
        /// 库存类型(1良品 2不良品)
        /// </summary>
        public sbyte StockType { get; set; }
        /// <summary>
        /// 是否有效(1有效-还在占用中 2无效-订单已发出)
        /// </summary>
        public sbyte IsValid { get; set; }
        /// <summary>
        /// 备注
        /// </summary>        
        public string Remark { get; set; }
        /// <summary>
        /// 删除标识
        /// </summary>
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建用户ID
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