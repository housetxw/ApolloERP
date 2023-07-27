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
    public class RoleClient : IRoleClient
    {
        #region 变量和构造器

        private readonly HttpClient client;

        private IConfiguration configuration { get; }

        public RoleClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.configuration = configuration;
            client = clientFactory.CreateClient("AccountAuthorityServer");
        }

        #endregion 变量和构造器

        #region 接口实现

        public async Task<bool> CreateRole(RoleDTO req)
        {
            var res = await client.PostAsJsonAsync<RoleDTO, bool>(configuration["AccountAuthorityServer:CreateRole"], req);
            return res;
        }

        public async Task<bool> SaveRoleAuthority(RoleAuthorityReqDTO req)
        {
            var res = await client.PostAsJsonAsync<RoleAuthorityReqDTO, bool>(configuration["AccountAuthorityServer:SaveRoleAuthority"], req);
            return res;
        }

        public async Task<bool> UpdateRoleById(RoleDTO req)
        {
            var res = await client.PostAsJsonAsync<RoleDTO, bool>(configuration["AccountAuthorityServer:UpdateRoleById"], req);
            return res;
        }

        public async Task<bool> DeleteRoleById(RoleDTO req)
        {
            var res = await client.PostAsJsonAsync<RoleDTO, bool>(configuration["AccountAuthorityServer:DeleteRoleById"], req);
            return res;
        }

        public async Task<RolePageListResDTO> GetPagedRoleList(RoleListReqDTO req)
        {
            var res = await client.GetAsJsonAsync<RoleListReqDTO, RolePageListResDTO>(configuration["AccountAuthorityServer:GetPagedRoleList"], req);
            return res;
        }

        public async Task<List<RoleDTO>> GetAllRoleList()
        {
            var res = await client.GetAsJsonAsync<List<RoleDTO>>(configuration["AccountAuthorityServer:GetAllRoleList"], "");
            return res;
        }

        public async Task<List<RoleDTO>> GetRoleListByOrgIdAsync(RoleListReqDTO req)
        {
            var res = await client.GetAsJsonAsync<RoleListReqDTO, List<RoleDTO>>(configuration["AccountAuthorityServer:GetRoleListByOrgIdAsync"], req);
            return res;
        }

        public async Task<List<RolePageResDTO>> GetRoleListAnyCondition(RoleListInternalReqDTO req)
        {
            var res = await client.GetAsJsonAsync<RoleListInternalReqDTO, List<RolePageResDTO>>(configuration["AccountAuthorityServer:GetRoleListAnyCondition"], req);
            return res;
        }

        public async Task<List<RoleAuthorityDTO>> GetRoleAuthorityListByRoleId(RoleAuthorityListReqByRoleIdDTO req)
        {
            var res = await client.GetAsJsonAsync<RoleAuthorityListReqByRoleIdDTO, List<RoleAuthorityDTO>>(configuration["AccountAuthorityServer:GetRoleAuthorityListByRoleId"], req);
            return res;
        }


        #endregion 接口实现

        #region 私有方法

        #endregion 私有方法
    }
}
