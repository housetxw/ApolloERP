using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Client.Model
{
    public class OrderCouponDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 订单Id
        /// </summary>
        public long OrderId { get; set; }

        public string OrderNo { get; set; }
        /// <summary>
        /// 用户使用的优惠券Id
        /// </summary>
        public long UserCouponId { get; set; }
        /// <summary>
        /// 使用规则Id
        /// </summary>
        public long CouponRuleId { get; set; }
        /// <summary>
        /// 优惠券标题名称
        /// </summary>
        public string CouponName { get; set; } = string.Empty;
        /// <summary>
        /// 优惠券金额
        /// </summary>
        public decimal CouponAmount { get; set; }

        /// <summary>
        /// 发行分类（0-未设置 1-总部发行 2-地方发行）
        /// </summary>
        public sbyte Category { get; set; }

        /// <summary>
        /// 类型（0未设置  1满减券  2实付券）
        /// </summary>
        public sbyte Type { get; set; }

        /// <summary>
        /// 券使用范围（0未设置 1线上线下通用 2只限线上使用 3只限线下使用）
        /// </summary>
        public sbyte RangeType { get; set; }
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
