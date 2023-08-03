using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Clients.Interface;
using Ae.C.MiniApp.Api.Client.Model.Product;
using Ae.C.MiniApp.Api.Client.Model.ShopProduct;
using Ae.C.MiniApp.Api.Client.Request.Product;
using Ae.C.MiniApp.Api.Client.Request.ShopProduct;
using Ae.C.MiniApp.Api.Client.Response.Product;
using Ae.C.MiniApp.Api.Common.Exceptions;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Client.Clients.Product
{
    public class ProductClient : IProductClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ApolloErpLogger<ProductClient> _logger;
        private readonly IConfiguration _configuration;


        public ProductClient(IHttpClientFactory clientFactory, IConfiguration configuration,
            ApolloErpLogger<ProductClient> logger)
        {
            this._clientFactory = clientFactory;
            this._configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// 商品搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ProductSearchClientResponse> ProductSearch(ProductSearchClientRequest request)
        {
            var client = _clientFactory.CreateClient("ProductServer");
            ApiResult<ProductSearchClientResponse> result = await client.PostAsJsonAsync<ProductSearchClientRequest, ApiResult<ProductSearchClientResponse>>(
                _configuration["ProductServer:Search"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new ProductSearchClientResponse();
            }
            else
            {
                _logger.Error($"ProductSearch_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 查询商品详情
        /// </summary>
        /// <param name="productCodes"></param>
        /// <returns></returns>
        public async Task<List<ProductDetailClientResponse>> ProductDetail(ProductDetailSearchClientRequest request)
        {
            var client = _clientFactory.CreateClient("ProductServer");
            ApiResult<List<ProductDetailClientResponse>> result = await client.PostAsJsonAsync<ProductDetailSearchClientRequest, ApiResult<List<ProductDetailClientResponse>>>(
                _configuration["ProductServer:GetProductDetail"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<ProductDetailClientResponse>();
            }
            else
            {
                _logger.Error($"GetProductDetail_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }


        /// <summary>
        /// 查询关联商品信息
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public async Task<AssociateProductDto> GetAssociateProduct(string productCode)
        {
            var client = _clientFactory.CreateClient("ProductServer");
            ApiResult<AssociateProductDto> result = await client.GetAsJsonAsync<object, ApiResult<AssociateProductDto>>(
                _configuration["ProductServer:GetAssociateProductsByCode"], new { productCode = productCode });
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new AssociateProductDto();
            }
            else
            {
                _logger.Error($"GetAssociateProduct_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 根据类目查询商品信息
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public async Task<List<ProductBaseInfoDto>> SelectProductsByCategory(CategoryProductClientRequest request)
        {
            var client = _clientFactory.CreateClient("ProductServer");
            ApiPagedResult<ProductBaseInfoDto> result = await client.GetAsJsonAsync<CategoryProductClientRequest, ApiPagedResult<ProductBaseInfoDto>>(
                _configuration["ProductServer:SelectProductsByCategory"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data.Items?.ToList() ?? new List<ProductBaseInfoDto>();
            }
            else
            {
                _logger.Warn($"GetAssociateProduct_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }


        /// <summary>
        /// 查询门店商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ShopProductDto>> SelectShopProduct(ShopProductQueryClientRequest request)
        {
            var client = _clientFactory.CreateClient("ProductServer");
            ApiResult<List<ShopProductDto>> result = await client.PostAsJsonAsync<ShopProductQueryClientRequest, ApiResult<List<ShopProductDto>>>(
                _configuration["ProductServer:SelectShopProduct"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data?.ToList() ?? new List<ShopProductDto>();
            }
            else
            {
                _logger.Error($"SelectShopProduct_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 获取门店上架的服务项目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ShopProductDetailDto>> GetOnSaleShopProduct(OnSaleShopProductClientRequest request)
        {
            var client = _clientFactory.CreateClient("ProductServer");
            ApiResult<List<ShopProductDetailDto>> result =
                await client.GetAsJsonAsync<OnSaleShopProductClientRequest, ApiResult<List<ShopProductDetailDto>>>(
                    _configuration["ProductServer:GetOnSaleShopProduct"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<ShopProductDetailDto>();
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"GetOnSaleShopProduct_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 服务大类 商品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ProductListByServiceTypeClientResponse> GetProductListByServiceType(
            ProductListByServiceTypeClientRequest request)
        {
            var client = _clientFactory.CreateClient("ProductServer");
            ApiResult<ProductListByServiceTypeClientResponse> result =
                await client
                    .PostAsJsonAsync<ProductListByServiceTypeClientRequest,
                        ApiResult<ProductListByServiceTypeClientResponse>>(
                        _configuration["ProductServer:GetProductListByServiceType"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"GetProductListByServiceType_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 商品详情页 （自营商品、门店服务项目）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ProductDetailPageDataClientResponse> GetProductDetailPageData(
            ProductDetailPageDataClientRequest request)
        {
            var client = _clientFactory.CreateClient("ProductServer");
            ApiResult<ProductDetailPageDataClientResponse> result =
                await client
                    .GetAsJsonAsync<ProductDetailPageDataClientRequest,
                        ApiResult<ProductDetailPageDataClientResponse>>(
                        _configuration["ProductServer:GetProductDetailPageData"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"GetProductDetailPageData_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 分页获取热门商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ConfigHotProductDto>> GetHotProductPageList(
            HotProductPageListClientRequest request)
        {
            var client = _clientFactory.CreateClient("ProductServer");
            ApiPagedResult<ConfigHotProductDto> result =
                await client.GetAsJsonAsync<HotProductPageListClientRequest, ApiPagedResult<ConfigHotProductDto>>(
                    _configuration["ProductServer:GetHotProductPageList"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"GetHotProductPageList_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 套餐卡查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<PackageCardProductDto>> GetPackageCardProductPageList(
            PackageCardProductPageListClientRequest request)
        {
            var client = _clientFactory.CreateClient("ProductServer");
            ApiPagedResult<PackageCardProductDto> result =
                await client
                    .PostAsJsonAsync<PackageCardProductPageListClientRequest, ApiPagedResult<PackageCardProductDto>>(
                        _configuration["ProductServer:GetPackageCardProductPageList"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"GetPackageCardProductPageList_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 首页活动配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ConfigAdvertisementDto>> QueryConfigAdvertisement(
            QueryConfigAdvertisementClientRequest request)
        {
            var client = _clientFactory.CreateClient("ProductServer");
            ApiResult<List<ConfigAdvertisementDto>> result =
                await client
                    .GetAsJsonAsync<QueryConfigAdvertisementClientRequest, ApiResult<List<ConfigAdvertisementDto>>>(
                        _configuration["ProductServer:QueryConfigAdvertisement"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<ConfigAdvertisementDto>();
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"QueryConfigAdvertisement_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 获取前台一级类目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ConfigFrontCategoryDto>> GetMainFrontCategory(MainFrontCategoryClientRequest request)
        {
            var client = _clientFactory.CreateClient("ProductServer");
            ApiResult<List<ConfigFrontCategoryDto>> result =
                await client
                    .GetAsJsonAsync<MainFrontCategoryClientRequest, ApiResult<List<ConfigFrontCategoryDto>>>(
                        _configuration["ProductServer:GetMainFrontCategory"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<ConfigFrontCategoryDto>();
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"GetMainFrontCategory_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 根据产品查秒杀信息
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public async Task<FlashSaleProductDto> GetFlashSaleProduct(string productCode)
        {
            var client = _clientFactory.CreateClient("ProductServer");
            ApiResult<FlashSaleProductDto> result =
                await client.GetAsJsonAsync<FlashSaleProductDto, ApiResult<FlashSaleProductDto>>(
                    _configuration["ProductServer:GetFlashSaleProduct"],
                    new FlashSaleProductDto() {ProductId = productCode});
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"GetFlashSaleProduct_Error {msg}");
                throw new CustomException(msg);
            }
        }

        public async Task<ApiResult<List<FlashSaleConfigVo>>> GetActiveFlashSaleConfigs(GetActiveFlashSaleConfigsRequest request)
        {
            var client = _clientFactory.CreateClient("ProductServer");
            ApiResult<List<FlashSaleConfigVo>> result =
                await client
                    .GetAsJsonAsync<GetActiveFlashSaleConfigsRequest, ApiResult<List<FlashSaleConfigVo>>>(
                        _configuration["ProductServer:GetActiveFlashSaleConfigs"], request);
            return result;
        }
    }
}
