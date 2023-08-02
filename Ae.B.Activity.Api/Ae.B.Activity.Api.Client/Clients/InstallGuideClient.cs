using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.B.Activity.Api.Client.Model;
using Ae.B.Activity.Api.Client.Request;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Activity.Api.Client.Clients
{
    public class InstallGuideClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }
        private readonly ApolloErpLogger<InstallGuideClient> _logger;

        public InstallGuideClient(IHttpClientFactory clientFactory, IConfiguration configuration, ApolloErpLogger<InstallGuideClient> _logger)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
            this._logger = _logger;
        }

        public async Task<ApiResult<InstallGuideClientDTO>> GetInstallGuideDetail(GetInstallGuidePagesClientRequest request)
        {
            var client = clientFactory.CreateClient("ActivityServer");
            ApiResult<InstallGuideClientDTO> result = await
                client.GetAsJsonAsync<GetInstallGuidePagesClientRequest,
                ApiResult<InstallGuideClientDTO>>(configuration["ActivityServer:GetInstallGuideDetail"], request);
            return result;
        }

        public async Task<ApiPagedResult<InstallGuideClientDTO>> GetInstallGuidePages(GetInstallGuidePagesClientRequest request)
        {
            var client = clientFactory.CreateClient("ActivityServer");
            ApiPagedResult<InstallGuideClientDTO> result = await
                client.GetAsJsonAsync<GetInstallGuidePagesClientRequest,
                ApiPagedResult<InstallGuideClientDTO>>(configuration["ActivityServer:GetInstallGuidePages"], request);
            return result;
        }

        public async Task<ApiResult<string>> UpdateInstallGuideStatus(UpdateInstallGuideStatusClientRequest request)
        {
            var client = clientFactory.CreateClient("ActivityServer");
            ApiResult<string> result = await
                client.PostAsJsonAsync<UpdateInstallGuideStatusClientRequest,
                ApiResult<string>>(configuration["ActivityServer:UpdateInstallGuideStatus"], request);
            return result;
        }


        public async Task<ApiResult<string>> CreateInstallGuide(InstallGuideClientDTO request)
        {
            var client = clientFactory.CreateClient("ActivityServer");
            ApiResult<string> result = await
                client.PostAsJsonAsync<InstallGuideClientDTO,
                ApiResult<string>>(configuration["ActivityServer:CreateInstallGuide"], request);
            return result;
        }

        public async Task<ApiResult<string>> DeleteInstallGuideFile(InstallGuideFileClientDTO request)
        {
            var client = clientFactory.CreateClient("ActivityServer");
            ApiResult<string> result = await
                client.PostAsJsonAsync<InstallGuideFileClientDTO,
                ApiResult<string>>(configuration["ActivityServer:DeleteInstallGuideFile"], request);
            return result;
        }

        public async Task<ApiResult<List<InstallGuideCategoryClientDTO>>> GetInstallGuideCategory()
        {
            var client = clientFactory.CreateClient("ActivityServer");
            ApiResult<List<InstallGuideCategoryClientDTO>> result = await
                client.GetAsJsonAsync<ApiResult<List<InstallGuideCategoryClientDTO>>>(configuration["ActivityServer:GetInstallGuideCategory"], null);
            return result;
        }

        public async Task<ApiResult<string>> UpdateInstallGuideInfo(InstallGuideClientDTO request)
        {
            var client = clientFactory.CreateClient("ActivityServer");
            ApiResult<string> result = await
                client.PostAsJsonAsync<InstallGuideClientDTO,
                ApiResult<string>>(configuration["ActivityServer:UpdateInstallGuideInfo"], request);
            return result;
        }
    }
}
