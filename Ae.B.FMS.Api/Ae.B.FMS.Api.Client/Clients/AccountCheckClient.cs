using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.B.FMS.Api.Client.Request;
using Ae.B.FMS.Api.Client;
using Ae.B.FMS.Api.Common.Exceptions;
using Ae.B.FMS.Api.Client.Interface;
using Ae.B.FMS.Api.Client.Model;
using Ae.B.FMS.Api.Core.Model.AccountCheck;
using Ae.B.FMS.Api.Core.Request.AccountCheck;

namespace Ae.B.FMS.Api.Client.Clients
{
  public  class AccountCheckClient: IAccountCheckClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ApolloErpLogger<AccountCheckClient> _logger;

        public AccountCheckClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ApolloErpLogger<AccountCheckClient> logger)
        {
            this._httpClientFactory = httpClientFactory;
            this._configuration = configuration;
            this._logger = logger;
        }

        public async  Task<string> CreateAccountCheckException(AccountCheckExceptionClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("FMSServer");
            ApiResult<string> result =
                await client.PostAsJsonAsync<AccountCheckExceptionClientRequest, ApiResult<string>>(
                    _configuration["FMSServer:CreateAccountCheckException"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Error($"CreateAccountCheckException_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async  Task<string> CreateAccountChecks(CreateAccountCheckClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("FMSServer");
            ApiResult<string> result =
                await client.PostAsJsonAsync<CreateAccountCheckClientRequest, ApiResult<string>>(
                    _configuration["FMSServer:CreateAccountCheck"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Error($"CreateAccountCheck_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async  Task<List<AccountCheckLogDTO>> GetAccountCheckLogs(AccountCheckLogClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("FMSServer");
            ApiResult<List<AccountCheckLogDTO>> result =
                await client
                    .GetAsJsonAsync<AccountCheckLogClientRequest,
                        ApiResult<List<AccountCheckLogDTO>>>(
                        _configuration["FMSServer:GetAccountCheckLogs"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<AccountCheckLogDTO>();
            }
            else
            {
                _logger.Error($"GetVehicleSalesNameByNian_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async  Task<ApiPagedResult<AccountCheckDTO>> GetShopAccountChecks(GetAccountCheckClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("FMSServer");

            var response =
                await client
                    .PostAsJsonAsync<GetAccountCheckClientRequest, ApiPagedResult<AccountCheckDTO>>(
                        _configuration["FMSServer:GetShopAccountChecks"], request);

            return response;
        }

        public async Task<ApiPagedResult<AccountCheckDTO>> GetRgAccountChecks(GetAccountCheckClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("FMSServer");

            var response =
                await client
                    .PostAsJsonAsync<GetAccountCheckClientRequest, ApiPagedResult<AccountCheckDTO>>(
                        _configuration["FMSServer:GetRgAccountChecks"], request);

            return response;
        }

        public async  Task<ApiPagedResult<AccountCheckDTO>> GetShopAccountUnChecks(GetAccountCheckClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("FMSServer");

            var response =
                await client
                    .PostAsJsonAsync<GetAccountCheckClientRequest, ApiPagedResult<AccountCheckDTO>>(
                        _configuration["FMSServer:GetShopAccountUnChecks"], request);

            return response;
        }

        public async Task<string> UpdateRgAccountCheckResult(RgAccountCheckConfirmClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("FMSServer");
            ApiResult<string> result =
                await client.PostAsJsonAsync<RgAccountCheckConfirmClientRequest, ApiResult<string>>(
                    _configuration["FMSServer:UpdateRgAccountCheckResult"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Error($"CreateAccountCheck_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async Task<string> RgAccountCheckWithdraw(RgAccountCheckWithdrawClientRequeset request) {
            var client = _httpClientFactory.CreateClient("FMSServer");
            ApiResult<string> result =
                await client.PostAsJsonAsync<RgAccountCheckWithdrawClientRequeset, ApiResult<string>>(
                    _configuration["FMSServer:RgAccountCheckWithdraw"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Error($"CreateAccountCheck_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

            //GetAccountCheckCollects
        public async  Task<ApiPagedResult<AccountCheckExceptionCollectClientDTO>> GetAccountCheckExceptionCollectList(GetAccountCheckClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("FMSServer");

            var response =
                await client
                    .PostAsJsonAsync<GetAccountCheckClientRequest, ApiPagedResult<AccountCheckExceptionCollectClientDTO>>(
                        _configuration["FMSServer:GetAccountCheckExceptionCollectList"], request);

            return response;
        }

        public async  Task<ApiPagedResult<AccountCheckCollectDTO>> GetAccountCheckCollects(GetAccountCheckCollectClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("FMSServer");

            var response =
                await client
                    .GetAsJsonAsync<GetAccountCheckCollectClientRequest, ApiPagedResult<AccountCheckCollectDTO>>(
                        _configuration["FMSServer:GetAccountCheckCollects"], request);

            return response;
        }

        public async  Task<string> AccountCheckExceptionHandle(AccountCheckExceptionHandleClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("FMSServer");
            ApiResult<string> result =
                await client.PostAsJsonAsync<AccountCheckExceptionHandleClientRequest, ApiResult<string>>(
                    _configuration["FMSServer:AccountCheckExceptionHandle"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Error($"AccountCheckExceptionHandle_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async  Task<ApiPagedResult<AccountCheckExceptionCollectClientDTO>> GetAccountCheckExceptionMonitorList(GetAccountCheckClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("FMSServer");

            var response =
                await client
                    .PostAsJsonAsync<GetAccountCheckClientRequest, ApiPagedResult<AccountCheckExceptionCollectClientDTO>>(
                        _configuration["FMSServer:GetAccountCheckExceptionMonitorList"], request);

            return response;

        }

        public async Task<ApiPagedResult<PurchaseAccountCheckDTO>> GetPurchaseAccountList(GetPurchaseAccountListRequest request)
        {
            var client = _httpClientFactory.CreateClient("FMSServer");
            return
                await client.GetAsJsonAsync<GetPurchaseAccountListRequest, ApiPagedResult<PurchaseAccountCheckDTO>>(
                    _configuration["FMSServer:GetPurchaseAccountList"], request);
        }

        public async Task<ApiResult<string>> SavePurchaseAccountRecord(ApiRequest<SavePurchaseAccountRecordRequest> request)
        {
            var client = _httpClientFactory.CreateClient("FMSServer");
            return
                   await client.PostAsJsonAsync<ApiRequest<SavePurchaseAccountRecordRequest>, ApiResult<string>>(
                       _configuration["FMSServer:SavePurchaseAccountRecord"], request);
        }

        public async Task<ApiResult<string>> SavePurchaseExceptionAccount(ApiRequest<SavePurchaseExceptionAccountRequest> request)
        {
            var client = _httpClientFactory.CreateClient("FMSServer");
            return
                   await client.PostAsJsonAsync<ApiRequest<SavePurchaseExceptionAccountRequest>, ApiResult<string>>(
                       _configuration["FMSServer:SavePurchaseExceptionAccount"], request);
        }
    }
}
