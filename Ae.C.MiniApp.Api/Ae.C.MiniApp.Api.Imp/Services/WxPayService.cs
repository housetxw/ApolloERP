using AutoMapper;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Clients.Interface;
using Ae.C.MiniApp.Api.Client.Request.Pay;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Imp.Services
{
    public class WxPayService : IWxPayService
    {
        private readonly IMapper mapper;
        private readonly ApolloErpLogger<OrderQueryService> logger;
        private readonly IWxPayClient wxPayClient;

        public WxPayService(IMapper mapper, ApolloErpLogger<OrderQueryService> logger, IWxPayClient wxPayClient)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.wxPayClient = wxPayClient;
        }

        public async Task<ApiResult<CreateWxPrePayOrderResponse>> CreateWxPrePayOrder(ApiRequest<CreateWxPrePayOrderRequest> request)
        {
            var result = Result.Failed<CreateWxPrePayOrderResponse>("请求支付失败");
            try
            {
                var clientRequest = mapper.Map<CreateWxPrePayOrderForMiniAppRequest>(request.Data);
                var clientResult = await wxPayClient.CreateWxPrePayOrderForMiniApp(clientRequest);
                if (clientResult.IsNotNullSuccess())
                {
                    var response = mapper.Map<CreateWxPrePayOrderResponse>(clientResult.Data);
                    result = Result.Success(response, clientResult.Message);
                }
                else
                {
                    result = Result.Failed<CreateWxPrePayOrderResponse>(clientResult.Code, clientResult.Message);
                }
            }
            catch (Exception ex)
            {
                logger.Error("CreateWxPrePayOrderEx", ex);
            }
            return result;
        }

        public async Task<string> WxPayResultNotify(string requestXml)
        {
            var responseXml = await wxPayClient.WxPayResultNotify(requestXml);
            return responseXml;
        }
    }
}
