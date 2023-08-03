using Ae.OrderComment.Service.Client.Request;
using Ae.OrderComment.Service.Client.Response;
using ApolloErp.Web.WebApi;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Ae.OrderComment.Service.Client.Clients.ShopServer
{
    public class TestClient: ITestClient
    {
        private readonly IHttpClientFactory clientFactory;
        private  IConfiguration configuration { get; }
        public TestClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }

       
    }
}
