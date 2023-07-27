using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ApolloErp.Log;
using Ae.Account.Api.Client.Interface.AccountAuthorityServer;
using Ae.Account.Api.Client.Model;
using Ae.Account.Api.Client.Request;
using Ae.Account.Api.Client.Response;

namespace Ae.Account.Api.Client.Clients.AccountAuthorityServer
{
    public class EmployeeRoleClient : IEmployeeRoleClient
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly HttpClient client;
        private IConfiguration configuration { get; }
        private readonly string className;

        private readonly ApolloErpLogger<EmployeeRoleClient> logger;

        public EmployeeRoleClient(IHttpClientFactory clientFactory, IConfiguration configuration, ApolloErpLogger<EmployeeRoleClient> logger)
        {
            this.configuration = configuration;
            client = clientFactory.CreateClient("AccountAuthorityServer");
            className = this.GetType().Name;

            this.logger = logger;
        }

        // ---------------------------------- ！！！授权接口实现！！！ --------------------------------------

        public async Task<List<AuthorizationResDTO>> GetEmpAuthorityListByEmpIdAsync(AuthorizationReqDTO req)
        {
            var res = await client.GetAsJsonAsync<AuthorizationReqDTO, List<AuthorizationResDTO>>(configuration["AccountAuthorityServer:GetEmpAuthorityListByEmpIdAsync"], req);

            logger.Info($"API: {className}.GetEmpAuthorityListByEmpIdAsync 返回值：TotalItems:{res.Count}, Items:{JsonConvert.SerializeObject(res)}");

            return res;
        }

        public async Task<List<AuthorizationResDTO>> GetRangeRoleAuthorityListByIds(RangeRoleAuthorityReqDTO req)
        {
            var res = await client.PostAsJsonAsync<RangeRoleAuthorityReqDTO, List<AuthorizationResDTO>>(configuration["AccountAuthorityServer:GetRangeRoleAuthorityListByIds"], req);

            logger.Info($"API: {className}.GetRangeRoleAuthorityListByIds 返回值：TotalItems:{res.Count}, Items:{JsonConvert.SerializeObject(res)}");

            return res;
        }


        // ---------------------------------- 接口实现 --------------------------------------
        public async Task<List<EmployeeRoleListDTO>> GetEmployeeRoleListByEmpIdAsync(EmployeeRoleListReqDTO req)
        {
            var res = await client.GetAsJsonAsync<EmployeeRoleListReqDTO, List<EmployeeRoleListDTO>>(configuration["AccountAuthorityServer:GetEmployeeRoleListByEmpIdAsync"], req);
            return res;
        }

        public async Task<bool> EditEmployeeRole(EmployeeRoleSaveReqDTO req)
        {
            var res = await client.PostAsJsonAsync<EmployeeRoleSaveReqDTO, bool>(configuration["AccountAuthorityServer:EditEmployeeRole"], req);
            return res;
        }


    }
}
