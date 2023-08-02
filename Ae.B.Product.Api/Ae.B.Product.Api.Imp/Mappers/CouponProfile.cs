using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Ae.B.Product.Api.Client.Model.Coupon;
using Ae.B.Product.Api.Client.Request.Coupon;
using Ae.B.Product.Api.Common.Extension;
using Ae.B.Product.Api.Common.Util;
using Ae.B.Product.Api.Core.Model.Coupon;
using Ae.B.Product.Api.Core.Request.Coupon;

namespace Ae.B.Product.Api.Imp.Mappers
{
    public class CouponProfile: Profile
    {
        public CouponProfile()
        {
            CreateMap<UserCouponPageByUserIdRequest, UserCouponPageByUserIdClientRequest>();
            CreateMap<UserCouponPageResByUserIdDto, UserCouponPageResByUserIdVo>();
            CreateMap<CouponRuleDto, CouponRuleVo>()
                .ForMember(d => d.Category, d => d.MapFrom(s => (sbyte) s.Category))
                .ForMember(d => d.CategoryName, d => d.MapFrom(s => EnumExtension.GetEnumDescription(s.Category)))
                .ForMember(d => d.Type, d => d.MapFrom(s => (sbyte) s.Type))
                .ForMember(d => d.TypeName, d => d.MapFrom(s => EnumExtension.GetEnumDescription(s.Type)))
                .ForMember(d => d.RangeType, d => d.MapFrom(s => (sbyte) s.RangeType))
                .ForMember(d => d.RangeTypeName, d => d.MapFrom(s => EnumExtension.GetEnumDescription(s.RangeType)))
                .ForMember(d => d.PayMethod, d => d.MapFrom(s => (sbyte) s.PayMethod))
                .ForMember(d => d.PayMethodName, d => d.MapFrom(s => EnumExtension.GetEnumDescription(s.PayMethod)))
                .ForMember(d => d.ValidStartType, d => d.MapFrom(s => (sbyte) s.ValidStartType))
                .ForMember(d => d.ValidStartTypeName,
                    d => d.MapFrom(s => EnumExtension.GetEnumDescription(s.ValidStartType)))
                .ForMember(d => d.ValidEndType, d => d.MapFrom(s => (sbyte) s.ValidEndType))
                .ForMember(d => d.ValidEndTypeName,
                    d => d.MapFrom(s => EnumExtension.GetEnumDescription(s.ValidEndType)))
                .ForMember(d => d.ValidDays,
                    d => d.MapFrom(s => s.ValidDays > 0 ? s.ValidDays.ToString() : string.Empty))
                .ForMember(d => d.EarliestUseDate,
                    d => d.MapFrom(s =>
                        s.EarliestUseDate.CompareTo(new DateTime(1900, 1, 1)) > 0
                            ? s.EarliestUseDate.ToString("yyyy-MM-dd HH:mm:ss")
                            : string.Empty))
                .ForMember(d => d.LatestUseDate,
                    d => d.MapFrom(s =>
                        s.LatestUseDate.CompareTo(new DateTime(1900, 1, 1)) > 0
                            ? s.LatestUseDate.ToString("yyyy-MM-dd HH:mm:ss")
                            : string.Empty))
                .ForMember(d => d.ShopTypeName, d => d.MapFrom(s => ShopTypeUtil.ConvertShopType(s.ShopType)))
                .ForMember(d => d.CategoryList, d => d.MapFrom(s => ConvertIntList(s.CategoryList)));
            CreateMap<CouponRuleRegionDto, CouponRuleRegionVo>();

            CreateMap<CouponActivityDto, CouponActivityVo>()
                .ForMember(d => d.Status, d => d.MapFrom(s => (int) s.Status))
                .ForMember(d => d.StatusName, d => d.MapFrom(s => EnumExtension.GetEnumDescription(s.Status)));

            CreateMap<CouponActivityDetailDto, CouponActivityDetailVo>()
                .ForMember(d => d.StatusName, d => d.MapFrom(s => EnumExtension.GetEnumDescription(s.Status)));

            CreateMap<UserCouponGrantRecordDto, UserCouponGrantRecordVo>()
                .ForMember(d => d.GrantMethodName, d => d.MapFrom(s => EnumExtension.GetEnumDescription(s.GrantMethod)))
                .ForMember(d => d.StatusName, d => d.MapFrom(s => EnumExtension.GetEnumDescription(s.Status)));

            CreateMap<GiftCouponRuleDetailDto, GiftCouponRuleDetailVo>().ReverseMap();
            CreateMap<CouponActDto, CouponActVo>().ReverseMap();
            CreateMap<CouponProductDto, CouponProductVo>().ReverseMap();

            CreateMap<GiftCouponRuleDto, GiftCouponRuleVo>()
                .ForMember(d => d.ChannelName, d => d.MapFrom(s => s.Channel.GetEnumDescription()));
        }

        private List<int> ConvertIntList(List<string> strLis)
        {
            if (strLis == null)
            {
                return new List<int>();
            }

            return strLis.Select(_ => Convert.ToInt32(_)).ToList();
        }
    }
}
