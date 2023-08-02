using AutoMapper;
using ApolloErp.Web.WebApi;
using Ae.B.Order.Api.Client.Model;
using Ae.B.Order.Api.Client.Request;
using Ae.B.Order.Api.Client.Response;
using Ae.B.Order.Api.Common.Util;
using Ae.B.Order.Api.Core.Model;
using Ae.B.Order.Api.Core.Request;
using Ae.B.Order.Api.Core.Response;
using Ae.B.Order.Api.Core.Response.OrderQuery;
using System;

namespace Ae.B.Order.Api.Imp.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<MergePayNoDTO, GetOrderMergeListResponse>();
            CreateMap<OrderDetailForBossAmountInfoDTO, OrderDetailForBossAmountInfoVO>();
            CreateMap<OrderDetailForBossFinanceInfoDTO, OrderDetailForBossFinanceInfoVO>();
            CreateMap<OrderDetailForBossOrderInfoDTO, OrderDetailForBossOrderInfoVO>();

            CreateMap<OrderDetailForBossProductDTO, OrderDetailForBossProductVO>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl.AddImageDomain()));//图片绝对路径
            CreateMap<OrderDetailForBossPackageProductDTO, OrderDetailForBossPackageProductVO>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl.AddImageDomain()));//图片绝对路径

            CreateMap<OrderDetailForBossProductInfoDTO, OrderDetailForBossProductInfoVO>();
            CreateMap<OrderDetailForBossUserInfoDTO, OrderDetailForBossUserInfoVO>();
            CreateMap<GetOrderDetailRequest, GetOrderDetailForBossClientRequest>();
            CreateMap<GetOrderDetailForBossClientResponse, GetOrderDetailResponse>();

            CreateMap<MergePayNoDTO, GetOrderDetailForBossClientRequest>();
            CreateMap<GetOrderDetailForBossClientResponse, MergePayNoDTO>();

            CreateMap<GetOrderListRequest, GetOrderListForBossClientRequest>()
                .ForMember(dest => dest.StartDateTime,
                opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.StartDateTime) ? new DateTime(1900, 1, 1) : DateTime.Parse(src.StartDateTime)))
                .ForMember(dest => dest.EndDateTime,
                opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.EndDateTime) ? new DateTime(1900, 1, 1) : DateTime.Parse(src.EndDateTime)));
            CreateMap<GetOrderListForBossClientResponse, GetOrderListResponse>();

            CreateMap<AppendSubmitOrderRequest, AppendSubmitOrderClientRequest>();
            CreateMap<ApiRequest<AppendSubmitOrderRequest>, ApiRequest<AppendSubmitOrderClientRequest>>();
            CreateMap<AppendSubmitOrderClientResponse, AppendSubmitOrderResponse>();

            CreateMap<OrderLogDTO, OrderLogVO>().ReverseMap();
            CreateMap<GetOrderLogListRequest, GetOrderLogListClientRequest>();
            CreateMap<GetOrderLogListRequest, GetOrderLogListClientRequest>();
        }
    }
}
