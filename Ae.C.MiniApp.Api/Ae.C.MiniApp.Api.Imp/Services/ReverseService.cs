using AutoMapper;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Common.Constant;
using Ae.C.MiniApp.Api.Core.Enums;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Request.Reverse;
using Ae.C.MiniApp.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Imp.Services
{
    public class ReverseService : IReverseService
    {
        private readonly IMapper mapper;
        private readonly ApolloErpLogger<ReverseService> logger;
        private readonly IIdentityService identityService;
        private readonly IReverseClient reverseClient;
        private readonly IShopOrderQueryClient _shopOrderQueryClient;

        public ReverseService(IMapper mapper, ApolloErpLogger<ReverseService> logger, IIdentityService identityService, IReverseClient reverseClient, IShopOrderQueryClient shopOrderQueryClient)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.identityService = identityService;
            this.reverseClient = reverseClient;
            _shopOrderQueryClient = shopOrderQueryClient;
        }

        public async Task<ApiResult> CancelNewOrder(ApiRequest<CancelNewOrderRequest> request)
        {
            request.Data.CreateBy = identityService.GetUserName()??CommonConstant.User;

            return await _shopOrderQueryClient.CancelOrder(request);
        }

        public async Task<ApiResult<CreateReverseOrderBaseResponse>> CancelOrder(ApiRequest<CancelOrderRequest> request)
        {
            var result = Result.Failed<CreateReverseOrderBaseResponse>("申请失败");
            try
            {
                var clientRequest = mapper.Map<CreateReverseOrderForCancelClientRequest>(request.Data);
                clientRequest.UserId = identityService.GetUserId();
                clientRequest.OperatorType = ReverseOperatorTypeEnum.Customer;
                var clientResult = await reverseClient.CreateReverseOrderForCancel(clientRequest);
                if (clientResult.IsNotNullSuccess())
                {
                    var response = mapper.Map<CreateReverseOrderBaseResponse>(clientResult.Data);
                    result = Result.Success(response, clientResult.Message);
                }
                else if (clientResult != null)
                {
                    result = Result.Failed<CreateReverseOrderBaseResponse>(clientResult.Code, clientResult.Message);
                }
            }
            catch (Exception ex)
            {
                logger.Error("CancelOrderEx", ex);
            }
            return result;
        }

        public async Task<ApiResult<List<ReverseReasonVO>>> GetReverseReasons(GetReverseReasonsRequest request)
        {
            var result = Result.Failed<List<ReverseReasonVO>>("加载异常，请重试");
            try
            {
                var clientRequest = mapper.Map<GetReverseReasonConfigsClientRequest>(request);
                var clientResult = await reverseClient.GetReverseReasonConfigs(clientRequest);
                if (clientResult.IsNotNullSuccess())
                {
                    var response = mapper.Map<List<ReverseReasonVO>>(clientResult.Data);
                    result = Result.Success(response, clientResult.Message);
                }
                else if (clientResult != null)
                {
                    result = Result.Failed<List<ReverseReasonVO>>(clientResult.Code, clientResult.Message);
                }
            }
            catch (Exception ex)
            {
                logger.Error("GetReverseReasonsEx", ex);
            }
            return result;
        }
    }
}
