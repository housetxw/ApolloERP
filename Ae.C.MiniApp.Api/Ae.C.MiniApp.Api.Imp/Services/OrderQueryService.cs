using AutoMapper;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Clients.Interface;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Client.Request.Order;
using Ae.C.MiniApp.Api.Client.Response.Order;
using Ae.C.MiniApp.Api.Common.Extension;
using Ae.C.MiniApp.Api.Core.Enums;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Request.OrderQuery;
using Ae.C.MiniApp.Api.Core.Response;
using Ae.C.MiniApp.Api.Core.Response.OrderQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Imp.Services
{
    public class OrderQueryService : IOrderQueryService
    {
        private readonly IMapper mapper;
        private readonly ApolloErpLogger<OrderQueryService> logger;
        private readonly IIdentityService identityService;
        private readonly IOrderQueryClient orderQueryClient;
        private readonly IConsumerOrderQueryClient consumerOrderQueryClient;
        private readonly IShopOrderQueryClient shopOrderQueryClient;
        private readonly IProductClient _productClient;
        private readonly ICouponClient _couponClient;

        public OrderQueryService(IMapper mapper, ApolloErpLogger<OrderQueryService> logger,
            IIdentityService identityService, IOrderQueryClient orderQueryClient,
            IConsumerOrderQueryClient consumerOrderQueryClient,
            IShopOrderQueryClient shopOrderQueryClient, IProductClient productClient, ICouponClient couponClient)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.identityService = identityService;
            this.orderQueryClient = orderQueryClient;
            this.consumerOrderQueryClient = consumerOrderQueryClient;
            this.shopOrderQueryClient = shopOrderQueryClient;
            _productClient = productClient;
            _couponClient = couponClient;
        }

        public async Task<ApiResult<List<GetEachStatusOrderCountResponse>>> GetEachStatusOrderCount(GetEachStatusOrderCountRequest request)
        {
            var result = Result.Failed<List<GetEachStatusOrderCountResponse>>("加载异常，请重试");
            try
            {
                var clientRequest = mapper.Map<GetEachStatusOrderCountForMiniAppRequest>(request);
                clientRequest.UserId = identityService.GetUserId();
                var clientResult = await orderQueryClient.GetEachStatusOrderCountForMiniApp(clientRequest);
                if (clientResult.IsNotNullSuccess())
                {
                    var response = mapper.Map<List<GetEachStatusOrderCountResponse>>(clientResult.Data);
                    result = Result.Success(response, clientResult.Message);
                }
                else if (clientResult != null)
                {
                    result = Result.Failed<List<GetEachStatusOrderCountResponse>>(clientResult.Code, clientResult.Message);
                }
            }
            catch (Exception ex)
            {
                logger.Error("GetEachStatusOrderCountEx", ex);
            }
            return result;
        }

        public async Task<ApiResult<GetOrderConfirmResponse>> GetOrderConfirm(ApiRequest<GetOrderConfirmRequest> request)
        {
            var result = Result.Failed<GetOrderConfirmResponse>("加载异常，请重试");
            try
            {
                var clientRequest = mapper.Map<GetOrderConfirmClientRequest>(request.Data);
                clientRequest.UserId = identityService.GetUserId();
                //clientRequest.UserId = "38c57e3c-cf4b-4c14-a5b5-6b4d6baad2fc";


                if (request.Data.ProduceType == ProductTypeEnum.DoorToDoor.ToInt())
                {
                    clientRequest.ProductInfos.Add(new Client.Model.Order.SelectedProductInfoDTO()
                    {
                        Pid = "BYFW-SMFW-SSMFW-2-FU",
                        Number = 1
                    });

                }

                if (request.Data.ProduceType == ProductTypeEnum.VipCard.ToInt())//会员卡特殊的逻辑，为了兼容下单创建的特殊门店，优化需要去掉
                {
                    clientRequest.ShopId = 4288;
                }

                var clientResult = await consumerOrderQueryClient.GetOrderConfirm(clientRequest);
                if (clientResult.IsNotNullSuccess())
                {
                    var response = mapper.Map<GetOrderConfirmResponse>(clientResult.Data);

                    var installService = response?.Products?.ToList()?.Where(_ => _.PackageOrProduct.ProductId == "BYFW-SMFW-SSMFW-2-FU")?.FirstOrDefault();

                    if (installService != null)
                    {
                        response.DoorToDoorService = installService?.PackageOrProduct;
                        response.TotalDoorToDoorFee = installService?.PackageOrProduct?.Price ?? 0;
                        response?.Products?.Remove(response?.Products?.ToList()?.Where(_ => _.PackageOrProduct.ProductId == "BYFW-SMFW-SSMFW-2-FU")?.FirstOrDefault());
                    }
                    else
                        response.DoorToDoorService = null;

                    result = Result.Success(response, clientResult.Message);
                }
                else if (clientResult != null)
                {
                    result = Result.Failed<GetOrderConfirmResponse>(clientResult.Code, clientResult.Message);
                }
            }
            catch (Exception ex)
            {
                logger.Error("GetOrderConfirmEx", ex);
            }
            return result;
        }

        public async Task<ApiResult<GetOrderDetailResponse>> GetOrderDetailForMiniApp(GetOrderDetailRequest request)
        {
            var result = Result.Failed<GetOrderDetailResponse>("加载异常，请重试");
            try
            {
                var clientRequest = mapper.Map<GetOrderDetailClientRequest>(request);
                clientRequest.UserId = identityService.GetUserId();
                //clientRequest.UserId = "6d6a2ed7-f1b0-4f1f-9ca9-a0853e7e6dd1";
                ApiResult<GetOrderDetailClientResponse> clientResult = null;
                //if (request.OrderNo.ToUpper().StartsWith("RGC"))
                //{
                //    clientResult = await consumerOrderQueryClient.GetOrderDetail(clientRequest);
                //}
                //else if (request.OrderNo.ToUpper().StartsWith("RGB"))
                //{
                clientResult = await shopOrderQueryClient.GetOrderDetailForMiniApp(clientRequest);
                //}
                if (clientResult.IsNotNullSuccess())
                {
                    var response = mapper.Map<GetOrderDetailResponse>(clientResult.Data);
                    if (response.ProductType == ProductTypeEnum.VipCard.ToInt())
                    {
                        var recommendProducts = await _couponClient.GetRecommendProductsForDiamondMembership(new Core.Request.Coupon.RecommendProductsForDiamondMembershipRequest());
                        response.RecommendProductInfos = recommendProducts?.Data;
                    }
                    result = Result.Success(response, clientResult.Message);
                }
                else if (clientResult != null)
                {
                    result = Result.Failed<GetOrderDetailResponse>(clientResult.Code, clientResult.Message);
                }
            }
            catch (Exception ex)
            {
                logger.Error("GetOrderDetailForMiniAppEx", ex);
            }
            return result;
        }

        public async Task<ApiPagedResult<GetOrderListResponse>> GetOrderListForMiniApp(GetOrderListRequest request)
        {
            var result = new ApiPagedResult<GetOrderListResponse>()
            {
                Code = ResultCode.Failed,
                Message = "加载异常，请重试"
            };
            try
            {
                var clientRequest = mapper.Map<GetOrderListForMiniAppClientRequest>(request);
                clientRequest.UserId = identityService.GetUserId();
                //clientRequest.UserId = "38c57e3c-cf4b-4c14-a5b5-6b4d6baad2fc";
                clientRequest.MiniAppType = 1;
                var clientResult = await orderQueryClient.GetOrderListForMiniApp(clientRequest);
                if (clientResult.IsNotNullSuccess())
                {
                    result = new ApiPagedResult<GetOrderListResponse>()
                    {
                        Code = ResultCode.Success,
                        Message = clientResult.Message,
                        Data = new ApiPagedResultData<GetOrderListResponse>()
                        {
                            TotalItems = clientResult.Data.TotalItems,
                            Items = mapper.Map<List<GetOrderListResponse>>(clientResult.Data.Items)
                        }
                    };
                }
                else if (clientResult != null)
                {
                    result = new ApiPagedResult<GetOrderListResponse>()
                    {
                        Code = clientResult.Code,
                        Message = clientResult.Message
                    };
                }
            }
            catch (Exception ex)
            {
                logger.Error("GetOrderListForMiniAppEx", ex);
            }
            return result;
        }

        public async Task<ApiResult<List<GetOrderPackageCardsResponse>>> GetOrderPackageCards(GetOrderPackageCardsRequest request)
        {
            var clientResponse = await orderQueryClient.GetOrderPackageCards(request);

            return Result.Success(clientResponse?.Data?.Items?.ToList());
        }

        public async Task<ApiResult<GetOrderServiceTypeResponse>> GetOrderServiceType(GetOrderServiceTypeRequest request)
        {
            return await consumerOrderQueryClient.GetOrderServiceType(request);
        }

        public async Task<ApiResult<GetOrderServiceTypeResponse>> GetOrderServiceTypeV2(GetOrderServiceTypeRequest request)
        {
            return await consumerOrderQueryClient.GetOrderServiceTypeV2(request);
        }

        public async Task<ApiResult<GetOrderVerificationCodeInfosResponse>> GetOrderVerificationCodeInfos(GetOrderVerificationCodeInfosRequest request)
        {
            var result = Result.Failed<GetOrderVerificationCodeInfosResponse>("加载异常，请重试");
            try
            {
                var clientRequest = mapper.Map<GetOrderVerificationCodeInfosClientRequest>(request);
                clientRequest.UserId = identityService.GetUserId();
                var clientResult = await consumerOrderQueryClient.GetOrderVerificationCodeInfos(clientRequest);
                if (clientResult.IsNotNullSuccess())
                {
                    var response = mapper.Map<GetOrderVerificationCodeInfosResponse>(clientResult.Data);
                    result = Result.Success(response, clientResult.Message);
                }
                else if (clientResult != null)
                {
                    result = Result.Failed<GetOrderVerificationCodeInfosResponse>(clientResult.Code, clientResult.Message);
                }
            }
            catch (Exception ex)
            {
                logger.Error("GetOrderVerificationCodeInfosEx", ex);
            }
            return result;
        }

        public async Task<ApiResult<List<GetPackageCardMainInfoResponse>>> GetPackageCardMainInfo(GetPackageCardMainInfoRequest request)
        {
            request.UserId = identityService.GetUserId();
            return await orderQueryClient.GetPackageCardMainInfo(request);
        }

        public async Task<ApiResult<GetPackageVerificationCodeDetailResponse>> GetPackageVerificationCodeDetail(GetPackageVerificationCodeDetailRequest request)
        {
            return await consumerOrderQueryClient.GetPackageVerificationCodeDetail(request);

        }

        public async Task<ApiResult<CalcOrderAmountResponse>> TrialCalcOrderAmount(ApiRequest<CalcOrderAmountRequest> request)
        {
            var result = Result.Failed<CalcOrderAmountResponse>("金额计算异常");
            try
            {
                var clientRequest = mapper.Map<TrialCalcOrderAmountClientRequest>(request.Data);
                clientRequest.UserId = identityService.GetUserId();
                //clientRequest.UserId = "38c57e3c-cf4b-4c14-a5b5-6b4d6baad2fc";
                var doorToDoorService = "BYFW-SMFW-SSMFW-2-FU";
                if (request.Data.ProduceType == ProductTypeEnum.DoorToDoor.ToInt())
                {
                    clientRequest.ProductInfos.Add(new Client.Model.Order.SelectedProductInfoDTO()
                    {
                        Pid = doorToDoorService,
                        Number = 1
                    });
                }
                var clientResult = await consumerOrderQueryClient.TrialCalcOrderAmount(clientRequest);
                if (clientResult.IsNotNullSuccess())
                {
                    var response = mapper.Map<CalcOrderAmountResponse>(clientResult.Data);

                    if (request.Data.ProduceType == ProductTypeEnum.DoorToDoor.ToInt())
                    {
                        var productDetail = await _productClient.ProductDetail(new Client.Request.Product.ProductDetailSearchClientRequest() { ProductCodes = new List<string>(1) { doorToDoorService } });
                        if (response != null)
                            response.TotalDoorToDoorFee = productDetail?.FirstOrDefault()?.Product?.SalesPrice ?? 0;
                    }
                    result = Result.Success(response, clientResult.Message);
                }
                else if (clientResult != null)
                {
                    result = Result.Failed<CalcOrderAmountResponse>(clientResult.Code, clientResult.Message);
                }
            }
            catch (Exception ex)
            {
                logger.Error("TrialCalcOrderAmountEx", ex);
            }
            return result;
        }
    }
}
