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
    public class AuthorityClient : IAuthorityClient
    {
        #region 变量和构造器

        private readonly HttpClient client;

        private IConfiguration configuration { get; }

        public AuthorityClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.configuration = configuration;
            client = clientFactory.CreateClient("AccountAuthorityServer");
        }

        #endregion 变量和构造器

        #region 接口实现

        public async Task<bool> CreateAuthority(AuthorityDTO req)
        {
            var res = await client.PostAsJsonAsync<AuthorityDTO, bool>(configuration["AccountAuthorityServer:CreateAuthority"], req);
            return res;
        }

        public async Task<bool> UpdateAuthorityById(AuthorityDTO req)
        {
            var res = await client.PostAsJsonAsync<AuthorityDTO, bool>(configuration["AccountAuthorityServer:UpdateAuthorityById"], req);
            return res;
        }

        public async Task<bool> DeleteAuthorityById(AuthorityDTO req)
        {
            var res = await client.PostAsJsonAsync<AuthorityDTO, bool>(configuration["AccountAuthorityServer:DeleteAuthorityById"], req);
            return res;
        }

        public async Task<AuthorityPageListResDTO> GetPagedAuthorityList(AuthorityListReqDTO req)
        {
            var res = await client.GetAsJsonAsync<AuthorityListReqDTO, AuthorityPageListResDTO>(configuration["AccountAuthorityServer:GetPagedAuthorityList"], req);
            return res;
        }

        public async Task<List<AuthorityDTO>> GetAllAuthorityList()
        {
            var res = await client.GetAsJsonAsync<List<AuthorityDTO>>(configuration["AccountAuthorityServer:GetAllAuthorityList"], "");
            return res;
        }

        public async Task<List<AuthorityDTO>> GetAuthorityListByApplicationIdAsync(AuthorityListReqDTO req)
        {
            var res = await client.GetAsJsonAsync<AuthorityListReqDTO, List<AuthorityDTO>>(configuration["AccountAuthorityServer:GetAuthorityListByApplicationIdAsync"], req);
            return res;
        }

        public async Task<List<AuthorityDTO>> GetAuthorityListByApplicationIdExceptIdAsync(AuthorityListReqDTO req)
        {
            var res = await client.GetAsJsonAsync<AuthorityListReqDTO, List<AuthorityDTO>>(configuration["AccountAuthorityServer:GetAuthorityListByApplicationIdExceptIdAsync"], req);
            return res;
        }

        public async Task<List<AuthorityPageResDTO>> GetAuthorityListAnyCondition(AuthorityListInternalReqDTO req)
        {
            var res = await client.GetAsJsonAsync<AuthorityListInternalReqDTO, List<AuthorityPageResDTO>>(configuration["AccountAuthorityServer:GetAuthorityListAnyCondition"], req);
            return res;
        }

        public async Task<List<AuthorityPageResDTO>> GetAuthorityListWithApplicationInfo(AuthorityListInternalReqDTO req)
        {
            var res = await client.GetAsJsonAsync<AuthorityListInternalReqDTO,List<AuthorityPageResDTO>>(configuration["AccountAuthorityServer:GetAuthorityListWithApplicationInfo"], req);
            return res;
        }

        public async Task<List<AuthorityDTO>> GetParentAuthorityListByAppIdAndAuthIdAsync(AuthorityListReqDTO req)
        {
            var res = await client.GetAsJsonAsync<AuthorityListReqDTO, List<AuthorityDTO>>(configuration["AccountAuthorityServer:GetParentAuthorityListByAppIdAndAuthIdAsync"], req);
            return res;
        }

        #endregion 接口实现

        #region 私有方法

        #endregion 私有方法

    }
}
