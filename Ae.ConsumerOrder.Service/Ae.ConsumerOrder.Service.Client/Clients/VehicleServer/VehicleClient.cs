using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Model.User;
using Ae.ConsumerOrder.Service.Client.Model.Vehicle;
using Ae.ConsumerOrder.Service.Client.Request;
using Ae.ConsumerOrder.Service.Client.Response;

namespace Ae.ConsumerOrder.Service.Client.Clients
{
    public class VehicleClient : IVehicleClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }

        public VehicleClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }

        public async Task<ApiResult<UserVehicleDTO>> GetUserVehicleByCarIdAsync(UserVehicleByCarIdRequest request)
        {
            var client = clientFactory.CreateClient("VehicleServer");
            var response = await client.GetAsJsonAsync<UserVehicleByCarIdRequest, ApiResult<UserVehicleDTO>>(configuration["VehicleServer:GetUserVehicleByCarIdAsync"], request);
            return response;
        }
    }
}
