using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;
namespace Ae.Shop.Api.Dal.Model
{
    [Table("stock_in")]
    public class StockInDO
    {
        /// <summary>
        /// 序号
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 来源单据Id
        /// </summary>
        [Column("source_inventory_id")]
        public long SourceInventoryId { get; set; }
        /// <summary>
        /// 来源单号
        /// </summary>
        [Column("source_inventory_no")]
        public string SourceInventoryNo { get; set; } = string.Empty;
        /// <summary>
        /// 入库单号
        /// </summary>
        [Column("stock_in_no")]
        public string StockInNo { get; set; } = string.Empty;
        /// <summary>
        /// 门店id
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }
        /// <summary>
        /// 开始入库日期
        /// </summary>
        [Column("stock_in_start_date")]
        public DateTime StockInStartDate { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 入库结束时间
        /// </summary>
        [Column("stock_in_finish_date")]
        public DateTime StockInFinishDate { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 入库类型(盘盈入库、采购入库、退货入库、其他入库）
        /// </summary>
        [Column("stock_in_type")]
        public string StockInType { get; set; } = string.Empty;
        /// <summary>
        /// 入库类型文本
        /// </summary>
        [Column("sotck_in_type_text")]
        public string SotckInTypeText { get; set; } = string.Empty;
        /// <summary>
        /// 入库总数量
        /// </summary>
        [Column("stock_in_num")]
        public long StockInNum { get; set; }
        /// <summary>
        /// 状态（全部入库、部分入库）
        /// </summary>
        [Column("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// 描述信息
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建用户ID
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
