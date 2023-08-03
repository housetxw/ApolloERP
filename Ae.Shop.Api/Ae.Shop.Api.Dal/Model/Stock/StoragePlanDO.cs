using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.Shop.Api.Dal.Model
{
    [Table("storage_plan")]
    public class StoragePlanDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 门店编号
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        [Column("shop_name")]
        public string ShopName { get; set; } = string.Empty;
        /// <summary>
        /// 仓库编号
        /// </summary>
        [Column("warehouse_id")]
        public long WarehouseId { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        [Column("warehouse_name")]
        public string WarehouseName { get; set; } = string.Empty;
        /// <summary>
        /// 计划编号
        /// </summary>
        [Column("plan_no")]
        public string PlanNo { get; set; } = string.Empty;
        /// <summary>
        /// 计划名称
        /// </summary>
        [Column("plan_name")]
        public string PlanName { get; set; } = string.Empty;
        /// <summary>
        /// 产品来源(总部产品，外采产品)
        /// </summary>
        [Column("source_type")]
        public string SourceType { get; set; } = string.Empty;
        /// <summary>
        /// 盘点类型(指定产品 全盘)
        /// </summary>
        [Column("type")]
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// 执行状态(新建 盘点中 盘点完成 差异处理中)
        /// </summary>
        [Column("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// 计划时间
        /// </summary>
        [Column("plan_time")]
        public DateTime PlanTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 实际完成时间
        /// </summary>
        [Column("actual_time")]
        public DateTime ActualTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识 0否 1是
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 更新类型 1.更新状态 2.状态+实际完成时间
        /// </summary>
        [NotMapped]
        public int UpdateType { get; set; }
    }
}