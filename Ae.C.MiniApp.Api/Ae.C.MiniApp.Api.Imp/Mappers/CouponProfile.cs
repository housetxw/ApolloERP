using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Model.Coupon;
using Ae.C.MiniApp.Api.Client.Request.Coupon;
using Ae.C.MiniApp.Api.Client.Response.Coupon;
using Ae.C.MiniApp.Api.Common.Extension;
using Ae.C.MiniApp.Api.Core.Request.Coupon;
using Ae.C.MiniApp.Api.Core.Response.Coupon;

namespace Ae.C.MiniApp.Api.Imp.Mappers
{
    public class CouponProfile : Profile
    {
        public CouponProfile()
        {
            #region UserCoupon

            CreateMap<UserCouponPageReqByUserIdVO, UserCouponPageReqByUserIdDTO>().ReverseMap();
            CreateMap<UserCouponPageResByUserIdDTO, UserCouponPageResByUserIdVO>().ReverseMap();
            CreateMap<ApiPagedResult<UserCouponPageResByUserIdDTO>, ApiPagedResult<UserCouponPageResByUserIdVO>>().ReverseMap();

            CreateMap<ExchangeCouponReqByCodeVO, ExchangeCouponReqByCodeDTO>().ReverseMap();
            CreateMap<ApiRequest<ExchangeCouponReqByCodeVO>, ApiRequest<ExchangeCouponReqByCodeDTO>>().ReverseMap();

            CreateMap<IntegralExchangeCouponReqVO, IntegralExchangeCouponReqDTO>().ReverseMap();
            CreateMap<ApiRequest<IntegralExchangeCouponReqVO>, ApiRequest<IntegralExchangeCouponReqDTO>>().ReverseMap();


            #endregion UserCoupon

            #region CouponActivity

            //CreateMap<CouponActivityPageReqByChannelVO, CouponActivityPageReqByChannelDTO>().ReverseMap();
            CreateMap<CouponActivityPageReqByChannelVOForDTO, CouponActivityPageReqByChannelDTO>().ReverseMap();
            CreateMap<CouponActivityPageResDTO, CouponActivityPageResVO>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.GetDescription()));
            CreateMap<ApiPagedResult<CouponActivityPageResDTO>, ApiPagedResult<CouponActivityPageResVO>>().ReverseMap();


            CreateMap<CouponDetailByActIdClientResponse, CouponDetailByActIdResponse>().ReverseMap();
            CreateMap<ActivityCouponDto, ActivityCouponVo>().ReverseMap();

            CreateMap<GetCouponListByProductClientResponse, GetCouponListByProductResponse>().ReverseMap();
            CreateMap<UserCouponListForProductDto, UserCouponListForProduct>().ReverseMap();

            #endregion CouponActivity

            #region CouponRule

            #endregion CouponRule

        }
    }
}
