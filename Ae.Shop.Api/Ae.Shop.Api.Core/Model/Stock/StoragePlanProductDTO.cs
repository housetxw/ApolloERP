using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
    public class StoragePlanProductDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
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
        /// 产品类型
        /// </summary>
        public string ProductType { get; set; } = string.Empty;

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; } = string.Empty;
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
        /// 处理方式
        /// </summary>
        public string DealType { get; set; } = string.Empty;
        /// <summary>
        /// 处理说明
        /// </summary>
        public string DealRemark { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识 0否 1是
        /// </summary>
        public sbyte IsDeleted { get; set; }
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

        public DateTime PlanTime { get; set; }
    }
}