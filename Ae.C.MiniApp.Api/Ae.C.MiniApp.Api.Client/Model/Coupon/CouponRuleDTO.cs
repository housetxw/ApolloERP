using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Model.Coupon
{
    public class CouponRuleDTO
    {
        /// <summary>
        /// 自增主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 优惠券规则名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 优惠券显示名称
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// 类型（0未设置 1满减券 2实付券）
        /// </summary>
        public sbyte Type { get; set; }
        /// <summary>
        /// 分类（0未设置 1通用券 2线上券 3门店券）
        /// </summary>
        public sbyte Category { get; set; }
        /// <summary>
        /// 面值
        /// </summary>
        public decimal Value { get; set; }
        /// <summary>
        /// 使用级别阈值
        /// </summary>
        public decimal Threshold { get; set; }
        /// <summary>
        /// 支付方式（0未设置 1线上支付 2到店支付）
        /// </summary>
        public sbyte PayMethod { get; set; }
        /// <summary>
        /// 是否按产品大类
        /// </summary>
        public sbyte IsByProductType { get; set; }
        /// <summary>
        /// 是否按产品品牌
        /// </summary>
        public sbyte IsByBrand { get; set; }
        /// <summary>
        /// 是否按产品Id
        /// </summary>
        public sbyte IsByPid { get; set; }
        /// <summary>
        /// 是否按门店类型
        /// </summary>
        public sbyte IsByShopType { get; set; }
        /// <summary>
        /// 适用门店类型（位或运算结果）
        /// </summary>
        public int ShopType { get; set; }
        /// <summary>
        /// 是否按门店Id
        /// </summary>
        public sbyte IsByShopId { get; set; }
        /// <summary>
        /// 是否按门店所在区域
        /// </summary>
        public sbyte IsByShopRegion { get; set; }
        /// <summary>
        /// 使用规则描述
        /// </summary>
        public string UseRuleDesc { get; set; } = string.Empty;
        /// <summary>
        /// 有效期开始类型（0未设置 1领取当天生效 2指定开始日期）
        /// </summary>
        public sbyte ValidStartType { get; set; }
        /// <summary>
        /// 有效期结束类型（0未设置 1持续指定天数 2指定截止日期）
        /// </summary>
        public sbyte ValidEndType { get; set; }
        /// <summary>
        /// 有效时长天数
        /// </summary>
        public int ValidDays { get; set; }
        /// <summary>
        /// 最早使用日期
        /// </summary>
        public DateTime EarliestUseDate { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 最晚使用日期
        /// </summary>
        public DateTime LatestUseDate { get; set; } = new DateTime(1900, 1, 1);
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
