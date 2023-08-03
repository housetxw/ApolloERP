using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
    public class StockInoutItemDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 出入库编号
        /// </summary>
        public long InoutId { get; set; }
        /// <summary>
        /// 关联子单号  采购子单号/盘库计划子单号
        /// </summary>
        public long ReleationId { get; set; } = 0;
        /// <summary>
        /// 商品名称Id
        /// </summary>
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 批次编号
        /// </summary>
        public string BatchNo { get; set; } = string.Empty;
        /// <summary>
        /// 供应商编号
        /// </summary>
        public long SupplierId { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName { get; set; } = string.Empty;
        /// <summary>
        /// 出入库总数
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 已入数量
        /// </summary>
        public int ActualNum { get; set; }
        /// <summary>
        /// 良品数
        /// </summary>
        public int GoodNum { get; set; }
        /// <summary>
        /// 不良品数
        /// </summary>
        public int BadNum { get; set; }
        /// <summary>
        /// 成本单价
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// 单位文本
        /// </summary>
        public string UomText { get; set; } = string.Empty;
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; } = string.Empty;
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

        /// <summary>
        /// 差异数
        /// </summary>
        public int DiffNum { get; set; }

        /// <summary>
        /// 可用数
        /// </summary>
        public int AvailableNum { get; set; }
    }

}
