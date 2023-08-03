using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
    public class StockInOutDTO
    {
        /// <summary>
        /// 出入库单号 主键
        /// </summary>
        public int StockId { get; set; }

        /// <summary>
        /// 关联单号  采购总单号/盘库计划编号 
        /// </summary>
        public string SourceInventoryNo { get; set; } = string.Empty;

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

        public string CreateBy { get; set; } = string.Empty;

        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 组合的操作类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 出入库时间
        /// </summary>
        public DateTime OperateTime { get; set; }

        /// <summary>
        /// 领用人
        /// </summary>
        public string Operator { get; set; } = string.Empty;

        public string Remark { get; set; } = string.Empty;

        public string Status{get;set;}=string.Empty;

        public List<StockInoutItemDTO> StockItems { get; set; } = new List<StockInoutItemDTO>();
    }
}
