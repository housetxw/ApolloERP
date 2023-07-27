using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Account.Api.Client.Interface;
using Ae.Account.Api.Client.Model;
using Ae.Account.Api.Client.Request;
using Ae.Account.Api.Client.Response;
using Ae.Account.Api.Common.Constant;
using Ae.Account.Api.Common.Extension;

namespace Ae.Account.Api.Client.Clients
{
    public class EmployeeClient : IEmployeeClient
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly HttpClient client;
        private IConfiguration configuration { get; }
        private readonly ApolloErpLogger<EmployeeClient> logger;
        private readonly string className;

        public EmployeeClient(ApolloErpLogger<EmployeeClient> logger, IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.configuration = configuration;
            client = clientFactory.CreateClient("ShopManageServer");

            this.logger = logger;
            className = this.GetType().Name;
        }

        // ---------------------------------- 接口实现 --------------------------------------
        public async Task<List<EmployeeResDTO>> GetAllEmployeeListAsync()
        {
            var res = await client.GetAsJsonAsync<ApiResult<List<EmployeeResDTO>>>(configuration["ShopManageServer:GetAllEmployeeListAsync"], "");
            if (res.Code != ResultCode.Success)
            {
                logger.Warn(JsonConvert.SerializeObject(res).GenArgumentErrorInfo(CommonConstant.ResultCodeFailed));
                return new List<EmployeeResDTO>();
            }

            logger.Info($"API: {className}.GetAllEmployeeListAsync 返回值：{JsonConvert.SerializeObject(res)}");
            return res.Data.ToList();
        }

        public async Task<List<EmployeeResDTO>> GetEmployeeListByOrgIdAndTypeAsync(EmployeeListReqDTO req)
        {
            var res = await client.GetAsJsonAsync<EmployeeListReqDTO, ApiResult<List<EmployeeResDTO>>>(configuration["ShopManageServer:GetEmployeeListByOrgIdAndTypeAsync"], req);
            if (res.Code != ResultCode.Success)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenExceptionInfo(CommonConstant.ResultCodeFailed));
                return new List<EmployeeResDTO>();
            }

            logger.Info($"API: {className}.GetEmployeeListByOrgIdAndTypeAsync 返回值：{JsonConvert.SerializeObject(res)}");
            return res.Data.ToList();
        }

        public async Task<ApiPagedResultData<EmployeePageDTO>> GetEmployeePage(EmployeePageReqDTO req)
        {
            var res = await client.PostAsJsonAsync<EmployeePageReqDTO, ApiPagedResult<EmployeePageDTO>>(configuration["ShopManageServer:GetEmployeePage"], req);
            if (res.Code != ResultCode.Success)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenExceptionInfo(CommonConstant.ResultCodeFailed));
                return new ApiPagedResultData<EmployeePageDTO>();
            }

            logger.Info($"API: {className}.GetEmployeePage 返回值：{JsonConvert.SerializeObject(res)}");
            return res.Data;
        }

        public async Task<List<long>> GetOrgRangeRoleIdList(OrgRangeRoleListForLoginReqDTO req)
        {
            var res = await client.GetAsJsonAsync<OrgRangeRoleListForLoginReqDTO, ApiResult<List<long>>>(configuration["ShopManageServer:GetOrgRangeRoleIdList"], req);
            if (res.Code != ResultCode.Success)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenExceptionInfo(CommonConstant.ResultCodeFailed));
                return new List<long>();
            }

            logger.Info($"API: {className}.GetEmployeePage 请求参数：{JsonConvert.SerializeObject(req)}");
            logger.Info($"API: {className}.GetEmployeePage 返回值：{JsonConvert.SerializeObject(res)}");

            return res.Data;
        }


    }
}
