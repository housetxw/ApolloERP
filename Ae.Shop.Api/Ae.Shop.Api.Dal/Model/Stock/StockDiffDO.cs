using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Dal.Model
{
    public class StockDiffDO
    {
        /// <summary>
        /// 盘库产品编号
        /// </summary>
        public long Id { get; set; }

        public string SourceType { get; set; }
        public long WarehouseId { get; set; }
        public string WarehouseName { get; set; } = string.Empty;

        public string PlanNo { get; set; }

        public string PlanName { get; set; }

        public string PlanType { get; set; }

        /// <summary>
        /// 计划编号
        /// </summary>
        public long PlanId { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// 系统数量
        /// </summary>
        public int SystemNum { get; set; }
        /// <summary>
        /// 盘点数量
        /// </summary>
        public int StorageNum { get; set; }
        /// <summary>
        /// 差异数量
        /// </summary>
        public int DiffNum { get; set; }
        /// <summary>
        /// 盘点人
        /// </summary>
        public string OperateBy { get; set; } = string.Empty;
        /// <summary>
        /// 盘点时间
        /// </summary>
        public DateTime OperateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// 处理人
        /// </summary>
        public string DealBy { get; set; } = string.Empty;
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime DealTime { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 处理备注
        /// </summary>
        public string DealRemark { get; set; }

        /// <summary>
        /// 处理方式
        /// </summary>
        public string DealType { get; set; } = string.Empty;


        public string ProductType { get; set; }

        public string Unit { get; set; } = string.Empty;
    }
}
