using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;
namespace Ae.Order.Service.Dal.Model
{
    [Table("order_package_card")]
    public class OrderPackageCardDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 核销规则ID
        /// </summary>
        [Column("rule_id")]
        public long RuleId { get; set; }
        /// <summary>
        /// 套餐卡名称
        /// </summary>
        [Column("package_name")]
        public string PackageName { get; set; } = string.Empty;
        /// <summary>
        /// 用户ID
        /// </summary>
        [Column("user_id")]
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// 用户手机号
        /// </summary>
        [Column("user_phone")]
        public string UserPhone { get; set; } = string.Empty;
        /// <summary>
        /// 订单号
        /// </summary>
        [Column("source_order_no")]
        public string SourceOrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 核销服务商品名称
        /// </summary>
        [Column("product_name")]
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 产品id
        /// </summary>
        [Column("product_id")]
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 单价
        /// </summary>
        [Column("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [Column("num")]
        public int Num { get; set; }
        /// <summary>
        /// 核销码（RGHX*****）
        /// </summary>
        [Column("code")]
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// 渠道（RG总部）
        /// </summary>
        [Column("channel")]
        public string Channel { get; set; } = string.Empty;
        /// <summary>
        /// 状态（0未使用 1已使用 2已过期 3作废）
        /// </summary>
        [Column("status")]
        public sbyte Status { get; set; }
        /// <summary>
        /// 起始有效时间
        /// </summary>
        [Column("start_valid_time")]
        public DateTime StartValidTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 截止有效时间
        /// </summary>
        [Column("end_valid_time")]
        public DateTime EndValidTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 核销生成的订单号
        /// </summary>
        [Column("verify_order_no")]
        public string VerifyOrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 核销门店ID
        /// </summary>
        [Column("verify_shop_id")]
        public long VerifyShopId { get; set; }
        /// <summary>
        /// 核销时间
        /// </summary>
        [Column("verify_time")]
        public DateTime VerifyTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识
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
