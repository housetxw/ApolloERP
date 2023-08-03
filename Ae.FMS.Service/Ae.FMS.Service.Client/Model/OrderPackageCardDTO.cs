using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Client.Model
{
    public class OrderPackageCardDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 核销规则ID
        /// </summary>
        public long RuleId { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// 套餐卡名称
        /// </summary>
        public string PackageName { get; set; } = string.Empty;
        /// <summary>
        /// 用户手机号
        /// </summary>
        public string UserPhone { get; set; } = string.Empty;
        /// <summary>
        /// 订单号
        /// </summary>
        public string SourceOrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 核销服务商品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 产品id
        /// </summary>
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 均摊价格
        /// </summary>
        public decimal AvgPrice { get; set; }
        /// <summary>
        /// 基数总价格
        /// </summary>
        public decimal BasePrice { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 核销码（RGHX*****）
        /// </summary>
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// 渠道（RG总部）
        /// </summary>
        public string Channel { get; set; } = string.Empty;
        /// <summary>
        /// 状态（0未使用 1已使用 2已过期）
        /// </summary>
        public sbyte Status { get; set; }
        /// <summary>
        /// 起始有效时间
        /// </summary>
        public DateTime StartValidTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 截止有效时间
        /// </summary>
        public DateTime EndValidTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 核销生成的订单号
        /// </summary>
        public string VerifyOrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 核销门店ID
        /// </summary>
        public long VerifyShopId { get; set; }
        /// <summary>
        /// 核销时间
        /// </summary>
        public DateTime VerifyTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识
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
    }
}
