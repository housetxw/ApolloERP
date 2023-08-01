﻿using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.BaoYang.Service.Client.Request;
using Ae.BaoYang.Service.Client.Response;
using Ae.BaoYang.Service.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Ae.BaoYang.Service.Client.Model;

namespace Ae.BaoYang.Service.Client.Clients
{
    public class StockClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ApolloErpLogger<StockClient> _logger;

        public StockClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ApolloErpLogger<StockClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// 大仓库存查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<List<WarehouseStockForAdaptationResponse>> GetWarehouseStockForAdaptation(
            WarehouseStockForAdaptationRequest request)
        {
            var client = _httpClientFactory.CreateClient("StockServer");
            ApiResult<List<WarehouseStockForAdaptationResponse>> result =
                await client
                    .PostAsJsonAsync<WarehouseStockForAdaptationRequest,
                        ApiResult<List<WarehouseStockForAdaptationResponse>>>(
                        _configuration["StockServer:GetWarehouseStockForAdaptation"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<WarehouseStockForAdaptationResponse>();
            }
            else
            {
                _logger.Error($"GetWarehouseStockForAdaptation_Error {result.Message}");
                return new List<WarehouseStockForAdaptationResponse>();
            }
        }

        /// <summary>
        /// 大仓库存查询
        /// </summary>
        /// <param name="provinceId"></param>
        /// <param name="cityId"></param>
        /// <param name="pidList"></param>
        /// <returns></returns>
        public async Task<List<WarehouseStock>> GetWarehouseStockForAdaptation(string provinceId,
            string cityId, List<string> pidList)
        {
            List<WarehouseStock> warehouseStocks = new List<WarehouseStock>();
            if (pidList == null || !pidList.Any())
            {
                return warehouseStocks;
            }

            var result = await GetWarehouseStockForAdaptation(new WarehouseStockForAdaptationRequest()
            {
                ProvinceId = provinceId.ToString(),
                CityId = cityId.ToString(),
                ProductCodes = pidList
            });
            if (result != null && result.Any())
            {
                warehouseStocks = result.GroupBy(_ => _.ProductCode).Select(_ => new WarehouseStock
                {
                    Pid = _.Key,
                    AvailableStockQuantity = _.Sum(t => t.AvailableNum)
                }).ToList();
            }

            return warehouseStocks;
        }
    }
}
