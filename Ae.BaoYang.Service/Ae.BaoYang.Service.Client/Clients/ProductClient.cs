using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Redis;
using ApolloErp.Web.WebApi;
using Ae.BaoYang.Service.Client.Model;
using Ae.BaoYang.Service.Client.Request;
using Ae.BaoYang.Service.Client.Response;
using Ae.BaoYang.Service.Common.Exceptions;
using Ae.BaoYang.Service.Common.Helper;

namespace Ae.BaoYang.Service.Client.Clients
{
    public class ProductClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ApolloErpLogger<ProductClient> _logger;
        private readonly string redisKey = "Ae:BaoYang:Service:ProductClient";
        private readonly RedisClient _redisClient;

        public ProductClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ApolloErpLogger<ProductClient> logger, RedisClient redisClient)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
            _redisClient = redisClient;
        }

        /// <summary>
        /// 套餐查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<List<PackageProductsByCodesResponse>> GetPackageProductsByCodesAsync(
            PackageProductsByCodesRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<List<PackageProductsByCodesResponse>> result =
                await client
                    .PostAsJsonAsync<PackageProductsByCodesRequest, ApiResult<List<PackageProductsByCodesResponse>>>(
                        _configuration["ProductServer:GetPackageProductsByCodes"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<PackageProductsByCodesResponse>();
            }
            else
            {
                _logger.Error($"GetPackageProductsByCodes_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 套餐查询
        /// </summary>
        /// <param name="packageList"></param>
        /// <returns></returns>
        public async Task<List<PackageProductsByCodesResponse>> GetPackageProductsByCodesAsync(List<string> packageList)
        {
            if (packageList == null || !packageList.Any())
            {
                return new List<PackageProductsByCodesResponse>();
            }

            List<PackageProductsByCodesResponse> result = new List<PackageProductsByCodesResponse>();
            var pageResult = PageHandler(packageList, 100);
            var allTask = await Task.WhenAll(pageResult.Select(_ => GetPackageProductsByCodesAsync(
                new PackageProductsByCodesRequest
                {
                    ProductCodes = _
                })));

            foreach (var itemTask in allTask)
            {
                result.AddRange(itemTask);
            }

            return result;
        }

        /// <summary>
        /// 根据pid查询商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<List<ProductDetailDTO>> GetProductsByProductCodes(ProductsByProductCodesRequest request,
            bool isOnsale)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<List<ProductDetailDTO>> result =
                await client
                    .PostAsJsonAsync<ProductsByProductCodesRequest, ApiResult<List<ProductDetailDTO>>>(
                        _configuration["ProductServer:GetProductsByProductCodes"], request);
            if (result.Code == ResultCode.Success)
            {
                if (isOnsale)
                {
                    return result.Data?.Where(_ => _.Product.OnSale == 1)?.ToList() ?? new List<ProductDetailDTO>();
                }
                else
                {
                    return result.Data?.ToList() ?? new List<ProductDetailDTO>();
                }
            }
            else
            {
                _logger.Error($"GetProductsByProductCodes_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 根据pid查询商品信息
        /// </summary>
        /// <param name="pidList"></param>
        /// <param name="isOnsale">是否只查上架商品</param>
        /// <returns></returns>
        public async Task<List<ProductDetailDTO>> GetProductsByProductCodes(List<string> pidList, bool isOnsale = true)
        {
            if (pidList == null || !pidList.Any())
            {
                return new List<ProductDetailDTO>();
            }

            pidList = pidList.Distinct().ToList();

            List<ProductDetailDTO> result = new List<ProductDetailDTO>();
            var pageResult = PageHandler(pidList, 100);
            var allTask = await Task.WhenAll(pageResult.Select(_ => GetProductsByProductCodes(
                new ProductsByProductCodesRequest
                {
                    ProductCodes = _
                }, isOnsale)));

            foreach (var itemTask in allTask)
            {
                result.AddRange(itemTask);
            }

            return result;
        }

        private List<List<string>> PageHandler(List<string> pidList, int pageSize)
        {
            List<List<string>> result = new List<List<string>>();
            int totalSize = pidList.Count / pageSize;
            int t = pidList.Count % pageSize;
            if (t > 0)
            {
                totalSize = totalSize + 1;
            }

            for (int i = 0; i < totalSize; i++)
            {
                var item = pidList.Skip(i * pageSize).Take(pageSize).ToList();
                result.Add(item);
            }

            return result;
        }

        /// <summary>
        /// 根据parNos查询商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ProductBaseInfoDTO>> SelectProductsByPartNos(SelectProductsByPartNosRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<List<ProductBaseInfoDTO>> result =
                await client
                    .PostAsJsonAsync<SelectProductsByPartNosRequest, ApiResult<List<ProductBaseInfoDTO>>>(
                        _configuration["ProductServer:SelectProductsByPartNos"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<ProductBaseInfoDTO>();
            }
            else
            {
                _logger.Error($"SelectProductsByPartNos_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        #region 根据类目查商品

        /// <summary>
        /// 根据category查询商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<ApiPagedResultData<ProductBaseInfoDTO>> SelectProductsByCategory(
            ProductsByCategoryClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiPagedResult<ProductBaseInfoDTO> result =
                await client
                    .GetAsJsonAsync<ProductsByCategoryClientRequest, ApiPagedResult<ProductBaseInfoDTO>>(
                        _configuration["ProductServer:SelectProductsByCategory"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new ApiPagedResultData<ProductBaseInfoDTO>();
            }
            else
            {
                _logger.Error($"SelectProductsByCategory_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 根据category查询商品 Cache
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task<List<ProductBaseInfoDTO>> SelectProductsByCategoryCache(int category)
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd");
            var key = redisKey + $":ProductsByCategory:{category}:{timestamp}";
            var result = await RedisHelper.GetOrSetAsync(_redisClient, key, () => SelectProductsByCategory(category),
                new TimeSpan(0, 0, 10, 0));
            return result;
        }

        /// <summary>
        /// 过滤数字产品
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        private async Task<List<ProductBaseInfoDTO>> SelectProductsByCategory(int categoryId)
        {
            List<int> expectCategoryId = new List<int>()
            {
                52 //1L机油 是数字产品  后续可能更改掉
            };
            int pageSize = 100;
            List<ProductBaseInfoDTO> result = new List<ProductBaseInfoDTO>();
            var productResultData = await SelectProductsByCategory(new ProductsByCategoryClientRequest()
            {
                CategoryId = categoryId,
                HasAttribute = true,
                HasInstallService = true,
                IsOnSale = 1,
                PageIndex = 1,
                PageSize = pageSize
            });
            if (productResultData != null)
            {
                var product1 = productResultData.Items?.ToList() ?? new List<ProductBaseInfoDTO>();
                var totalCount = productResultData.TotalItems;
                if (expectCategoryId.Contains(categoryId))
                {
                    result.AddRange(product1);
                }
                else
                {
                    result.AddRange(product1.Where(_ => _.ProductAttribute != 3));
                }

                int totalPage = (int) Math.Ceiling((decimal) totalCount / pageSize);
                if (totalPage > 1)
                {
                    List<int> pageList = new List<int>();
                    for (int i = 1; i < totalPage; i++)
                    {
                        pageList.Add(i + 1);
                    }

                    var allTask = await Task.WhenAll(pageList.Select(_ => SelectProductsByCategory(
                        new ProductsByCategoryClientRequest
                        {
                            CategoryId = categoryId,
                            HasAttribute = true,
                            HasInstallService = true,
                            IsOnSale = 1,
                            PageIndex = _,
                            PageSize = pageSize

                        })));
                    foreach (var itemTask in allTask)
                    {
                        if (itemTask.Items != null && itemTask.Items.Any())
                        {
                            if (expectCategoryId.Contains(categoryId))
                            {
                                result.AddRange(itemTask.Items);
                            }
                            else
                            {
                                result.AddRange(itemTask.Items.Where(_ => _.ProductAttribute != 3));
                            }
                        }
                    }
                }
            }

            return result;
        }

        #endregion

        /// <summary>
        /// 获取产品对应安装服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<InstallServiceByProductClientResponse> GetInstallServiceByProduct(
            InstallServiceByProductClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<InstallServiceByProductClientResponse> result =
                await client
                    .PostAsJsonAsync<InstallServiceByProductClientRequest,
                        ApiResult<InstallServiceByProductClientResponse>>(
                        _configuration["ProductServer:GetInstallServiceByProduct"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"GetInstallServiceByProduct_Error {msg}");
                throw new CustomException(msg);
            }
        }
    }
}
