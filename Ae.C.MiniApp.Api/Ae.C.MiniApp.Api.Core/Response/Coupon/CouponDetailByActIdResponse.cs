using Ae.C.MiniApp.Api.Core.Model.Coupon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response.Coupon
{
    /// <summary>
    /// CouponDetailByActIdResponse
    /// </summary>
    public class CouponDetailByActIdResponse
    {
        /// <summary>
        /// 是否可领取
        /// </summary>
        public bool CanDraw { get; set; }

        /// <summary>
        /// 不可领取原因
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否仅新用户可领取
        /// </summary>
        public bool IsNewUserOnly { get; set; }

        /// <summary>
        /// 状态（0未发布 1可领取 2暂停领取 3已作废）
        /// </summary>
        public CouponActivityStatue Status { get; set; }

        /// <summary>
        /// 渠道（0未设置 1小程序 2Android-App 3iOS-App 4S网站 5官网）
        /// </summary>
        public CouponActivityChannel Channel { get; set; }

        /// <summary>
        /// 单用户最大领取数量（0为未设置）
        /// </summary>
        public int MaxNumPerUser { get; set; }

        /// <summary>
        /// 发放总数
        /// </summary>
        public int TotalNum { get; set; }

        /// <summary>
        /// 领取总数
        /// </summary>
        public int ReceiveNum { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 券列表
        /// </summary>
        public List<ActivityCouponVo> CouponList { get; set; }
    }

    /// <summary>
    /// ActivityCouponVo
    /// </summary>
    public class ActivityCouponVo
    {
        /// <summary>
        /// 优惠券Id
        /// </summary>
        public long CouponId { get; set; }

        /// <summary>
        /// 优惠券规则名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 优惠券展示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 分类 （0未设置 1优惠券）
        /// </summary>
        public CouponCategory Category { get; set; }

        /// <summary>
        /// 类型（0未设置  1满减券  2实付券）
        /// </summary>
        public CouponType Type { get; set; }

        /// <summary>
        /// 类型适用范围（0未设置 1通用券 2线上券  3门店券）
        /// </summary>
        public CouponRangeType RangeType { get; set; }

        /// <summary>
        /// 面值
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// 阀值
        /// </summary>
        public decimal Threshold { get; set; }

        /// <summary>
        /// 图片Url
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 支付方式（0未设置  1线上支付  2到店支付）
        /// </summary>
        public PayMethod PayMethod { get; set; }

        /// <summary>
        /// 适用规则描述
        /// </summary>
        public string UseRuleDesc { get; set; }

        /// <summary>
        /// 有效期开始类型（0未设置  1领取当天生效  2指定开始日期）
        /// </summary>
        public ValidStartType ValidStartType { get; set; }

        /// <summary>
        /// 有效期结束类型（0未设置  1持续指定天数  2指定截止日期）
        /// </summary>
        public ValidEndType ValidEndType { get; set; }

        /// <summary>
        /// 有效时长天数
        /// </summary>
        public int ValidDays { get; set; }

        /// <summary>
        /// 最早开始日期
        /// </summary>
        public DateTime EarliestUseDate { get; set; }

        /// <summary>
        /// 最晚使用日期
        /// </summary>
        public DateTime LatestUseDate { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }
    }
}
