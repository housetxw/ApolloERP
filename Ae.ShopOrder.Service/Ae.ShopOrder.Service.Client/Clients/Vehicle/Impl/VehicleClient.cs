using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Client.Model.Vehicle;
using Ae.ShopOrder.Service.Client.Request.Vehicle;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Client.Clients.Vehicle.Impl
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
