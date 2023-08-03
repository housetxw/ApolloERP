using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.OrderComment.Service.Client.Interface;
using Ae.OrderComment.Service.Client.Model;
using Ae.OrderComment.Service.Client.Request;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.OrderComment.Service.Client.Clients
{
    public class VehicleClient: IVehicleClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public VehicleClient(IHttpClientFactory httpClientFactory, 
            IConfiguration configuration
            )
        {
            this._httpClientFactory = httpClientFactory;
            this._configuration = configuration;
        }

        public async Task<ApiResult<UserVehicleDTO>> GetUserVehicleByCarIdAsync(UserVehicleByCarIdClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<UserVehicleDTO> result =
                await client.GetAsJsonAsync<UserVehicleByCarIdClientRequest, ApiResult<UserVehicleDTO>>(
                    _configuration["VehicleServer:GetUserVehicleByCarIdAsync"], request);
            return result;
        }

    }
}
