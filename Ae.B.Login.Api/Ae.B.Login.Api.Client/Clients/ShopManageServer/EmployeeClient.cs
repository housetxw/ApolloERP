using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ApolloErp.Log;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ApolloErp.Web.WebApi;
using Ae.B.Login.Api.Client.Interface;
using Ae.B.Login.Api.Client.Request.ShopManage.Employee;
using Ae.B.Login.Api.Client.Response.ShopManage.Employee;
using Ae.B.Login.Api.Common.Constant;
using Ae.B.Login.Api.Common.Exceptions;
using Ae.B.Login.Api.Common.Extension;

namespace Ae.B.Login.Api.Client.Clients.ShopManageServer
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
        public async Task<List<EmployeeResDTO>> GetEmpAndOrgListForLoginByAccountIdAsync(EmployeeListReqDTO req)
        {
            var res = await client.GetAsJsonAsync<EmployeeListReqDTO, ApiResult<List<EmployeeResDTO>>>(configuration["ShopManageServer:GetEmpAndOrgListForLoginByAccountIdAsync"], req);

            //logger.Info($"API: {className}.GetEmpAndOrgListForLoginByAccountIdAsync 返回值：{JsonConvert.SerializeObject(res)}");

            if (res.Code != ResultCode.Success)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenExceptionInfo(CommonConstant.ResultCodeFailed));
                return new List<EmployeeResDTO>();
            }
            return res.Data.Count == 0 ? new List<EmployeeResDTO>() : res.Data.ToList();
        }

        public async Task<ApiPagedResult<EmployeePageDTO>> GetEmpAndOrgPageForLoginByAccountIdAsync(EmployeePageForLoginListReqDTO req)
        {
            ApiPagedResult<EmployeePageDTO> res = new ApiPagedResult<EmployeePageDTO>();

            try
            {
                res = await client.GetAsJsonAsync<EmployeePageForLoginListReqDTO, ApiPagedResult<EmployeePageDTO>>(configuration["ShopManageServer:GetEmpAndOrgPageForLoginByAccountIdAsync"], req);

                //logger.Info($"API: {className}.GetEmpAndOrgPageForLoginByAccountIdAsync 返回值：{JsonConvert.SerializeObject(res)}");

                if (res.Code != ResultCode.Success)
                {
                    logger.Error(JsonConvert.SerializeObject(req).GenExceptionInfo(CommonConstant.ResultCodeFailed));
                    return new ApiPagedResult<EmployeePageDTO>();
                }
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenExceptionInfo(CommonConstant.ResultCodeException), e);
                throw new CustomException(CommonConstant.InternalAPIError);
            }

            return res.Data?.TotalItems == 0 ? new ApiPagedResult<EmployeePageDTO>() : res;
        }

        public async Task<ApiPagedResult<EmployeePageDTO>> GetEmpAndOrgPageForLoginByEmpIdAsync(EmployeePageForLoginListByTokenReqDTO req)
        {
            ApiPagedResult<EmployeePageDTO> res = new ApiPagedResult<EmployeePageDTO>();

            try
            {
                res = await client.GetAsJsonAsync<EmployeePageForLoginListByTokenReqDTO, ApiPagedResult<EmployeePageDTO>>(configuration["ShopManageServer:GetEmpAndOrgPageForLoginByEmpIdAsync"], req);

                //logger.Info($"API: {className}.GetEmpAndOrgPageForLoginByEmpIdAsync 返回值：{JsonConvert.SerializeObject(res)}");

                if (res.Code != ResultCode.Success)
                {
                    logger.Error(JsonConvert.SerializeObject(req).GenExceptionInfo(CommonConstant.ResultCodeFailed));
                    return new ApiPagedResult<EmployeePageDTO>();
                }
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenExceptionInfo(CommonConstant.ResultCodeException), e);
                throw new CustomException(CommonConstant.InternalAPIError);
            }

            return res.Data?.TotalItems == 0 ? new ApiPagedResult<EmployeePageDTO>() : res;
        }

        public async Task<ApiPagedResult<EmployeePageDTO>> GetEmpAndOrgPageForLoginByEmpIdExcCurOrgIdAsync(EmployeePageForLoginListByTokenReqDTO req)
        {
            ApiPagedResult<EmployeePageDTO> res = new ApiPagedResult<EmployeePageDTO>();

            try
            {
                res = await client.GetAsJsonAsync<EmployeePageForLoginListByTokenReqDTO, ApiPagedResult<EmployeePageDTO>>(configuration["ShopManageServer:GetEmpAndOrgPageForLoginByEmpIdExcCurOrgIdAsync"], req);

                //logger.Info($"API: {className}.GetEmpAndOrgPageForLoginByEmpIdExcCurOrgIdAsync 返回值：{JsonConvert.SerializeObject(res)}");

                if (res.Code != ResultCode.Success)
                {
                    logger.Error(JsonConvert.SerializeObject(req).GenExceptionInfo(CommonConstant.ResultCodeFailed));
                    return new ApiPagedResult<EmployeePageDTO>();
                }
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenExceptionInfo(CommonConstant.ResultCodeException), e);
                throw new CustomException(CommonConstant.InternalAPIError);
            }

            return res.Data?.TotalItems == 0 ? new ApiPagedResult<EmployeePageDTO>() : res;
        }

        public async Task<ApiPagedResult<EmployeePageDTO>> GetOrgRangePageForLoginByEmpIdExcCurOrgId(EmployeePageForLoginListByTokenReqDTO req)
        {
            ApiPagedResult<EmployeePageDTO> res = new ApiPagedResult<EmployeePageDTO>();

            try
            {
                res = await client.GetAsJsonAsync<EmployeePageForLoginListByTokenReqDTO, ApiPagedResult<EmployeePageDTO>>(configuration["ShopManageServer:GetOrgRangePageForLoginByEmpIdExcCurOrgId"], req);

                //logger.Info($"API: {className}.GetOrgRangePageForLoginByEmpIdExcCurOrgId 返回值：{JsonConvert.SerializeObject(res)}");

                if (res.Code != ResultCode.Success)
                {
                    logger.Error(JsonConvert.SerializeObject(req).GenExceptionInfo(CommonConstant.ResultCodeFailed));
                    return new ApiPagedResult<EmployeePageDTO>();
                }
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenExceptionInfo(CommonConstant.ResultCodeException), e);
                throw new CustomException(CommonConstant.InternalAPIError);
            }

            return res.Data?.TotalItems == 0 ? new ApiPagedResult<EmployeePageDTO>() : res;
        }

    }
}
