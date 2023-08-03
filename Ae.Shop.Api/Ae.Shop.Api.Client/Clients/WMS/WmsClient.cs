using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Client.Model;
using Ae.Shop.Api.Client.Request;
using Ae.Shop.Api.Client.Response;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Request.ShopWms;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Client.Clients
{
    public class WmsClient : IWmsClient
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;
        private readonly ApolloErpLogger<WmsClient> logger;

        public WmsClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ApolloErpLogger<WmsClient> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
            this.logger = logger;
        }

        public async Task<ApiResult> CreateAsn(CreateAsnRequest request)
        {
            var client = httpClientFactory.CreateClient("WMSServer");

            var response =
                await client
                    .PostAsJsonAsync<CreateAsnRequest, ApiResult<string>>(
                        configuration["WMSServer:CreateAsn"], request);

            return response;
        }

        /// <summary>
        /// 批量创建库存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<BatchCreateStockLocationClientResponse>> BatchCreateStockLocation(BatchCreateStockLocationClientRequest request)
        {
            var client = httpClientFactory.CreateClient("StockServer");

            var result = await client.PostAsJsonAsync<BatchCreateStockLocationClientRequest,
                ApiResult<BatchCreateStockLocationClientResponse>>(configuration["StockServer:BatchCreateStockLocation"], request);
            return result;
        }

        /// <summary>
        /// 批量创建库存扭转记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> BatchCreateStockTranction(BatchCreateStockTranctionClientRequest request)
        {
            var client = httpClientFactory.CreateClient("StockServer");

            var result = await client.PostAsJsonAsync<BatchCreateStockTranctionClientRequest,
                ApiResult>(configuration["StockServer:BatchCreateStockTranction"], request);
            return result;
        }

        /// <summary>
        /// 更新调拨任务状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> UpdateAllotTaskStatus(AllotTaskRequest request)
        {
            var client = httpClientFactory.CreateClient("WMSServer");

            var response =
                await client
                    .PostAsJsonAsync<AllotTaskRequest, ApiResult<string>>(
                        configuration["WMSServer:UpdateAllotTaskStatus"], request);

            return response;
        }

        public async Task<ApiResult<string>> CreateAllotTask(ApiRequest<AllotTaskDTO> request)
        {
            var client = httpClientFactory.CreateClient("WMSServer");

            var response =
                await client
                    .PostAsJsonAsync<ApiRequest<AllotTaskDTO>, ApiResult<string>>(
                        configuration["WMSServer:CreateAllotTask"], request);

            return response;
        }

        public async Task<ApiResult<string>> UpdateProductPackageStatus(PackageProductClientRequest request)
        {
            var client = httpClientFactory.CreateClient("WMSServer");

            var response =
                await client
                    .PostAsJsonAsync<PackageProductClientRequest, ApiResult<string>>(
                        configuration["WMSServer:UpdateProductPackageStatus"], request);

            return response;
        }

        public async Task<ApiResult> UpdateWarehouseTransferProductReceiveNum(UpdateWarehouseTransferProductRequest request)
        {
            var client = httpClientFactory.CreateClient("WMSServer");

            var response =
                await client
                    .PostAsJsonAsync<UpdateWarehouseTransferProductRequest, ApiResult>(
                        configuration["WMSServer:UpdateWarehouseTransferProductReceiveNum"], request);

            return response;
        }

        public async Task<ApiResult<List<SupplierProductStockDTO>>> GetSupplierProductStocks(GetSupplierStockRequest request)
        {
            var client = httpClientFactory.CreateClient("StockServer");

            var response =
                await client
                    .GetAsJsonAsync<GetSupplierStockRequest, ApiResult<List<SupplierProductStockDTO>>>(
                        configuration["StockServer:GetSupplierProductStocks"], request);

            return response;
        }

        public async Task<ApiResult<WarehouseConfigDTO>> GetWarehouseConfig(WarehouseConfigRequest request)
        {
            var client = httpClientFactory.CreateClient("WMSServer");

            var response =
                await client
                    .GetAsJsonAsync<WarehouseConfigRequest, ApiResult<WarehouseConfigDTO>>(
                        configuration["WMSServer:GetWarehouseConfig"], request);

            return response;
        }

        #region 供应商收发货功能

        public async Task<ApiResult<string>> SubmitVenderStock(VenderStockInitRequest request)
        {
            var client = httpClientFactory.CreateClient("WMSServer");

            var response =
                await client
                    .PostAsJsonAsync<VenderStockInitRequest, ApiResult<string>>(
                        configuration["WMSServer:SubmitVenderStock"], request);

            return response;
        }

        public async Task<ApiResult<string>> SubmitCompanyInStock(AcceptCompanyStockRequest request)
        {
            var client = httpClientFactory.CreateClient("WMSServer");

            var response =
                await client
                    .PostAsJsonAsync<AcceptCompanyStockRequest, ApiResult<string>>(
                        configuration["WMSServer:SubmitCompanyInStock"], request);

            return response;

        }

        public async Task<ApiResult<List<VenderStockDTO>>> GetCompanyInStocks(GetCompanyStocksRequest request)
        {
            var client = httpClientFactory.CreateClient("WMSServer");

            var response =
                await client
                    .GetAsJsonAsync<GetCompanyStocksRequest, ApiResult<List<VenderStockDTO>>>(
                        configuration["WMSServer:GetCompanyInStocks"], request);

            return response;

        }

        public async Task<ApiResult<string>> CompanySendStock(AcceptCompanyStockRequest request)
        {
            var client = httpClientFactory.CreateClient("WMSServer");

            var response =
                await client
                    .PostAsJsonAsync<AcceptCompanyStockRequest, ApiResult<string>>(
                        configuration["WMSServer:CompanySendStock"], request);

            return response;

        }

        public async Task<ApiResult<VenderStockResponse>> GetCompanyStocks(GetCompanyStocksRequest request)
        {
            var client = httpClientFactory.CreateClient("WMSServer");

            var response =
                await client
                    .GetAsJsonAsync<GetCompanyStocksRequest, ApiResult<VenderStockResponse>>(
                        configuration["WMSServer:GetCompanyStocks"], request);

            return response;

        }

        public async Task<ApiResult<string>> CancelCompanySendStock(CancelTaskRequest request)
        {
            var client = httpClientFactory.CreateClient("WMSServer");

            var response =
                await client
                    .PostAsJsonAsync<CancelTaskRequest, ApiResult<string>>(
                        configuration["WMSServer:CancelCompanySendStock"], request);

            return response;
        }

        public async Task<ApiResult<ShopOccupyMappingDTO>> GetShopOccupyMappingInfo(GetShopOccupyMappingRequest request)
        {
            var client = httpClientFactory.CreateClient("WMSServer");

            var response =
                await client
                    .GetAsJsonAsync<GetShopOccupyMappingRequest, ApiResult<ShopOccupyMappingDTO>>(
                        configuration["WMSServer:GetShopOccupyMapping"], request);

            return response;

        }

        #endregion
    }
}
