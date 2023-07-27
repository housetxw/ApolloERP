using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Client.Inteface;
using Ae.Shop.Service.Client.Request;
using Ae.Shop.Service.Client.Response;
using Ae.Shop.Service.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Client.Clients
{

    public class ActivityClient:IActivityClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }


        public ActivityClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }

        public async Task<ApiResult<GenMinAppCodeResponse>> GenMinAppCode(GenMinAppCodeClientRequest request)
        {
            var client = clientFactory.CreateClient("ActivityServer");

           var result =
                await client.PostAsJsonAsync<GenMinAppCodeClientRequest,
                ApiResult<GenMinAppCodeResponse>>
                (configuration["ActivityServer:GenMinAppCode"], request);
            if (result.Code != ResultCode.Success)
            {
               // _logger.Error($"GenMinAppCode_Error {result.Message}");
                throw new CustomException(result.Message);
            }
            return result;
        }
    }
}
