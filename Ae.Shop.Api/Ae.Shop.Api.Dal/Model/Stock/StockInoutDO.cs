using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.Shop.Api.Dal.Model
{
    [Table("stock_inout")]
    public class StockInoutDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 关联单号 这个可能会有多个单号
        /// </summary>
        [Column("source_inventory_no")]
        public string SourceInventoryNo { get; set; } = string.Empty;
        /// <summary>
        /// 门店编号
        /// </summary>
        [Column("location_id")]
        public long LocationId { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        [Column("location_name")]
        public string LocationName { get; set; } = string.Empty;
        /// <summary>
        /// 操作类型(入库,出库)
        /// </summary>
        [Column("operation_type")]
        public string OperationType { get; set; } = string.Empty;
        /// <summary>
        /// 来源类型(采购入库 订单出库 盘盈入库 盘亏出库)
        /// </summary>
        [Column("source_type")]
        public string SourceType { get; set; } = string.Empty;
        /// <summary>
        /// 领用人
        /// </summary>
        [Column("operator")]
        public string Operator { get; set; } = string.Empty;
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
        /// 出入库时间
        /// </summary>
        [Column("operate_time")]
        public DateTime OperateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 删除标识 0未删除 1已删除
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
