using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Client.Model.Order;
using Ae.ShopOrder.Service.Client.Model.OrderForC;
using Ae.ShopOrder.Service.Client.Model.Shop;
using Ae.ShopOrder.Service.Client.Model.User;
using Ae.ShopOrder.Service.Client.Model.Vehicle;
using Ae.ShopOrder.Service.Client.Model.WMS;
using Ae.ShopOrder.Service.Client.Request.OrderForC;
using Ae.ShopOrder.Service.Client.Request.WMS;
using Ae.ShopOrder.Service.Client.Response.Coupon;
using Ae.ShopOrder.Service.Client.Response.User;
using Ae.ShopOrder.Service.Core.Model;
using Ae.ShopOrder.Service.Core.Model.Order;
using Ae.ShopOrder.Service.Core.Model.Product;
using Ae.ShopOrder.Service.Core.Model.Stock;
using Ae.ShopOrder.Service.Core.Request;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Core.Response.Order;
using Ae.ShopOrder.Service.Dal.Model;

namespace Ae.ShopOrder.Service.Imp.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {

            CreateMap<OrderProductDO, OrderDetailProductDTO>();


            #region 《Order Car》

            CreateMap<GetOrderCarRequest, GetOrderCarClientRequest>();

            CreateMap<OrderCarClientDTO, OrderCarDTO>();

            CreateMap<ApiResult<List<OrderCarClientDTO>>, ApiResult<List<OrderCarDTO>>>();

            CreateMap<OrderCarDO, OrderCarDTO>();

            #endregion

            #region OrderNotAdapter
            CreateMap<OrderNotAdapterRequest, OrderNotAdapterDO>();
            #endregion

            #region WMS

            CreateMap<GetWareHouseTransferRequest, GetWareHouseTransferClientRequest>();

            CreateMap<GetWareHouseTransferProductClientDTO, GetWareHouseTransferProductVO>();
            CreateMap<GetWareHouseTransferPackageClientDTO, GetWareHouseTransferPackageVO>();

            CreateMap<GetWareHouseTransferClientDTO, GetWareHouseTransferResponse>();

            CreateMap<ApiResult<GetWareHouseTransferClientDTO>, ApiResult<GetWareHouseTransferResponse>>();
            #endregion


            #region 《 Reserver 与 Arrival》
            CreateMap<ShopDTO, OrderAddressDO>();

            CreateMap<UserInfoResponse, OrderUserDO>();

            CreateMap<UserVehicleDTO, OrderCarDO>();

            CreateMap<OrderDO, MainOrderDTO>();

            CreateMap<OrderDO, MainOrderDTO>();

            //CreateMap<List<OrderProductDO>, List<MainOrderProductDTO>>();

            //CreateMap<OrderProductDO, MainOrderProductDTO>();

            #endregion


            CreateMap<OrderDO, OrderDetailForBossOrderInfoDto>();
            CreateMap<OrderDO, OrderDetailForBossFinanceInfoDto>();
            CreateMap<OrderDO, OrderDetailForBossAmountInfoDto>();
            CreateMap<OrderUserDO, OrderDetailForBossUserInfoDto>();
            CreateMap<OrderProductDO, OrderDetailForBossPackageProductDto>();
            CreateMap<OrderProductDO, OrderDetailForBossProductDto>();
            CreateMap<OrderLogDO, OrderLogDTO>();


            #region Dispatch
            CreateMap<OrderDispatchDO, OrderDispatchDTO>();

            CreateMap<UpdateOrderDispatchRequest, OrderDispatchDO>();

            CreateMap<UpdateOrderDispatchRequest, UpdateOrderDispatchRequest>();


            #endregion

            #region Stock
            CreateMap<OrderDetailProductDTO, UseStockOrderDetailProductDTO>();
            CreateMap<OrderDetailPackageProductDTO, UseStockOrderDetailPackageProductDTO>();
            CreateMap<OrderProductDO, UseStockOrderDetailProductDTO>();
            CreateMap<OrderAddressDO, OrderAddressDTO>().ReverseMap();
            #endregion

            CreateMap<OrderConfirmProductDTO, OrderProductDO>();

            CreateMap<OrderDO, MainOrderDTO>()
           .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<OrderProductDO, MainOrderProductDTO>()
              .ForMember(dest => dest.Id, opt => opt.Ignore())
              .ForMember(dest => dest.OrderProductId, opt => opt.MapFrom(src => src.Id));

            CreateMap<SubmitOrderRequest, GetOrderConfirmPackageProductServicesRequest>();

            CreateMap<OrderPaySuccessNotifyRequest, UpdatePayStatusRequest>();

            CreateMap<OrderPayInstallRequest, OrderPaySuccessNotifyRequest>();


            CreateMap<UserAddressDTO, OrderAddressDO>();


            CreateMap<OrderProductDTO, OrderProductDO>();

            CreateMap<FranchisesConfigDO, FranchisesConfigDTO>();


            CreateMap<PagedEntity<OrderDO>, ApiPagedResultData<GetOrdersForOfficeResponse>>();

            CreateMap<OrderDO, GetOrdersForOfficeResponse>();

            CreateMap<OrderProductDTO, GetOrdersForOfficeProductDTO>();

            CreateMap<UserCouponEntityCustomResponse, OrderCouponInformationDto>();


            CreateMap<OrderInsuranceCompanyDO, OrderInsuranceCompanyDTO>();

            CreateMap<InsuranceCompanyDO, InsuranceCompanyDTO>();

            CreateMap<PagedEntity<VerificationRuleDO>, ApiPagedResultData<VerificationRuleDTO>>();

            CreateMap<VerificationRuleDO, VerificationRuleDTO>();

            CreateMap<SaveVerificationRuleRequest, VerificationRuleDO>()
                .ForMember(dest => dest.EarliestUseDate,
                opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.EarliestUseDate) ? new DateTime(1900, 1, 1) : DateTime.Parse(src.EarliestUseDate)))
                .ForMember(dest => dest.LatestUseDate,
                opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.LatestUseDate) ? new DateTime(1900, 1, 1) : DateTime.Parse(src.LatestUseDate)));


            CreateMap<SaveBeautiyOrPackageCardVerificationProductRequest, VerificationRulePidDO>();

            CreateMap<UserVehicleDTO, Core.Request.BaoYang.VehicleRequest>();


            CreateMap<OrderProductDO, OrderProductNewDTO>();


            CreateMap<PagedEntity<OrderProductDO>, ApiPagedResultData<OrderProductNewDTO>>();

            CreateMap<PagedEntity<OrderProductNewDTO>, ApiPagedResultData<OrderProductNewDTO>>();

            CreateMap<PagedEntity<GetOrderOutProductResponse>, ApiPagedResultData<GetOrderOutProductResponse>>();


            CreateMap<OrderPackageCardDO, OrderPackageCardDTO>();

            CreateMap<SubmitOrderResponse, SubmitUseVerificationCodeOrderResponse>().ReverseMap();
            CreateMap< ApiResult<SubmitOrderResponse>, ApiResult<SubmitUseVerificationCodeOrderResponse>>().ReverseMap();
        }


    }
}
