using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.ConsumerOrder.Service.Dal.Model
{
    [Table("order_verification_code")]
    public class OrderVerificationCodeDO
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
        [Column("order_no")]
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 核销服务商品名称
        /// </summary>
        [Column("product_name")]
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 市场价
        /// </summary>
        [Column("marketing_price")]
        public decimal MarketingPrice { get; set; }
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
        /// 状态（0未使用 1已使用 2已过期）
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
