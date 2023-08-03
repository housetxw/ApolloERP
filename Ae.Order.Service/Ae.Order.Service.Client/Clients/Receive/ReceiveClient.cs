using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Order.Service.Client.Interface;
using Ae.Order.Service.Client.Request.Receive;
using Ae.Order.Service.Client.Response.Receive;
using Ae.Order.Service.Common.Exceptions;

namespace Ae.Order.Service.Client.Clients.Receive
{
    public class ReceiveClient : IReceiveClient
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly IHttpClientFactory clientFactory;
        private readonly ApolloErpLogger<ReceiveClient> logger;
        private readonly IConfiguration configuration;
        private readonly HttpClient client;

        public ReceiveClient(IHttpClientFactory clientFactory, ApolloErpLogger<ReceiveClient> logger, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            client = clientFactory.CreateClient("ReceiveServer");
            this.configuration = configuration;
            this.logger = logger;
        }

        // ---------------------------------- 接口实现 --------------------------------------

        public async Task<List<ShopReserveOrderDTO>> GetReserveInfoByReserveIdOrOrderNum(GetReserveInfoByReserveIdOrOrderNum req)
        {
            var res = await client.PostAsJsonAsync<GetReserveInfoByReserveIdOrOrderNum, ApiResult<List<ShopReserveOrderDTO>>>(configuration["ReceiveServer:GetReserveInfoByReserveIdOrOrderNum"], req);
            if (res.IsNotNullSuccess() && res.Code == ResultCode.Success)
            {
                return res.Data;
            }

            logger.Warn($"GetReserveInfoByReserveIdOrOrderNum_Error {res?.Message}");
            throw new CustomException(res?.Message);
        }


    }
}
