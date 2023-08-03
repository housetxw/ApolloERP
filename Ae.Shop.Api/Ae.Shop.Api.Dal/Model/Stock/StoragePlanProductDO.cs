using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.Shop.Api.Dal.Model
{
    [Table("storage_plan_product")]
    public class StoragePlanProductDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 计划编号
        /// </summary>
        [Column("plan_id")]
        public long PlanId { get; set; }

        /// <summary>
        /// 来源编号 盘库产品主键
        /// 产品重盘后生成新盘库产品 关联源单号
        /// </summary>
        [Column("source_id")]
        public long SourceId { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        [Column("product_id")]
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        [Column("product_name")]
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 产品分类
        /// </summary>
        [Column("product_type")]
        public string ProductType { get; set; } = string.Empty;
        /// <summary>
        /// 单位
        /// </summary>
        [Column("unit")]
        public string Unit { get; set; } = string.Empty;
        /// <summary>
        /// 系统数量
        /// </summary>
        [Column("system_num")]
        public int SystemNum { get; set; }
        /// <summary>
        /// 盘点数量
        /// </summary>
        [Column("storage_num")]
        public int StorageNum { get; set; }
        /// <summary>
        /// 差异数量
        /// </summary>
        [Column("diff_num")]
        public int DiffNum { get; set; }
        /// <summary>
        /// 盘点人
        /// </summary>
        [Column("operate_by")]
        public string OperateBy { get; set; } = string.Empty;
        /// <summary>
        /// 盘点时间
        /// </summary>
        [Column("operate_time")]
        public DateTime OperateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 处理人
        /// </summary>
        [Column("deal_by")]
        public string DealBy { get; set; } = string.Empty;
        /// <summary>
        /// 处理时间
        /// </summary>
        [Column("deal_time")]
        public DateTime DealTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 处理方式
        /// </summary>
        [Column("deal_type")]
        public string DealType { get; set; } = string.Empty;
        /// <summary>
        /// 处理说明
        /// </summary>
        [Column("deal_remark")]
        public string DealRemark { get; set; } = string.Empty;
        /// <summary>
        /// 状态
        /// </summary>
        [Column("status")]
        public string Status { get; set; } = string.Empty;
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
    }
}