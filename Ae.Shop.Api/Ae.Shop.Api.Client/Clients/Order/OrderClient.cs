using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Client.Request;
using Ae.Shop.Api.Client.Response;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Model.Order;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Request.Order;
using Ae.Shop.Api.Core.Response.Order;

namespace Ae.Shop.Api.Client.Clients.Order
{
    public class OrderClient : IOrderClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ApolloErpLogger<OrderClient> _logger;

        public OrderClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ApolloErpLogger<OrderClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// 得到订单基本信息集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<OrderDTO>>> GetOrderBaseInfo(GetOrderBaseInfoRequest request)
        {
            var client = _httpClientFactory.CreateClient("OrderServer");
            var response =
                 await client
                    .PostAsJsonAsync<GetOrderBaseInfoRequest, ApiResult<List<OrderDTO>>>(
                        _configuration["OrderServer:GetOrderBaseInfo"], request);
            return response;
        }

        public async Task<ApiResult<List<OrderCarDTO>>> GetOrderCarsInfo(GetOrderCarsRequest request)
        {
            var client = _httpClientFactory.CreateClient("OrderServer");
            var response =
                 await client
                    .PostAsJsonAsync<GetOrderCarsRequest, ApiResult<List<OrderCarDTO>>>(
                        _configuration["OrderServer:GetOrderCarsInfo"], request);
            return response;
        }

                
        public async Task<ApiResult<List<OrderDispatchDTO>>> GetOrderDispatch(GetOrderDispatchRequest request)
        {
            var client = _httpClientFactory.CreateClient("ShopOrderServer");
            var response =
                 await client
                    .PostAsJsonAsync<GetOrderDispatchRequest, ApiResult<List<OrderDispatchDTO>>>(
                        _configuration["ShopOrderServer:GetOrderDispatch"], request);
            return response;
        }





        /// <summary>
        /// 得到订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<OrderDTO>> GetOrderInfoListForShop(GetOrderInfoListForShopRequest request)
        {
            var client = _httpClientFactory.CreateClient("OrderServer");
            var response=
                 await client
                    .PostAsJsonAsync<GetOrderInfoListForShopRequest, ApiPagedResult<OrderDTO>>(
                        _configuration["OrderServer:GetOrderInfoListForShop"], request);
            return response;
        }

        public async Task<ApiResult<OrderInsuranceCompanyDTO>> GetOrderInsuranceCompany(GetOrderInsuranceCompanyRequest request)
        {
            var client = _httpClientFactory.CreateClient("ShopOrderServer");

            var response =
                await client
                    .GetAsJsonAsync<GetOrderInsuranceCompanyRequest, ApiResult<OrderInsuranceCompanyDTO>>(
                        _configuration["ShopOrderServer:GetOrderInsuranceCompany"], request);

            return response;
        }
       

        /// <summary>
        /// 得到订单产品信息集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<OrderProductDTO>>> GetOrderProduct(GetOrderProductRequest request)
        {
            var client = _httpClientFactory.CreateClient("OrderServer");
            var response =
                 await client
                    .PostAsJsonAsync<GetOrderProductRequest, ApiResult<List<OrderProductDTO>>>(
                        _configuration["OrderServer:GetOrderProduct"], request);
            return response;
        }



        /// <summary>
        /// 订单占库存结果通知
        /// </summary>
        /// <returns></returns>
        public async Task<string> OrderUseStockNotify(OrderUseStockNotifyClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ShopOrderServer");

            var result = await client.PostAsJsonAsync<OrderUseStockNotifyClientRequest,
                ApiResult>(_configuration["ShopOrderServer:OrderUseStockNotify"], request);
            return result.Message;
        }

        /// <summary>
        /// 查询占库存订单详情
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<QueryUseStockOrderDetailResponse>> QueryUseStockOrderDetail(QueryUseStockOrderDetailClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ShopOrderServer");

            var result = await client.GetAsJsonAsync<QueryUseStockOrderDetailClientRequest,
                ApiResult<QueryUseStockOrderDetailResponse>>(_configuration["ShopOrderServer:QueryUseStockOrderDetail"], request);
            return result;

        }

        /// <summary>
        /// 查询订单实物和外采产品详情
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<QueryOrderDetailUseStockResponse>> QueryOrderRealProductDetail(QueryUseStockOrderDetailClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ShopOrderServer");

            var result = await client.GetAsJsonAsync<QueryUseStockOrderDetailClientRequest,
                ApiResult<QueryOrderDetailUseStockResponse>>(_configuration["ShopOrderServer:QueryOrderRealProductDetail"], request);
            return result;

        }

        #region  订单报表

        public async Task<ApiResult<GetOrderNotCompleteStaticReportResponse>> GetOrderNotCompleteStaticReport(GetOrderNotCompleteStaticReportRequest request)
        {
            var client = _httpClientFactory.CreateClient("OrderServer");
            var response = await client.GetAsJsonAsync<GetOrderNotCompleteStaticReportRequest, ApiResult<GetOrderNotCompleteStaticReportResponse>>(_configuration["OrderServer:GetOrderNotCompleteStaticReport"], request);
            return response;
        }

        public async Task<ApiPagedResult<GetOrderOutProductsProfitResponse>> GetOrderOutProductsProfitReport(ApiRequest<GetOrderOutProductsProfitRequest> request)
        {
            var client = _httpClientFactory.CreateClient("ShopOrderServer");
            var response = await client.PostAsJsonAsync<ApiRequest<GetOrderOutProductsProfitRequest>, ApiPagedResult<GetOrderOutProductsProfitResponse>>(_configuration["ShopOrderServer:GetOrderOutProductsProfitReport"], request);
            return response;
        }
        public async Task<ApiResult<GetOrderCompleteStaticReportResponse>> GetOrderCompleteStaticReport(GetOrderCompleteStaticReportRequest request)
        {
            var client = _httpClientFactory.CreateClient("OrderServer");
            var response = await client.GetAsJsonAsync<GetOrderCompleteStaticReportRequest, ApiResult<GetOrderCompleteStaticReportResponse>>(_configuration["OrderServer:GetOrderCompleteStaticReport"], request);
            return response;
        }

        public async Task<ApiPagedResult<GetOrderDetailStaticReportResponse>> GetOrderDetailStaticReport(GetOrderDetailStaticReportRequest request)
        {
            var client = _httpClientFactory.CreateClient("ShopOrderServer");
            var response = await client.PostAsJsonAsync<GetOrderDetailStaticReportRequest, ApiPagedResult<GetOrderDetailStaticReportResponse>>(_configuration["ShopOrderServer:GetOrderDetailStaticReport"], request);
            return response;
        }

        public async Task<ApiPagedResult<OrderProductNewDTO>> GetOrderProductsReport(ApiRequest<GetOrderProductsRequest> request)
        {
            var client = _httpClientFactory.CreateClient("ShopOrderServer");
            var response = await client.PostAsJsonAsync<ApiRequest<GetOrderProductsRequest>, ApiPagedResult<OrderProductNewDTO>>(_configuration["ShopOrderServer:GetOrderProductsReport"], request);
            return response;
        }
        #endregion

        #region  技师绩效

        public async Task<ApiResult<List<EmployeePerformanceOrderDTO>>> GetEmployeePerformanceOrderList(EmployeePerformanceRequest request)
        {
            var client = _httpClientFactory.CreateClient("ShopOrderServer");
            var response = await client.GetAsJsonAsync<EmployeePerformanceRequest, ApiResult<List<EmployeePerformanceOrderDTO>>>(
                    _configuration["ShopOrderServer:GetEmployeePerformanceOrderList"], request);
            return response;
        }

        /// <summary>
        /// 更新员工绩效订单
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult> UpdateEmployeePerformanceOrder(UpdateEmployeePerformanceRequest request)
        {
            var client = _httpClientFactory.CreateClient("ShopOrderServer");

            var result = await client.PostAsJsonAsync<UpdateEmployeePerformanceRequest,
                ApiResult>(_configuration["ShopOrderServer:UpdateEmployeePerformanceOrder"], request);
            return result;
        }

        #endregion

    }
}
