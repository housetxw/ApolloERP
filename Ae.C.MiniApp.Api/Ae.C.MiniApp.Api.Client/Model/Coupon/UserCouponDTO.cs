using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Model.Coupon
{
    public class UserCouponDTO
    {
        /// <summary>
        /// 自增主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 发放方式（0未设置 1系统自动触发 2运营手动触发 3用户自主领取 4用户积分兑换）
        /// </summary>
        public sbyte GrantMethod { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// 活动Id
        /// </summary>
        public long ActivityId { get; set; }
        /// <summary>
        /// 规则Id
        /// </summary>
        public long RuleId { get; set; }
        /// <summary>
        /// 优惠券显示名称
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// 使用状态（0未使用 1已使用 2已过期）
        /// </summary>
        public sbyte Status { get; set; }
        /// <summary>
        /// 开始有效时间
        /// </summary>
        public DateTime StartValidTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 截止有效时间
        /// </summary>
        public DateTime EndValidTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 用户领取设备IP地址
        /// </summary>
        public string UserIp { get; set; } = string.Empty;
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
