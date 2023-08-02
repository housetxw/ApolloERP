using Ae.B.Product.Api.Core.Enums;
using Ae.B.Product.Api.Core.Model.Coupon;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.Coupon
{
    /// <summary>
    /// 添加优惠券配置
    /// </summary>
    public class AddCouponRuleRequest
    {
        /// <summary>
        /// 优惠券规则名称（满减券¥20满0元可用）
        /// </summary>
        [Required(ErrorMessage = "优惠券规则名称不能为空")]
        public string Name { get; set; }

        /// <summary>
        /// 优惠券显示名称（电池优惠券）
        /// </summary>
        [Required(ErrorMessage = "优惠券显示名称不能为空")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 分类（0未设置 1总部优惠券）
        /// </summary>
        public CouponCategory Category { get; set; }

        /// <summary>
        /// 类型（0未设置 1满减券 2实付券）
        /// </summary>
        public Enums.CouponType Type { get; set; }

        /// <summary>
        /// 分类（0未设置 1通用券 2线上券 3门店券）
        /// </summary>
        public CouponRangeType RangeType { get; set; }

        /// <summary>
        /// 面值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 使用级别阈值
        /// </summary>
        public string Threshold { get; set; }

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
        public string ShopIdList { get; set; } = string.Empty;

        /// <summary>
        /// 区域配置
        /// </summary>
        public List<CouponRuleRegionVo> RegionList { get; set; } = new List<CouponRuleRegionVo>();

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
        public string PidList { get; set; } = string.Empty;

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
        public string ValidDays { get; set; }

        /// <summary>
        /// 最早使用日期
        /// </summary>
        public string EarliestUseDate { get; set; }

        /// <summary>
        /// 最晚使用日期
        /// </summary>
        public string LatestUseDate { get; set; }
    }
}
