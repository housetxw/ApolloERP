using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Client.Inteface;
using Ae.Shop.Service.Client.Model;
using Ae.Shop.Service.Client.Request;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ae.Shop.Service.Client.Response;
using Ae.Shop.Service.Common.Constant;
using Ae.Shop.Service.Common.Extension;

namespace Ae.Shop.Service.Client.Clients
{
    public class AccountAuthorityClient : IAccountAuthorityClient
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly IHttpClientFactory clientFactory;
        private readonly ApolloErpLogger<AccountAuthorityClient> logger;
        private IConfiguration configuration { get; }
        private readonly string className;

        public AccountAuthorityClient(IHttpClientFactory clientFactory,
            ApolloErpLogger<AccountAuthorityClient> logger,
            IConfiguration configuration
            )
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
            this.logger = logger;

            className = this.GetType().Name;
        }

        // ---------------------------------- 接口实现 --------------------------------------
        /// <summary>
        /// 创建员工账户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<CreateAccountResDTO>> CreateAccountWithIdDefaultPwdAsync(AccountCreateRequest request)
        {
            var client = clientFactory.CreateClient("AccountAuthorityServer");
            ApiResult<CreateAccountResDTO> result = await client.PostAsJsonAsync<AccountCreateRequest, ApiResult<CreateAccountResDTO>>(configuration["AccountAuthorityServer:CreateAccountWithIdDefaultPwd"], request);
            return result;
        }

        public async Task<ApiResult<CheckAccountExistDTO>> CheckAccountIsExistByName(AccountEntityReqByNameDTO req)
        {
            var res = await clientFactory.CreateClient("AccountAuthorityServer")
                    .GetAsJsonAsync<AccountEntityReqByNameDTO, ApiResult<CheckAccountExistDTO>>(configuration["AccountAuthorityServer:CheckAccountIsExistByName"], req);

            if (res.Code == ResultCode.Success) return res;

            logger.Warn(JsonConvert.SerializeObject(req).GenExceptionInfo(CommonConstant.ResultCodeFailed));
            return res;
        }

        /// <summary>
        /// 根据AccountId，删除账号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAccountById(AccountDeleteByIdRequest request)
        {
            var client = clientFactory.CreateClient("AccountAuthorityServer");
            ApiResult<bool> result = await client.PostAsJsonAsync<AccountDeleteByIdRequest, ApiResult<bool>>(configuration["AccountAuthorityServer:DeleteAccountById"], request);
            return result.Data;
        }

        public async Task<List<EmployeeRoleListDTO>> GetEmployeeRoleListByEmpId(EmployeeRoleListReqDTO req)
        {
            ApiResult<List<EmployeeRoleListDTO>> res = new ApiResult<List<EmployeeRoleListDTO>>
            {
                Data = new List<EmployeeRoleListDTO>()
            };

            try
            {
                res = await clientFactory.CreateClient("AccountAuthorityServer")
                    .GetAsJsonAsync<EmployeeRoleListReqDTO, ApiResult<List<EmployeeRoleListDTO>>>
                        (configuration["AccountAuthorityServer:GetEmployeeRoleListByEmpId"], req);

                if (res.Code != ResultCode.Success)
                {
                    logger.Warn(JsonConvert.SerializeObject(req).GenExceptionInfo(CommonConstant.ResultCodeFailed));
                    return res.Data;
                }
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenExceptionInfo(CommonConstant.ResultCodeException), e);
                return new List<EmployeeRoleListDTO>();
            }
            finally
            {
                //logger.Info($"API: {className}.GetEmployeeRoleListByEmpId 请求参数：{JsonConvert.SerializeObject(req)}");
                //logger.Info($"API: {className}.GetEmployeeRoleListByEmpId 返回值：{JsonConvert.SerializeObject(res)}");
            }

            return res.Data;
        }

        /// <summary>
        /// 添加门店员工默认角色
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddShopEmployeeDefaultRole(EmployeeDefaultRoleClientRequest request)
        {
            var client = clientFactory.CreateClient("AccountAuthorityServer");
            ApiResult<bool> result = await client.PostAsJsonAsync<EmployeeDefaultRoleClientRequest, ApiResult<bool>>(configuration["AccountAuthorityServer:AddShopEmployeeDefaultRole"], request);
            return result.Data;
        }

        public async Task<ApiResult<List<RoleDTO>>> GetRoleListByOrgIdAndType(RoleListReqDTO req)
        {
            ApiResult<List<RoleDTO>> res = new ApiResult<List<RoleDTO>>
            {
                Data = new List<RoleDTO>()
            };

            try
            {
                res = await clientFactory.CreateClient("AccountAuthorityServer")
                    .GetAsJsonAsync<RoleListReqDTO, ApiResult<List<RoleDTO>>>(configuration["AccountAuthorityServer:GetRoleListByOrgIdAndType"], req);

                if (res.Code != ResultCode.Success)
                {
                    logger.Warn(JsonConvert.SerializeObject(req).GenExceptionInfo(CommonConstant.ResultCodeFailed));
                    return res;
                }
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenExceptionInfo(CommonConstant.ResultCodeException), e);
            }
            finally
            {
                //logger.Info($"API: {className}.GetRoleListByOrgIdAndType 请求参数：{JsonConvert.SerializeObject(req)}");
                //logger.Info($"API: {className}.GetRoleListByOrgIdAndType 返回值：{JsonConvert.SerializeObject(res)}");
            }

            return res;
        }

        public async Task<List<OrgRangeRolesDTO>> GetRoleListByOrgIds(List<OrgEntityReqDTO> req)
        {
            var resApi = new ApiResult<List<OrgRangeRolesDTO>>
            {
                Data = new List<OrgRangeRolesDTO>()
            };

            try
            {
                resApi = await clientFactory.CreateClient("AccountAuthorityServer")
                    .PostAsJsonAsync<List<OrgEntityReqDTO>, ApiResult<List<OrgRangeRolesDTO>>>
                        (configuration["AccountAuthorityServer:GetRoleListByOrgIds"], req);

                if (resApi?.Code != ResultCode.Success)
                {
                    logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo(CommonConstant.ResultCodeFailed));
                    return resApi?.Data;
                }
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenExceptionInfo(CommonConstant.ResultCodeException), e);
                return resApi?.Data;
            }
            finally
            {
                //logger.Info($"API: {className}.GetRoleListByOrgIds 请求参数：{JsonConvert.SerializeObject(req)}");
                //logger.Info($"API: {className}.GetRoleListByOrgIds 返回值：{JsonConvert.SerializeObject(resApi)}");
            }

            return resApi.Data;
        }

        public async Task<bool> EditEmployeeRoleWithRoleId(EmployeeRoleSaveReqWithRoleIdDTO req)
        {
            var resApi = new ApiResult<bool>();

            try
            {
                resApi = await clientFactory.CreateClient("AccountAuthorityServer")
                    .PostAsJsonAsync<EmployeeRoleSaveReqWithRoleIdDTO, ApiResult<bool>>
                        (configuration["AccountAuthorityServer:EditEmployeeRoleWithRoleId"], req);

                if (resApi?.Code != ResultCode.Success)
                {
                    logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo(CommonConstant.ResultCodeFailed));
                    return false;
                }
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenExceptionInfo(CommonConstant.ResultCodeException), e);
                return false;
            }
            finally
            {
                //logger.Info($"API: {className}.EditEmployeeRoleWithRoleId 请求参数：{JsonConvert.SerializeObject(req)}");
                //logger.Info($"API: {className}.EditEmployeeRoleWithRoleId 返回值：{JsonConvert.SerializeObject(resApi)}");
            }

            return resApi.Data;
        }

        /// <summary>
        /// 获取员工包含角色列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<List<EmployeeRolesDTO>> GetEmployeeRoleListByEmpIds(EmployeeRoleListByEmpIdsReqDTO req)
        {
            ApiResult<List<EmployeeRolesDTO>> res = new ApiResult<List<EmployeeRolesDTO>>
            {
                Data = new List<EmployeeRolesDTO>()
            };

            try
            {
                res = await clientFactory.CreateClient("AccountAuthorityServer")
                    .PostAsJsonAsync<EmployeeRoleListByEmpIdsReqDTO, ApiResult<List<EmployeeRolesDTO>>>
                        (configuration["AccountAuthorityServer:GetEmployeeRoleListByEmpIds"], req);

                if (res.Code != ResultCode.Success)
                {
                    logger.Warn(JsonConvert.SerializeObject(req).GenExceptionInfo(CommonConstant.ResultCodeFailed));
                    return res.Data;
                }
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenExceptionInfo(CommonConstant.ResultCodeException), e);
                return new List<EmployeeRolesDTO>();
            }
            finally
            {
                //logger.Info($"API: {className}.GetEmployeeRoleListByEmpIds 请求参数：{JsonConvert.SerializeObject(req)}");
                //logger.Info($"API: {className}.GetEmployeeRoleListByEmpIds 返回值：{JsonConvert.SerializeObject(res)}");
            }
            return res.Data;
        }

    }
}
