using Ae.B.Product.Api.Client.Model.Coupon;
using Ae.B.Product.Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.Coupon
{
    public class AddCouponRuleClientRequest
    {
        /// <summary>
        /// 优惠券规则名称（满减券¥20满0元可用）
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 优惠券显示名称（优惠券）
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 分类（0未设置 1总部优惠券）
        /// </summary>
        public CouponCategory Category { get; set; }

        /// <summary>
        /// 类型（0未设置 1满减券 2实付券）
        /// </summary>
        public CouponType Type { get; set; }

        /// <summary>
        /// 分类（0未设置 1通用券 2线上券 3门店券）
        /// </summary>
        public CouponRangeType RangeType { get; set; }

        /// <summary>
        /// 面值
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// 使用级别阈值
        /// </summary>
        public decimal Threshold { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 支付方式（0未设置 1线上支付 2到店支付）
        /// </summary>
        public PayMethod PayMethod { get; set; }

        /// <summary>
        /// 适用门店类型（位或运算结果）
        /// </summary>
        public int ShopType { get; set; }

        /// <summary>
        /// 门店配置
        /// </summary>
        public List<long> ShopIdList { get; set; } = new List<long>();

        /// <summary>
        /// 区域配置
        /// </summary>
        public List<CouponRuleRegionDto> RegionList { get; set; } = new List<CouponRuleRegionDto>();

        /// <summary>
        /// 产品类目配置
        /// </summary>
        public List<string> CategoryList { get; set; } = new List<string>();

        /// <summary>
        /// 产品品牌配置
        /// </summary>
        public List<string> BrandList { get; set; } = new List<string>();

        /// <summary>
        /// 产品Pid配置
        /// </summary>
        public List<string> PidList { get; set; } = new List<string>();

        /// <summary>
        /// 使用规则描述
        /// </summary>
        public string UseRuleDesc { get; set; } = string.Empty;

        /// <summary>
        /// 有效期开始类型（0未设置 1领取当天生效 2指定开始日期）
        /// </summary>
        public ValidStartType ValidStartType { get; set; }

        /// <summary>
        /// 有效期结束类型（0未设置 1持续指定天数 2指定截止日期）
        /// </summary>
        public ValidEndType ValidEndType { get; set; }

        /// <summary>
        /// 有效时长天数
        /// </summary>
        public int ValidDays { get; set; }

        /// <summary>
        /// 最早使用日期
        /// </summary>
        public DateTime? EarliestUseDate { get; set; }

        /// <summary>
        /// 最晚使用日期
        /// </summary>
        public DateTime? LatestUseDate { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
    }
}
