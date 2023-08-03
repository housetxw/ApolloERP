using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Client.Request.Pay;
using Ae.C.MiniApp.Api.Client.Response.Pay;
using Ae.C.MiniApp.Api.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Client.Clients.Pay
{
    public class PayClient : IPayClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }

        private readonly ApolloErpLogger<PayClient> _logger;

        public PayClient(IHttpClientFactory clientFactory, IConfiguration configuration,
            ApolloErpLogger<PayClient> logger)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
            _logger = logger;
        }

        public async Task<ApiResult<ConfirmPayClientResponse>> ConfirmPay(ConfirmPayClientRequest request)
        {
            var client = clientFactory.CreateClient("PayServer");
            var response = await client.PostAsJsonAsync<ConfirmPayClientRequest, ApiResult<ConfirmPayClientResponse>>(configuration["PayServer:ConfirmPay"], request);
            return response;
        }

        public async Task<ApiResult> ClosePay(ClosePayClientRequest request)
        {
            var client = clientFactory.CreateClient("PayServer");
            var response = await client.PostAsJsonAsync<ClosePayClientRequest, ApiResult>(configuration["PayServer:ClosePay"], request);
            return response;
        }

        /// <summary>
        /// 企业付款
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<EnterprisePayClientResponse> EnterprisePay(EnterprisePayClientRequest request)
        {
            var client = clientFactory.CreateClient("PayServer");

            ApiResult<EnterprisePayClientResponse> result =
                await client.PostAsJsonAsync<EnterprisePayClientRequest, ApiResult<EnterprisePayClientResponse>>(
                    configuration["PayServer:EnterprisePay"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"EnterprisePay_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 微信转账到支付宝账户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<WeChatTransferAliPayClientResponse> WeChatTransferAliPay(
            WeChatTransferAliPayClientRequest request)
        {
            var client = clientFactory.CreateClient("PayServer");
            var response =
                await client
                    .PostAsJsonAsync<WeChatTransferAliPayClientRequest, ApiResult<WeChatTransferAliPayClientResponse>>(
                        configuration["PayServer:WeChatTransferAliPay"], request);
            if (response.IsNotNullSuccess())
            {
                return response.Data;
            }
            else
            {
                var msg = response?.Message ?? "系统异常";
                _logger.Info($"WeChatTransferAliPay_Exception {msg}");
                throw new CustomException(msg);
            }
        }
    }
}
