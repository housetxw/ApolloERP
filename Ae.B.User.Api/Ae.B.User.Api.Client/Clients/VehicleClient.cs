using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.B.User.Api.Client.Model.Vehicle;
using Ae.B.User.Api.Client.Request.Vehicle;
using Ae.B.User.Api.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.User.Api.Client.Clients
{
    public class VehicleClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ApolloErpLogger<VehicleClient> _logger;

        public VehicleClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ApolloErpLogger<VehicleClient> logger)
        {
            this._httpClientFactory = httpClientFactory;
            this._configuration = configuration;
            this._logger = logger;
        }

        public async Task<List<UserVehicleDto>> GetAllUserVehiclesAsync(AllUserVehiclesClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<List<UserVehicleDto>> result =
                await client.GetAsJsonAsync<AllUserVehiclesClientRequest, ApiResult<List<UserVehicleDto>>>(
                    _configuration["VehicleServer:GetAllUserVehiclesAsync"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Error($"GetAllUserVehiclesAsync_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }
    }
}
