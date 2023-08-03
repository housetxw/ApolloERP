using AutoMapper;
using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Model;
using Ae.ConsumerOrder.Service.Client.Model.Coupon;
using Ae.ConsumerOrder.Service.Client.Model.Order;
using Ae.ConsumerOrder.Service.Client.Model.User;
using Ae.ConsumerOrder.Service.Client.Model.Vehicle;
using Ae.ConsumerOrder.Service.Client.Request;
using Ae.ConsumerOrder.Service.Client.Response;
using Ae.ConsumerOrder.Service.Core.Model;
using Ae.ConsumerOrder.Service.Core.Request;
using Ae.ConsumerOrder.Service.Core.Request.OrderCommand;
using Ae.ConsumerOrder.Service.Core.Response;
using Ae.ConsumerOrder.Service.Core.Response.SharingPromotion;
using Ae.ConsumerOrder.Service.Dal.Model;

namespace Ae.ConsumerOrder.Service.Imp.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderCarDO, OrderCarDTO>().ReverseMap();
            CreateMap<GetOrderConfirmRequest, GetOrderConfirmPackageProductServicesRequest>();
            CreateMap<TrialCalcOrderAmountRequest, GetOrderConfirmPackageProductServicesRequest>();
            CreateMap<OrderProductDO, OrderDetailProductDTO>();
            CreateMap<OrderConfirmProductDTO, OrderProductDO>();
            CreateMap<UserInfoResponse, OrderUserDO>();
            CreateMap<UserVehicleDTO, OrderCarDO>();
            CreateMap<ShopDTO, OrderAddressDO>();
            CreateMap<OrderDO, MainOrderDTO>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<OrderProductDO, MainOrderProductDTO>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.OrderProductId, opt => opt.MapFrom(src => src.Id));
            CreateMap<SubmitOrderRequest, GetOrderConfirmPackageProductServicesRequest>();
            CreateMap<SubmitUseVerificationCodeOrderRequest, SubmitOrderRequest>();
            CreateMap<ApiRequest<SubmitUseVerificationCodeOrderRequest>, ApiRequest<SubmitOrderRequest>>();
            CreateMap<SendOrderUseStockRequest, SendOrderUseStockClientRequest>();
            CreateMap<SendOrderReleaseStockRequest, SendOrderReleaseStockClientRequest>();
            CreateMap<OrderDetailProductDTO, UseStockOrderDetailProductDTO>();
            CreateMap<OrderDetailPackageProductDTO, UseStockOrderDetailPackageProductDTO>();
            CreateMap<OrderAddressDO, OrderAddressDTO>().ReverseMap();
            CreateMap<OrderUserDO, OrderUserDTO>().ReverseMap();

            CreateMap<OrderVerificationCodeDO, GetVerificationCodeOrderListResponse>();
            CreateMap<OrderVerificationCodeDO, GetVerificationCodeOrderDetailResponse>();

            CreateMap<OrderDO, SubmitOrderRequest>();

            CreateMap<OrderDO, OrderDetailForBossOrderInfoDTO>();
            CreateMap<OrderDO, OrderDetailForBossFinanceInfoDTO>();
            CreateMap<OrderDO, OrderDetailForBossAmountInfoDTO>();
            CreateMap<OrderUserDO, OrderDetailForBossUserInfoDTO>();
            CreateMap<OrderProductDO, OrderDetailForBossProductDTO>();
            CreateMap<OrderProductDO, OrderDetailForBossPackageProductDTO>();

            CreateMap<OrderLogDO, OrderLogDTO>().ReverseMap();

            CreateMap<OrderPaySuccessNotifyRequest, UpdatePayStatusRequest>();

            CreateMap<UserAddressDTO, OrderAddressDO>();

            CreateMap<UserCouponPageResByUserIdDTO, GetSharingCouponResponse>();

            CreateMap<UserVehicleDTO, Client.Request.BaoYang.VehicleRequest>();
        }
    }
}
