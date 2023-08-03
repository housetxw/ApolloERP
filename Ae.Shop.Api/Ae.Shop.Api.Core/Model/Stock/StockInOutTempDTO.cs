using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
   public class StockInOutTempDTO
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
        /// 关联单号  采购总单号/盘库计划编号 
        /// </summary>
        public string SourceInventoryNo { get; set; } = string.Empty;
        /// <summary>
        /// 关联子单号  采购子单号/盘库计划子单号
        /// </summary>
        public long ReleationId { get; set; } = 0;

        public long LocationId { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string LocationName { get; set; }

        /// <summary>
        /// 操作类型(入库 出库)
        /// </summary>
        public string OperationType { get; set; }

        /// <summary>
        /// 来源类型(采购入库 盘盈入库 盘亏出库 订单安装 调拨出库)
        /// </summary>
        public string SourceType { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
        public string CreateBy { get; set; } = string.Empty;

        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 组合的操作类型
        /// </summary>
        public string Type { get; set; }
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
        /// 出入库总数
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 已入数量
        /// </summary>
        public int ActualNum { get; set; }
        /// <summary>
        /// 单位文本
        /// </summary>
        public string UomText { get; set; } = string.Empty;
    }
}
