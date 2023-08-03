using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;


namespace Ae.Shop.Api.Dal.Model
{
    [Table("tech_claim_product")]

    public class TechClaimProductDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 记录单号
        /// </summary>
        [Column("record_id")]
        public long RecordId { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        [Column("product_id")]
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 产名称
        /// </summary>
        [Column("product_name")]
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 产品类型
        /// </summary>
        [Column("category_name")]
        public string CategoryName { get; set; } = string.Empty;
        /// <summary>
        /// 结算成本价
        /// </summary>
        [Column("cost_price")]
        public decimal CostPrice { get; set; }
        /// <summary>
        /// 领取数量
        /// </summary>
        [Column("num")]
        public int Num { get; set; }
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
        public byte IsDeleted { get; set; }
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
