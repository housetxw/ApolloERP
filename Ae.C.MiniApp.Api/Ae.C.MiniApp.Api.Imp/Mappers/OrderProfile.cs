using AutoMapper;
using ApolloErp.Common.QrCode;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Model.Order;
using Ae.C.MiniApp.Api.Client.Request.Order;
using Ae.C.MiniApp.Api.Client.Response.Order;
using Ae.C.MiniApp.Api.Common.Extension;
using Ae.C.MiniApp.Api.Common.Util;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Response;

namespace Ae.C.MiniApp.Api.Imp.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderUserOperationDTO, OrderUserOperationVO>()
                .ForMember(dest => dest.ShowFunctionName, opt => opt.MapFrom(src => src.Function.GetDescription()));

            CreateMap<OrderDetailProductDTO, OrderDetailProductVO>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl.AddImageDomain()));//图片绝对路径
            CreateMap<OrderDetailPackageProductDTO, OrderDetailPackageProductVO>();

            CreateMap<OrderInstallCodeInfoDTO, OrderInstallCodeInfoVO>()
                .ForMember(dest => dest.QRCodeBase64String, opt => opt.MapFrom(src => $"data:image/png;base64,{QrCodeCreateHelper.GetQRCode(src.Code, 5)}"));//二维码
            CreateMap<VerificationRuleInfoDTO, VerificationRuleInfoVO>();
            CreateMap<OrderVerificationCodeBaseInfoDTO, OrderVerificationCodeBaseInfoVO>()
                .ForMember(dest => dest.QRCodeBase64String, opt => opt.MapFrom(src => $"data:image/png;base64,{QrCodeCreateHelper.GetQRCode(src.Code, 5)}"));//二维码
            CreateMap<OrderVerificationCodeInfoDTO, OrderVerificationCodeInfoVO>()
                .ForMember(dest => dest.QRCodeBase64String, opt => opt.MapFrom(src => $"data:image/png;base64,{QrCodeCreateHelper.GetQRCode(src.Code, 5)}"));//二维码
            CreateMap<GetOrderVerificationCodeInfosRequest, GetOrderVerificationCodeInfosClientRequest>();
            CreateMap<GetOrderVerificationCodeInfosClientResponse, GetOrderVerificationCodeInfosResponse>();

            CreateMap<GetOrderDetailRequest, GetOrderDetailClientRequest>();
            CreateMap<GetOrderDetailClientResponse, GetOrderDetailResponse>();

            CreateMap<GetOrderListRequest, GetOrderListForMiniAppClientRequest>();
            CreateMap<GetOrderListForMiniAppClientResponse, GetOrderListResponse>();

            CreateMap<SelectedProductInfoVO, SelectedProductInfoDTO>();
            CreateMap<BaseSubmitOrderInfoVO, BaseSubmitOrderInfoDTO>();
            CreateMap<SubmitOrderRequest, SubmitOrderClientRequest>();
            CreateMap<ApiRequest<SubmitOrderRequest>, ApiRequest<SubmitOrderClientRequest>>();
            CreateMap<SubmitOrderClientResponse, SubmitOrderResponse>();

            CreateMap<BaseSubmitOrderInfoVO, BaseSubmitOrderInfoDTO>();
            CreateMap<OrderCalcPriceInfoDTO, OrderCalcPriceInfoVO>();

            CreateMap<OrderConfirmProductDTO, OrderConfirmProductVO>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl.AddImageDomain()));//图片绝对路径
            CreateMap<OrderConfirmPackageProductDTO, OrderConfirmPackageProductVO>();

            CreateMap<GetOrderConfirmRequest, GetOrderConfirmClientRequest>();
            CreateMap<GetOrderConfirmClientResponse, GetOrderConfirmResponse>();

            CreateMap<CalcOrderAmountRequest, TrialCalcOrderAmountClientRequest>();
            CreateMap<TrialCalcOrderAmountClientResponse, CalcOrderAmountResponse>();

            CreateMap<BuyAgainRequest, BuyAgainClientRequest>();
            CreateMap<ApiRequest<BuyAgainRequest>, ApiRequest<BuyAgainClientRequest>>();
            CreateMap<BuyAgainClientResponse, BuyAgainResponse>();

            CreateMap<GetEachStatusOrderCountRequest, GetEachStatusOrderCountForMiniAppRequest>();
            CreateMap<GetEachStatusOrderCountForMiniAppResponse, GetEachStatusOrderCountResponse>();
        }
    }
}
