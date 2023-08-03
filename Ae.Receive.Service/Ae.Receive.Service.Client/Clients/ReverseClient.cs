using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Log;
using Ae.Receive.Service.Client.Request.Order;
using ApolloErp.Web.WebApi;
using Ae.Receive.Service.Client.Inteface;
using Ae.Receive.Service.Common.Exceptions;
using Newtonsoft.Json;

namespace Ae.Receive.Service.Client.Clients
{
    public class ReverseClient: IReverseClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly ApolloErpLogger<ReverseClient> _logger;

        public ReverseClient(IHttpClientFactory clientFactory, IConfiguration configuration,
            ApolloErpLogger<ReverseClient> logger)
        {
            this._clientFactory = clientFactory;
            this._configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// 创建取消类型的逆向申请单For到店与预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> CreateReverseOrderCancelForArrivalOrReserve(CancelOrderRequest request)
        {
            var client = _clientFactory.CreateClient("ReverseServer");
            ApiResult result = await client.PostAsJsonAsync<CancelOrderRequest, ApiResult>(
                _configuration["ReverseServer:CreateReverseOrderCancelForArrivalOrReserve"], request);
            //_logger.Info($"CreateReverseOrderCancelForArrivalOrReserve_Result Para={JsonConvert.SerializeObject(request)} Result={JsonConvert.SerializeObject(result)}");
            if (result.Code == ResultCode.Success)
            {
                return true;
            }
            else
            {
                _logger.Error($"CreateReverseOrderCancelForArrivalOrReserve_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }
    }
}
