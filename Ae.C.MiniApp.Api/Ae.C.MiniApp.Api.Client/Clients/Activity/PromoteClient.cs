using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Ae.C.MiniApp.Api.Client.Interface;

namespace Ae.C.MiniApp.Api.Client.Clients.Activity
{
    public class PromoteClient : IPromoteClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }
        public PromoteClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }
    }
}
