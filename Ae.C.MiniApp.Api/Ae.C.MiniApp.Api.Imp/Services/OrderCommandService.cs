using AutoMapper;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Client.Request.Order;
using Ae.C.MiniApp.Api.Common.Exceptions;
using Ae.C.MiniApp.Api.Common.Extension;
using Ae.C.MiniApp.Api.Core.Enums;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Response;
using System;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Imp.Services
{
    public class OrderCommandService : IOrderCommandService
    {
        private readonly IMapper mapper;
        private readonly ApolloErpLogger<OrderCommandService> logger;
        private readonly IIdentityService identityService;
        private readonly IConsumerOrderCommandClient consumerOrderCommandClient;

        public OrderCommandService(IMapper mapper, ApolloErpLogger<OrderCommandService> logger, IIdentityService identityService, IConsumerOrderCommandClient consumerOrderCommandClient)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.identityService = identityService;
            this.consumerOrderCommandClient = consumerOrderCommandClient;
        }

        public async Task<ApiResult<BuyAgainResponse>> BuyAgain(ApiRequest<BuyAgainRequest> request)
        {
            var result = Result.Failed<BuyAgainResponse>("下单失败");
            try
            {
                var clientRequest = mapper.Map<ApiRequest<BuyAgainClientRequest>>(request);
                clientRequest.Data.UserId = identityService.GetUserId();
                var clientResult = await consumerOrderCommandClient.BuyAgain(clientRequest);
                if (clientResult.IsNotNullSuccess())
                {
                    var response = mapper.Map<BuyAgainResponse>(clientResult.Data);
                    result = Result.Success(response, clientResult.Message);
                }
                else if (clientResult != null)
                {
                    result = Result.Failed<BuyAgainResponse>(clientResult.Code, clientResult.Message);
                }
            }
            catch (Exception ex)
            {
                logger.Error("BuyAgainEx", ex);
            }
            return result;
        }

        public async Task<ApiResult<SubmitOrderResponse>> SubmitOrder(ApiRequest<SubmitOrderRequest> request)
        {
            var result = Result.Failed<SubmitOrderResponse>("下单失败");
            try
            {
                if (request.ShopId > 0 && request.Data.ShopId == 0)
                {
                    request.Data.ShopId = request.ShopId;
                }
                var clientRequest = mapper.Map<ApiRequest<SubmitOrderClientRequest>>(request);
                clientRequest.Data.UserId = identityService.GetUserId();
                if (request.Data.ProduceType == ProductTypeEnum.VipCard.ToInt())//会员卡特殊的逻辑，为了兼容下单创建的特殊门店，优化需要去掉
                {
                    clientRequest.ShopId = 4288;
                    clientRequest.Data.ShopId = 4288;
                    clientRequest.Data.DeliveryType = 1;
                }
                //if (clientRequest.Data.DeliveryType == 1 && clientRequest.Data.ShopId <= 0)
                //{
                //    throw new CustomException("请选择服务门店");
                //}
                if (clientRequest.Data.DeliveryType == 2 && clientRequest.Data.ReceiverAddressId <= 0)
                {
                    throw new CustomException("请选择收货地址");
                }
                
                var clientResult = await consumerOrderCommandClient.SubmitOrder(clientRequest);
                if (clientResult.IsNotNullSuccess())
                {
                    var response = mapper.Map<SubmitOrderResponse>(clientResult.Data);
                    result = Result.Success(response, clientResult.Message);
                }
                else if (clientResult != null)
                {
                    result = Result.Failed<SubmitOrderResponse>(clientResult.Code, clientResult.Message);
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error("SubmitOrderEx", ex);
            }
            return result;
        }
    }
}
