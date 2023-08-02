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
using Ae.B.FMS.Api.Core.Model.Settlement;
using Ae.B.FMS.Api.Core.Request.Settlement;

namespace Ae.B.FMS.Api.Client.Clients
{
    public class SettlementClient : ISettlementClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ApolloErpLogger<SettlementClient> _logger;

        public SettlementClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ApolloErpLogger<SettlementClient> logger)
        {
            this._httpClientFactory = httpClientFactory;
            this._configuration = configuration;
            this._logger = logger;
        }

        public async Task<ApiPagedResult<GetSettlementLisClientDTO>> GetSettlementList(GetSettlementListClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("FMSServer");

            var response =
                await client
                    .PostAsJsonAsync<GetSettlementListClientRequest, ApiPagedResult<GetSettlementLisClientDTO>>(
                        _configuration["FMSServer:GetSettlementList"], request);

            return response;

        }

        public async Task<List<GetSettlementDetailDTO>> GetSettlementDetail(GetSettlementDetailClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("FMSServer");
            ApiResult<List<GetSettlementDetailDTO>> result =
                await client
                    .GetAsJsonAsync<GetSettlementDetailClientRequest,
                        ApiResult<List<GetSettlementDetailDTO>>>(
                        _configuration["FMSServer:GetSettlementDetail"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<GetSettlementDetailDTO>();
            }
            else
            {
                _logger.Error($"GetSettlementDetail_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async Task<ApiResult> SaveSettlementReview(SaveSettlementReviewClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("FMSServer");
            ApiResult result =
                await client.PostAsJsonAsync<SaveSettlementReviewClientRequest, ApiResult>(
                    _configuration["FMSServer:SaveSettlementReview"], request);
            if (result.Code == ResultCode.Success)
            {
                return result;
            }
            else
            {
                _logger.Error($"CreateAccountCheck_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async Task<ApiResult<SettlementPayReviewClientDTO>> SettlementPayReviewDO(GetSettlementDetailClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("FMSServer");
            ApiResult<SettlementPayReviewClientDTO> result =
                await client.GetAsJsonAsync<GetSettlementDetailClientRequest, ApiResult<SettlementPayReviewClientDTO>>(
                    _configuration["FMSServer:SettlementPayReviewDO"], request);
            if (result.Code == ResultCode.Success)
            {
                return result;
            }
            else
            {
                _logger.Error($"SettlementPayReviewDO_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async Task<ApiPagedResult<PurchaseSettlementBatchDTO>> GetPurchaseSettlementList(GetPurchaseSettlementListRequest request)
        {
            var client = _httpClientFactory.CreateClient("FMSServer");
            return
                await client.GetAsJsonAsync<GetPurchaseSettlementListRequest, ApiPagedResult<PurchaseSettlementBatchDTO>>(
                    _configuration["FMSServer:GetPurchaseSettlementList"], request);

        }

        public async Task<ApiResult<List<PurchaseSettlementDetailDTO>>> GetPurchaseSettlementDetail(GetPurchaseSettlementDetailRequest request)
        {
            var client = _httpClientFactory.CreateClient("FMSServer");
            return
                await client.GetAsJsonAsync<GetPurchaseSettlementDetailRequest, ApiResult<List<PurchaseSettlementDetailDTO>>>(
                    _configuration["FMSServer:GetPurchaseSettlementDetail"], request);
        }

        public async Task<ApiResult<string>> SavePurchaseSettlementOrder(ApiRequest<SavePurchaseSettlementOrderRequest> request)
        {
            var client = _httpClientFactory.CreateClient("FMSServer");
            return
                   await client.PostAsJsonAsync<ApiRequest<SavePurchaseSettlementOrderRequest>, ApiResult<string>>(
                       _configuration["FMSServer:SavePurchaseSettlementOrder"], request);
        }

        public async Task<ApiResult<string>> SavePurchaseSettlementReview(ApiRequest<SavePurchaseSettlementReviewRequest> request)
        {
            var client = _httpClientFactory.CreateClient("FMSServer");
            return
                   await client.PostAsJsonAsync<ApiRequest<SavePurchaseSettlementReviewRequest>, ApiResult<string>>(
                       _configuration["FMSServer:SavePurchaseSettlementReview"], request);
        }
    }
}
