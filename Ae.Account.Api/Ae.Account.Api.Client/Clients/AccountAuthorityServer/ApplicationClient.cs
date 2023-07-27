using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Ae.Account.Api.Client.Interface;
using Ae.Account.Api.Client.Model;
using Ae.Account.Api.Client.Request;
using Ae.Account.Api.Client.Response;

namespace Ae.Account.Api.Client.Clients.AccountAuthorityServer
{
    public class ApplicationClient : IApplicationClient
    {
        private readonly HttpClient client;

        private IConfiguration configuration { get; }

        public ApplicationClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.configuration = configuration;
            client = clientFactory.CreateClient("AccountAuthorityServer");
        }

        public async Task<bool> CreateApplication(ApplicationDTO req)
        {
            var res = await client.PostAsJsonAsync<ApplicationDTO, bool>(configuration["AccountAuthorityServer:CreateApplication"], req);
            return res;
        }

        public async Task<bool> UpdateApplicationById(ApplicationDTO req)
        {
            var res = await client.PostAsJsonAsync<ApplicationDTO, bool>(configuration["AccountAuthorityServer:UpdateApplicationById"], req);
            return res;
        }

        public async Task<bool> DeleteApplicationById(ApplicationDTO req)
        {
            var res = await client.PostAsJsonAsync<ApplicationDTO, bool>(configuration["AccountAuthorityServer:DeleteApplicationById"], req);
            return res;
        }

        public async Task<AppListResDTO> GetPagedApplicationList(AppListReqDTO req)
        {
            var res = await client.GetAsJsonAsync<AppListReqDTO, AppListResDTO>(configuration["AccountAuthorityServer:GetPagedApplicationList"], req);
            return res;
        }

        public async Task<AppResDTO> GetApplicationById(long id)
        {
            var res = await client.GetAsJsonAsync<long, AppResDTO>(configuration["AccountAuthorityServer:GetApplicationById"], id);
            return res;
        }

        public async Task<AppResDTO> GetApplicationByName(string name)
        {
            var res = await client.GetAsJsonAsync<string, AppResDTO>(configuration["AccountAuthorityServer:GetApplicationByName"], name);
            return res;
        }

        public async Task<AppResDTO> GetApplicationByInitialism(string initialism)
        {
            var res = await client.GetAsJsonAsync<string, AppResDTO>(configuration["AccountAuthorityServer:GetApplicationByInitialism"], initialism);
            return res;
        }

        public async Task<AppResDTO> GetApplicationByRoute(string route)
        {
            var res = await client.GetAsJsonAsync<string, AppResDTO>(configuration["AccountAuthorityServer:GetApplicationByRoute"], route);
            return res;
        }

        public async Task<AppResDTO> GetApplication(AppReqDTO req)
        {
            var res = await client.GetAsJsonAsync<AppReqDTO, AppResDTO>(configuration["AccountAuthorityServer:GetApplication"], req);
            return res;
        }

        public async Task<List<AppResDTO>> GetApplicationListAnyCondition(AppReqDTO req)
        {
            var res = await client.GetAsJsonAsync<AppReqDTO, List<AppResDTO>>(configuration["AccountAuthorityServer:GetApplicationListAnyCondition"], req);
            return res;
        }

        public async Task<List<AppResDTO>> GetApplicationList()
        {
            var res = await client.GetAsJsonAsync<List<AppResDTO>>(configuration["AccountAuthorityServer:GetApplicationList"], "");
            return res;
        }

    }
}
