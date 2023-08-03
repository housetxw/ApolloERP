using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Model.Coupon
{
    public class CouponActivityDTO
    {
        /// <summary>
        /// 自增主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 活动名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 规则Id
        /// </summary>
        public long RuleId { get; set; }
        /// <summary>
        /// 渠道
        /// </summary>
        public sbyte Channel { get; set; }
        /// <summary>
        /// 状态（0待发布 1可领取 2暂停领取 3已作废）
        /// </summary>
        public sbyte Status { get; set; }
        /// <summary>
        /// 是否仅新用户可领取
        /// </summary>
        public sbyte IsNewUserOnly { get; set; }
        /// <summary>
        /// 单用户最大领取数量（0为未设置）
        /// </summary>
        public int MaxNumPerUser { get; set; }
        /// <summary>
        /// 是否是积分兑换活动（默认0否）
        /// </summary>
        public sbyte IsIntegralExchange { get; set; }
        /// <summary>
        /// 需要积分类型（0未设置 1鸡蛋 2鹅蛋）
        /// </summary>
        public sbyte NeedIntegralType { get; set; }
        /// <summary>
        /// 需要积分数量
        /// </summary>
        public int NeedIntegralNum { get; set; }
        /// <summary>
        /// 发放总数
        /// </summary>
        public int TotalNum { get; set; }
        /// <summary>
        /// 领取总数
        /// </summary>
        public int ReceiveNum { get; set; }
        /// <summary>
        /// 访问地址
        /// </summary>
        public string Url { get; set; } = string.Empty;
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; } = new DateTime(1900, 1, 1);
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
