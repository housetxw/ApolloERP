using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.Shop.Api.Dal.Model
{
    [Table("stock_out")]
    public class StockOutDO
    {
        /// <summary>
        /// 序号
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 门店Id
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }
        /// <summary>
        /// 出库单号
        /// </summary>
        [Column("out_stock_code")]
        public string OutStockCode { get; set; } = string.Empty;
        /// <summary>
        /// 出库类型（order）
        /// </summary>
        [Column("out_stock_type")]
        public string OutStockType { get; set; } = string.Empty;
        /// <summary>
        /// 出库类型文本
        /// </summary>
        [Column("out_stock_type_text")]
        public string OutStockTypeText { get; set; } = string.Empty;
        /// <summary>
        /// 关联的单号
        /// </summary>
        [Column("source_no")]
        public string SourceNo { get; set; } = string.Empty;
        /// <summary>
        /// 出库数量
        /// </summary>
        [Column("num")]
        public long Num { get; set; }
        /// <summary>
        /// 备注描述
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 出库状态(失败、成功、部分成功）
        /// </summary>
        [Column("status")]
        public string Status { get; set; } = string.Empty;
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
