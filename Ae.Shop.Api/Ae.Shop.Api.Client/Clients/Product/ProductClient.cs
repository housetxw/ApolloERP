using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Client.Model.Product;
using Ae.Shop.Api.Client.Request;
using Ae.Shop.Api.Client.Request.Product;
using Ae.Shop.Api.Client.Response.Product;
using Ae.Shop.Api.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Client.Clients.Product
{
    public class ProductClient : IProductClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ApolloErpLogger<ProductClient> _logger;
        public ProductClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, ApolloErpLogger<ProductClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<List<ProductDetailClientVo>> GetProductsByProductCodes(ProductDetailSearchClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<List<ProductDetailClientVo>> result =
                 await client.PostAsJsonAsync<ProductDetailSearchClientRequest, ApiResult<List<ProductDetailClientVo>>>(
                    _configuration["ProductServer:GetProductsByProductCodes"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<ProductDetailClientVo>();
            }
            else
            {
                _logger.Info($"SearchProduct_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 搜索商品
        /// </summary>
        /// <param name="request">搜索关键字</param>
        /// <returns></returns>
        public async Task<ProductSearchClientResponse> SearchProduct(ProductSearchClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<ProductSearchClientResponse> result =
                 await client.PostAsJsonAsync<ProductSearchClientRequest, ApiResult<ProductSearchClientResponse>>(
                    _configuration["ProductServer:Search"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new ProductSearchClientResponse();
            }
            else
            {
                _logger.Info($"SearchProduct_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        ///  分页查询门店商品信息
        /// </summary>
        /// <returns></returns>
        public async Task<ApiPagedResult<ShopProductDto>> SearchShopProduct(ShopProductSearchClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiPagedResult<ShopProductDto> result =
                await client.PostAsJsonAsync<ShopProductSearchClientRequest, ApiPagedResult<ShopProductDto>>(
                    _configuration["ProductServer:SearchShopProduct"], request);
            if (result.IsNotNullSuccess())
            {
                return result ?? new ApiPagedResult<ShopProductDto>();
            }
            else
            {
                _logger.Info($"SearchShopProduct_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 获取产品类目分级展示
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProductCategoryDto>> GetProductCategory(ProductCategoryClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<List<ProductCategoryDto>> result =
                await client.PostAsJsonAsync<ProductCategoryClientRequest, ApiResult<List<ProductCategoryDto>>>(
                    _configuration["ProductServer:GetProductCategory"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<ProductCategoryDto>();
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"GetProductCategory_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 查询商品品牌
        /// </summary>
        /// <returns></returns>
        public async Task<List<BrandDto>> GetBrandList()
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<List<BrandDto>> result =
                await client.GetAsJsonAsync<ApiResult<List<BrandDto>>>(
                    _configuration["ProductServer:GetBrandList"], null);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<BrandDto>();
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"GetBrandList_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 查询类目息 by 类目 和 类目 level 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<List<CategoryInfoDto>> GetCategorysById(int? categoryId, int? level)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<List<CategoryInfoDto>> result =
                await client.GetAsJsonAsync<Object, ApiResult<List<CategoryInfoDto>>>(
                    _configuration["ProductServer:GetCategorysById"], new {categoryId = categoryId, level = level});
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<CategoryInfoDto>();
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"GetCategorysById_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 查询单位信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<UnitDto>> GetUnitList()
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<List<UnitDto>> result =
                await client.GetAsJsonAsync<ApiResult<List<UnitDto>>>(_configuration["ProductServer:GetUnitList"],
                    null);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<UnitDto>();
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"GetUnitList_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 编辑商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> SaveProduct(ProductEditClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<ProductEditClientRequest, ApiResult<bool>>(
                    _configuration["ProductServer:SaveProduct"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"SaveProduct_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 查询套餐商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ProductPackageDto>> GetPackageProductsByCodes(ProductDetailSearchClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<List<ProductPackageDto>> result =
                await client.PostAsJsonAsync<ProductDetailSearchClientRequest, ApiResult<List<ProductPackageDto>>>(
                    _configuration["ProductServer:GetPackageProductsByCodes"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<ProductPackageDto>();
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"GetPackageProductsByCodes_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        ///  根据类目Id获取属性信息
        /// </summary>
        /// <param name="categoryId">三级类目Id</param>
        /// <returns></returns>
        public async Task<List<CategoryAttributeDto>> GetAttributesByCategoryId(string categoryId)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<List<CategoryAttributeDto>> result =
                await client.GetAsJsonAsync<Object, ApiResult<List<CategoryAttributeDto>>>(
                    _configuration["ProductServer:GetAttributesByCategoryId"], new { categoryId = categoryId });
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<CategoryAttributeDto>();
            }
            else
            {
                _logger.Info($"GetAttributesByCategoryId_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }
    }
}
