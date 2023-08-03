using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Request.Activity;
using Ae.ConsumerOrder.Service.Client.Response.Activity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ConsumerOrder.Service.Client.Clients.ActivityServer
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

        /// <summary>
        /// 查找用户分享人
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetUserRefersResponse>> GetUserRefers(GetUserRefersRequest request)
        {
            var client = clientFactory.CreateClient("ActivityServer");
            var response = await client.GetAsJsonAsync<GetUserRefersRequest, ApiResult<GetUserRefersResponse>>(configuration["ActivityServer:GetUserRefers"], request);
            return response;
        }

       
    }
}
