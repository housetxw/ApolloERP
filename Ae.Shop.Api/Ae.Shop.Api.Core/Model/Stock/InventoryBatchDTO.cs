using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
    public class InventoryBatchDTO
    {
        /// <summary>
        /// 序号
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 来源单据Id
        /// </summary>
        public long SourceInventoryId { get; set; }
        /// <summary>
        /// 来源单号
        /// </summary>
        public string SourceInventoryNo { get; set; } = string.Empty;
        /// <summary>
        /// 产品的pid
        /// </summary>
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 库存id
        /// </summary>
        public long InventoryId { get; set; }
        /// <summary>
        /// 批次编号
        /// </summary>
        public long BatchNo { get; set; }
        /// <summary>
        /// 外联批次号
        /// </summary>
        public string RefBatchNo { get; set; } = string.Empty;
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 运费
        /// </summary>
        public decimal Freight { get; set; }
        /// <summary>
        /// 成本
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// 库存总数量
        /// </summary>
        public long TotalNum { get; set; }
        /// <summary>
        /// 可用库存 要扣除锁定库存
        /// </summary>
        public long CanUseNum { get; set; }

        public int AvailableNum { get; set; }

        /// <summary>
        /// 占用量
        /// </summary>
        public int OccupyNum { get; set; }
        /// <summary>
        /// 周期
        /// </summary>
        public string WeekYear { get; set; } = string.Empty;
        /// <summary>
        /// 货主id
        /// </summary>
        public long OwnId { get; set; }
        /// <summary>
        /// 货主类型 (仓库、门店、供应商(Company,Shop))
        /// </summary>
        public string OwnType { get; set; } = string.Empty;
        /// <summary>
        /// 供应商Id
        /// </summary>
        public string SupplierId { get; set; } = string.Empty;
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName { get; set; } = string.Empty;
        /// <summary>
        /// 入库产品的属性（良品，不良品）
        /// </summary>
        public string ProductAttrType { get; set; } = string.Empty;
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
