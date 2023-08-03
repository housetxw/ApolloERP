using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Core.Request.Fms;

namespace Ae.ShopOrder.Service.Client.Clients.FMS
{
    public  class FmsClient:IFmsClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private object configuration;

        private IConfiguration _configuration { get; }

        public FmsClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        /// <summary>
        /// 计算门店对账公司对账金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> CalculationReconciliationFee(CalculationReconciliationFeeRequest request)
        {

            var client = _clientFactory.CreateClient("FmsServer");

            var response =
                await client
                    .GetAsJsonAsync<CalculationReconciliationFeeRequest, ApiResult>(
                        _configuration["FmsServer:CalculationReconciliationFee"], request);

            return response;
        }
        /// <summary>
        /// 删除订单对账
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> DeleteAccountCheck(CalculationReconciliationFeeRequest request)
        {
            var client = _clientFactory.CreateClient("FmsServer");

            var response =
                await client
                    .PostAsJsonAsync<CalculationReconciliationFeeRequest, ApiResult<string>>(
                        _configuration["FmsServer:DeleteAccountCheck"], request);

            return response;
        }
    }
}
