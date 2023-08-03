using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Model.Fiance;
using Ae.Shop.Api.Core.Request.Fiance;
using Ae.Shop.Api.Core.Response.Fiance;

namespace Ae.Shop.Api.Client.Clients.Fiance
{
    public class FianceClient : IFianceClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private IConfiguration _configuration { get; }

        public FianceClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }
        /// <summary>
        /// 核对账单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> CreateAccountCheck(CreateAccountCheckClientRequest request)
        {
            var client = _clientFactory.CreateClient("FMSServer");
            ApiResult response =
                await client.PostAsJsonAsync<CreateAccountCheckClientRequest, ApiResult>(_configuration["FMSServer:CreateAccountCheck"], request);
            return response;
        }

        /// <summary>
        /// 查询门店未对账列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<AccountCheckDTO>> GetShopAccountUnChecks(GetAccountCheckRequest request)
        {
            var client = _clientFactory.CreateClient("FMSServer");
            var responseValue = JsonConvert.SerializeObject(request);
            ApiPagedResult<AccountCheckDTO> response =
                await client.PostAsJsonAsync<GetAccountCheckRequest, ApiPagedResult<AccountCheckDTO>>(_configuration["FMSServer:GetShopAccountUnChecks"], request);
            return response;
        }

        /// <summary>
        /// 门店已对账列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<AccountCheckDTO>> GetShopAccountChecks(GetAccountCheckRequest request)
        {
            var client = _clientFactory.CreateClient("FMSServer");
            ApiPagedResult<AccountCheckDTO> response =
                await client.PostAsJsonAsync<GetAccountCheckRequest, ApiPagedResult<AccountCheckDTO>>(_configuration["FMSServer:GetShopAccountChecks"], request);
            return response;
        }

        /// <summary>
        /// 已对账列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<AccountCheckDTO>> GetRgAccountChecks(GetAccountCheckRequest request)
        {
            var client = _clientFactory.CreateClient("FMSServer");
            ApiPagedResult<AccountCheckDTO> response =
                await client.PostAsJsonAsync<GetAccountCheckRequest, ApiPagedResult<AccountCheckDTO>>(_configuration["FMSServer:GetRgAccountChecks"], request);
            return response;
        }

        /// <summary>
        /// 异常订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<AccountCheckExceptionCollectDTO>> GetAccountCheckExceptionCollectList(GetAccountCheckRequest request)
        {
            var client = _clientFactory.CreateClient("FMSServer");
            ApiPagedResult<AccountCheckExceptionCollectDTO> response =
                await client.PostAsJsonAsync<GetAccountCheckRequest, ApiPagedResult<AccountCheckExceptionCollectDTO>>(_configuration["FMSServer:GetAccountCheckExceptionCollectList"], request);
            return response;
        }

        /// <summary>
        /// 查询日志
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<AccountCheckLogDTO>>> GetAccountCheckLogs(GetAccountCheckLogRequest request)
        {
            var client = _clientFactory.CreateClient("FMSServer");
            ApiResult<List<AccountCheckLogDTO>> response =
                await client.GetAsJsonAsync<GetAccountCheckLogRequest, ApiResult<List<AccountCheckLogDTO>>>(_configuration["FMSServer:GetAccountCheckLogs"], request);
            return response;
        }

        /// <summary>
        /// 得到提现记录列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<GetWithdrawalRecordListResponse>> GetWithdrawalRecordList(GetWithdrawalRecordListRequest request)
        {
            var client = _clientFactory.CreateClient("FMSServer");
            ApiPagedResult<GetWithdrawalRecordListResponse> response =
                await client.GetAsJsonAsync<GetWithdrawalRecordListRequest, ApiPagedResult<GetWithdrawalRecordListResponse>>(_configuration["FMSServer:GetWithdrawalRecordList"], request);
            return response;
        }

        /// <summary>
        /// 得到开始提现申请的信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetWithdrawalApplyResponse>> GetWithdrawalApply(GetWithdrawalApplyRequest request)
        {
            var client = _clientFactory.CreateClient("FMSServer");
            ApiResult<GetWithdrawalApplyResponse> response =
                await client.GetAsJsonAsync<GetWithdrawalApplyRequest, ApiResult<GetWithdrawalApplyResponse>>(_configuration["FMSServer:GetWithdrawalApply"], request);
            return response;
        }

        /// <summary>
        /// 结算单详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<GetWithdrawalOrderRecordListResponse>> GetWithdrawalOrderRecordList(GetWithdrawalOrderRecordListRequest request)
        {
            var client = _clientFactory.CreateClient("FMSServer");
            ApiPagedResult<GetWithdrawalOrderRecordListResponse> response =
                await client.GetAsJsonAsync<GetWithdrawalOrderRecordListRequest, ApiPagedResult<GetWithdrawalOrderRecordListResponse>>(_configuration["FMSServer:GetWithdrawalOrderRecordList"], request);
            return response;
        }

        /// <summary>
        /// 发起提现申请
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<SubmitWithdrawalApplyResponse>> SubmitWithdrawalApply(SubmitWithdrawalApplyRequest request)
        {
            var client = _clientFactory.CreateClient("FMSServer");
            ApiResult<SubmitWithdrawalApplyResponse> response =
                await client.PostAsJsonAsync<SubmitWithdrawalApplyRequest, ApiResult<SubmitWithdrawalApplyResponse>>(_configuration["FMSServer:SubmitWithdrawalApply"], request);
            return response;
        }

        public async Task<ApiResult<string>> CalculationPurchaseReconciliationFee(CalculatePurchaseOrderRequest request)
        {
            var client = _clientFactory.CreateClient("FMSServer");
            ApiResult<string> response =
                await client.PostAsJsonAsync<CalculatePurchaseOrderRequest, ApiResult<string>>(_configuration["FMSServer:CalculationPurchaseReconciliationFee"], request);
            return response;
        }

        public async Task<ApiResult> CalculationReconciliationFee(CalculationReconciliationFeeRequest request)
        {
            var client = _clientFactory.CreateClient("FMSServer");
            ApiResult response =
                await client.GetAsJsonAsync<CalculationReconciliationFeeRequest, ApiResult>(_configuration["FMSServer:CalculationReconciliationFee"], request);
            return response;
        }

        public async Task<ApiResult> CalculationReconciliationFeeBatch(CreateAccountCheckClientRequest request)
        {
            var client = _clientFactory.CreateClient("FMSServer");
            ApiResult response =
                await client.PostAsJsonAsync<CreateAccountCheckClientRequest, ApiResult>(_configuration["FMSServer:CalculationReconciliationFeeBatch"], request);
            return response;
        }
    }
}
