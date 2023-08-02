using Ae.B.Product.Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model.Coupon
{
    public class CouponRuleDto
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
        /// 发行分类（0-未设置 1-总部发行 2-地方发行）
        /// </summary>
        public CouponCategory Category { get; set; }

        /// <summary>
        /// 类型（0未设置  1满减券  2实付券）
        /// </summary>
        public CouponType Type { get; set; }

        /// <summary>
        /// 券使用范围（0未设置 1线上线下通用 2只限线上使用 3只限线下使用）
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
        /// 是否根据门店区域配置
        /// </summary>
        public bool IsByShopRegion { get; set; }

        /// <summary>
        /// 区域配置
        /// </summary>
        public List<CouponRuleRegionDto> RegionList { get; set; } = new List<CouponRuleRegionDto>();

        /// <summary>
        /// 是否根据门店类型配置
        /// </summary>
        public bool IsByShopType { get; set; }

        /// <summary>
        /// 门店类型
        /// </summary>
        public int ShopType { get; set; }

        /// <summary>
        /// 是否根据门店Id配置
        /// </summary>
        public bool IsByShopId { get; set; }

        /// <summary>
        /// 门店配置
        /// </summary>
        public List<long> ShopIdList { get; set; } = new List<long>();

        /// <summary>
        /// 是否根据产品大类配置
        /// </summary>
        public bool IsByProductCategory { get; set; }

        /// <summary>
        /// 产品类目配置
        /// </summary>
        public List<string> CategoryList { get; set; } = new List<string>();

        /// <summary>
        /// 是否根据产品品牌
        /// </summary>
        public bool IsByProductBrand { get; set; }

        /// <summary>
        /// 产品品牌配置
        /// </summary>
        public List<string> BrandList { get; set; } = new List<string>();

        /// <summary>
        /// 是否根据产品Pid
        /// </summary>
        public bool IsByPid { get; set; }

        /// <summary>
        /// 产品Pid配置
        /// </summary>
        public List<string> PidList { get; set; } = new List<string>();

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
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 规格区域配置
    /// </summary>
    public class CouponRuleRegionDto
    {
        /// <summary>
        /// 省Id
        /// </summary>
        public string ProvinceId { get; set; } = string.Empty;

        /// <summary>
        /// 市Id
        /// </summary>
        public string CityId { get; set; } = string.Empty;

        /// <summary>
        /// 区Id
        /// </summary>
        public string DistrictId { get; set; } = string.Empty;

        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; } = string.Empty;

        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// 区
        /// </summary>
        public string District { get; set; } = string.Empty;
    }
}
