using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Client.Interfaces;
using Ae.B.Product.Api.Client.Model;
using Ae.B.Product.Api.Client.Request;
using Ae.B.Product.Api.Client.Response;
using Ae.B.Product.Api.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Ae.B.Product.Api.Core.Model;
using Ae.B.Product.Api.Core.Request;
using Ae.B.Product.Api.Client.Model.ShopProduc;
using Ae.B.Product.Api.Client.Request.ShopProduct;
using Ae.B.Product.Api.Client.Request.Product;
using Ae.B.Product.Api.Client.Model.Product;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Ae.B.Product.Api.Client.Clients
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
                throw new CustomException(result.Message);
            }
        }
        /// <summary>
        ///   查询类目息 by 类目 和 类目 level 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<List<CategoryInfoDto>> GetCategorysById(int? categoryId, int? level)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<List<CategoryInfoDto>> result =
                 await client.GetAsJsonAsync<Object, ApiResult<List<CategoryInfoDto>>>(
                    _configuration["ProductServer:GetCategorysById"], new { categoryId = categoryId, level = level });
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<CategoryInfoDto>();
            }
            else
            {
                _logger.Info($"GetCategorysById_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 查询商品详情信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ProductDetailDto>> GetProductsByProductCodes(ProductDetailSearchClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<List<ProductDetailDto>> result =
                 await client.PostAsJsonAsync<ProductDetailSearchClientRequest, ApiResult<List<ProductDetailDto>>>(
                    _configuration["ProductServer:GetProductDetail"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<ProductDetailDto>();
            }
            else
            {
                _logger.Info($"GetProductsByProductCodes_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 查询单位信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<UnitDto>> GetUnits()
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<List<UnitDto>> result =
                 await client.GetAsJsonAsync<ApiResult<List<UnitDto>>>(
                    _configuration["ProductServer:GetUnitList"], null);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<UnitDto>();
            }
            else
            {
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

        /// <summary>
        ///  编辑商品
        /// </summary>
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
                _logger.Info($"SaveProduct_Error {result.Message}");
                throw new CustomException(result.Message);
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
                _logger.Info($"GetPackageProductsByCodes_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }


        /// <summary>
        ///  批量导入商品
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ImportProduct(ImportProductClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<bool> result =
                 await client.PostAsJsonAsync<ImportProductClientRequest, ApiResult<bool>>(
                    _configuration["ProductServer:ImportProduct"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"ImportProduct_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        ///  批量导入商品属性
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ImportProductAttribute(ImportProductAttributeClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<bool> result =
                 await client.PostAsJsonAsync<ImportProductAttributeClientRequest, ApiResult<bool>>(
                    _configuration["ProductServer:ImportProductAttribute"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"ImportProductAttribute_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        ///  获取所有的属性信息
        /// </summary>
        /// <param name="categoryId">三级类目Id</param>
        /// <returns></returns>
        public async Task<List<AttributeNameDto>> SelectAttributeNames(AttributeNameSearchClientRequest clientRequest)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<List<AttributeNameDto>> result =
                 await client.PostAsJsonAsync<AttributeNameSearchClientRequest, ApiResult<List<AttributeNameDto>>>(
                    _configuration["ProductServer:SelectAttributeNames"], clientRequest);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<AttributeNameDto>();
            }
            else
            {
                _logger.Info($"SelectAttributeNames_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 根据 partNo 查询商品信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProductBaseInfoDto>> SelectProductsByPartNos(ProductPartClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<List<ProductBaseInfoDto>> result =
                 await client.PostAsJsonAsync<ProductPartClientRequest, ApiResult<List<ProductBaseInfoDto>>>(
                    _configuration["ProductServer:SelectProductsByPartNos"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<ProductBaseInfoDto>();
            }
            else
            {
                _logger.Info($"SelectProductsByPartNos_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        ///  分页查询商品属性信息
        /// </summary>
        /// <param name="categoryId">三级类目Id</param>
        /// <returns></returns>
        public async Task<ApiPagedResult<AttributeNameDto>> SearchAttribute(AttributeSearchClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiPagedResult<AttributeNameDto> result =
                 await client.GetAsJsonAsync<AttributeSearchClientRequest, ApiPagedResult<AttributeNameDto>>(
                    _configuration["ProductServer:SearchAttribute"], request);
            if (result.IsNotNullSuccess())
            {
                return result ?? new ApiPagedResult<AttributeNameDto>();
            }
            else
            {
                _logger.Info($"SearchAttribute_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        ///  编辑属性信息
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SaveAttribute(AttributeEditClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<bool> result =
                 await client.PostAsJsonAsync<AttributeEditClientRequest, ApiResult<bool>>(
                    _configuration["ProductServer:SaveAttribute"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"SaveAttribute_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        ///  获取单个属性信息
        /// </summary>
        /// <returns></returns>
        public async Task<AttributeClientResponse> GetAttributeById(int attributeNameId)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<AttributeClientResponse> result =
                 await client.GetAsJsonAsync<object, ApiResult<AttributeClientResponse>>(
                    _configuration["ProductServer:GetAttributeById"], new { attributeNameId = attributeNameId });
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"GetAttributeById_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 分页查询商品品牌信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<GetProductBrandVo>> GetProductBrandList(GetProductBrandListRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");

            return await client.GetAsJsonAsync<GetProductBrandListRequest, ApiPagedResult<GetProductBrandVo>>(
                  _configuration["ProductServer:GetProductBrandList"], request);

        }

        /// <summary>
        ///   分页查询商品单位信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<GetProductUnitListVo>> GetProductUnitList(
           GetProductUnitListRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");

            return await client.GetAsJsonAsync<GetProductUnitListRequest, ApiPagedResult<GetProductUnitListVo>>(
                _configuration["ProductServer:GetProductUnitList"], request);
        }

        /// <summary>
        /// 添加品牌
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> AddBrand(AddBrandRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");

            return await client.PostAsJsonAsync<AddBrandRequest, ApiResult>(
                _configuration["ProductServer:AddBrand"], request);
        }

        /// <summary>
        /// 编辑品牌
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> EditBrand(AddBrandRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");

            return await client.PostAsJsonAsync<AddBrandRequest, ApiResult>(
                _configuration["ProductServer:EditBrand"], request);
        }

        /// <summary>
        /// 添加单位
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> AddUnit(AddUnitRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");

            return await client.PostAsJsonAsync<AddUnitRequest, ApiResult>(
                _configuration["ProductServer:AddUnit"], request);
        }

        /// <summary>
        /// 编辑单位
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> EditUnit(AddUnitRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");

            return await client.PostAsJsonAsync<AddUnitRequest, ApiResult>(
                _configuration["ProductServer:EditUnit"], request);
        }

        public async Task<ApiResult<List<ListCategoryDto>>> ListCategory(GetCategoryListRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            var result = await client.GetAsJsonAsync<GetCategoryListRequest, ApiResult<List<ListCategoryDto>>>(_configuration["ProductServer:ListCategory"], request);
            return result;
        }

        public async Task<ApiResult<DimCategoryVo>> GetCategory(int id)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            var result = await client.GetAsJsonAsync<object, ApiResult<DimCategoryVo>>(_configuration["ProductServer:GetCategory"], new { id });
            return result;
        }

        public async Task<ApiResult<List<CategoryTreeSelectVo>>> CategoryTreeSelect(GetCategoryListRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            var result = await client.GetAsJsonAsync<GetCategoryListRequest,ApiResult<List<CategoryTreeSelectVo>>>(_configuration["ProductServer:CategoryTreeSelect"], request);
            return result;
        }

        public async Task<ApiResult> UpdateCategory(DimCategoryVo vo)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            var result = await client.PostAsJsonAsync<DimCategoryVo, ApiResult>(_configuration["ProductServer:UpdateCategory"], vo);
            return result;
        }

        public async Task<ApiResult> AddCategory(DimCategoryVo vo)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            var result = await client.PostAsJsonAsync<DimCategoryVo, ApiResult>(_configuration["ProductServer:AddCategory"], vo);
            return result;
        }

        public async Task<ApiResult> DeleteCategory(DimCategoryVo vo)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            var result = await client.PostAsJsonAsync<object, ApiResult>(_configuration["ProductServer:DeleteCategory"], vo);
            return result;
        }

        /// <summary>
        ///  获取类目属性信息
        /// </summary>
        /// <param name="categoryId">三级类目Id</param>
        /// <returns></returns>
        public async Task<List<CategoryAttributeDto>> SelectCategoryAttribute(CategoryAttributeClientRequest clientRequest)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<List<CategoryAttributeDto>> result =
                 await client.GetAsJsonAsync<CategoryAttributeClientRequest, ApiResult<List<CategoryAttributeDto>>>(
                    _configuration["ProductServer:SelectCategoryAttribute"], clientRequest);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<CategoryAttributeDto>();
            }
            else
            {
                _logger.Info($"SelectCategoryAttribute_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        ///  编辑类目属性
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SaveCategoryAttribute(CategoryAttributeEditClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<bool> result =
                 await client.PostAsJsonAsync<CategoryAttributeEditClientRequest, ApiResult<bool>>(
                    _configuration["ProductServer:SaveCategoryAttribute"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"ImportProductAttribute_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 查询单个门店商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ShopProductDto> GetShopProductByCode(string shopProductCode)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<ShopProductDto> result = await client.GetAsJsonAsync<object, ApiResult<ShopProductDto>>(
                _configuration["ProductServer:GetShopProductByCode"], new { shopProductCode = shopProductCode });
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new ShopProductDto();
            }
            else
            {
                _logger.Info($"GetShopProductByCode_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        ///  保存门店商品信息
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SaveShopProduct(ShopProductEditClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<bool> result = await client.PostAsJsonAsync<ShopProductEditClientRequest, ApiResult<bool>>(
                    _configuration["ProductServer:SaveShopProduct"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"SaveShopProduct_Error {result.Message}");
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
            ApiPagedResult<ShopProductDto> result = await client.PostAsJsonAsync<ShopProductSearchClientRequest, ApiPagedResult<ShopProductDto>>(
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
        ///  批量导入门店商品信息
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ImportShopProduct(ImportShopProductClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<bool> result = await client.PostAsJsonAsync<ImportShopProductClientRequest, ApiResult<bool>>(
                    _configuration["ProductServer:ImportShopProduct"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"ImportShopProduct_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }
        /// <summary>
        ///  审核门店商品上架
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AuditShopProduct(ShopProductAuditClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<bool> result = await client.PostAsJsonAsync<ShopProductAuditClientRequest, ApiResult<bool>>(
                    _configuration["ProductServer:AuditShopProduct"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"AuditShopProduct_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 防伪码查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ProductSecurityCodeDto> GetSecurityCode(SecurityCodeClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<ProductSecurityCodeDto> result =
                await client.GetAsJsonAsync<SecurityCodeClientRequest, ApiResult<ProductSecurityCodeDto>>(
                    _configuration["ProductServer:GetSecurityCode"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"GetSecurityCode_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 根据父级类目获取子类目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<DimCategoryVo>> GetCategoryByParentId(CategoryByParentIdClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<List<DimCategoryVo>> result =
                await client.GetAsJsonAsync<CategoryByParentIdClientRequest, ApiResult<List<DimCategoryVo>>>(
                    _configuration["ProductServer:GetCategoryByParentId"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<DimCategoryVo>();
            }
            else
            {
                _logger.Info($"GetCategoryByParentId_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 获取所有品牌
        /// </summary>
        /// <returns></returns>
        public async Task<List<GetProductBrandVo>> GetAllProductBrandList()
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<List<GetProductBrandVo>> result =
                await client.GetAsJsonAsync<ApiResult<List<GetProductBrandVo>>>(
                    _configuration["ProductServer:GetAllProductBrandList"], null);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<GetProductBrandVo>();
            }
            else
            {
                _logger.Info($"GetAllProductBrandList_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 获取上门产品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<DoorProductDto>> GetDoorProductPageList(
            DoorProductPageListClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiPagedResult<DoorProductDto> result =
                await client.GetAsJsonAsync<DoorProductPageListClientRequest, ApiPagedResult<DoorProductDto>>(
                    _configuration["ProductServer:GetDoorProductPageList"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"GetDoorProductPageList_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 编辑上门产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditDoorProduct(EditDoorProductClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<EditDoorProductClientRequest, ApiResult<bool>>(
                    _configuration["ProductServer:EditDoorProduct"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"EditDoorProduct_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 新增上门产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddDoorProducts(AddDoorProductsClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<AddDoorProductsClientRequest, ApiResult<bool>>(
                    _configuration["ProductServer:AddDoorProducts"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"AddDoorProducts_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 查询安装服务列表
        /// </summary>
        public async Task<ApiPagedResultData<ProductInstallServiceDto>> GetProductInstallService(
            ProductInstallServiceClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiPagedResult<ProductInstallServiceDto> result =
                await client
                    .GetAsJsonAsync<ProductInstallServiceClientRequest, ApiPagedResult<ProductInstallServiceDto>>(
                        _configuration["ProductServer:GetProductInstallService"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"GetProductInstallService_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 查询安装服务详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ProductInstallServiceDto> GetProductInstallServiceDetail(
            ProductInstallServiceDetailClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<ProductInstallServiceDto> result =
                await client
                    .GetAsJsonAsync<ProductInstallServiceDetailClientRequest, ApiResult<ProductInstallServiceDto>>(
                        _configuration["ProductServer:GetProductInstallServiceDetail"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"GetProductInstallServiceDetail_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 产品搜索-子产品搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<ApiPagedResultData<ProductBaseInfoDto>> SearchProductCommon(
            SearchProductCommonClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiPagedResult<ProductBaseInfoDto> result =
                await client.PostAsJsonAsync<SearchProductCommonClientRequest, ApiPagedResult<ProductBaseInfoDto>>(
                    _configuration["ProductServer:SearchProductCommon"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"SearchProductCommon_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 搜索安装服务
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<List<ProductBaseInfoDto>> SearchInstallService(string content)
        {
            int pageSize = 100;
            List<ProductBaseInfoDto> result = new List<ProductBaseInfoDto>();
            var productResultData = await SearchProductCommon(new SearchProductCommonClientRequest()
            {
                ProductName = content,
                OnSale = -1,
                ProductAttribute = new List<int>() {2},
                PageIndex = 1,
                PageSize = pageSize
            });
            if (productResultData != null)
            {
                var product1 = productResultData.Items?.ToList() ?? new List<ProductBaseInfoDto>();
                var totalCount = productResultData.TotalItems;
                result.AddRange(product1);
                int totalPage = (int) Math.Ceiling((decimal) totalCount / pageSize);
                if (totalPage > 1)
                {
                    List<int> pageList = new List<int>();
                    for (int i = 1; i < totalPage; i++)
                    {
                        pageList.Add(i + 1);
                    }

                    var allTask = await Task.WhenAll(pageList.Select(_ => SearchProductCommon(
                        new SearchProductCommonClientRequest()
                        {
                            ProductName = content,
                            OnSale = -1,
                            ProductAttribute = new List<int>() {2},
                            PageIndex = _,
                            PageSize = pageSize
                        })));
                    foreach (var itemTask in allTask)
                    {
                        if (itemTask.Items != null && itemTask.Items.Any())
                        {
                            result.AddRange(itemTask.Items);
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 编辑安装服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditInstallService(EditInstallServiceClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<EditInstallServiceClientRequest, ApiResult<bool>>(
                    _configuration["ProductServer:EditInstallService"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"EditInstallService_Error {msg}");
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
            var client = _httpClientFactory.CreateClient("ProductServer");
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
        /// 新增热销产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddHotProduct(AddHotProductClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<AddHotProductClientRequest, ApiResult<bool>>(
                    _configuration["ProductServer:AddHotProduct"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"AddHotProduct_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 编辑热销产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditHotProduct(EditHotProductClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<EditHotProductClientRequest, ApiResult<bool>>(
                    _configuration["ProductServer:EditHotProduct"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"EditHotProduct_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 搜索商品For添加热门商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ConfigHotProductDto>> SearchProductForHot(
            SearchProductForHotClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiPagedResult<ConfigHotProductDto> result =
                await client.GetAsJsonAsync<SearchProductForHotClientRequest, ApiPagedResult<ConfigHotProductDto>>(
                    _configuration["ProductServer:SearchProductForHot"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"SearchProductForHot_Error {msg}");
                throw new CustomException(msg);
            }
        }

        #region 套餐卡

        /// <summary>
        /// 套餐卡查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<PackageCardProductDto>> GetPackageCardProductPageList(
            PackageCardProductPageListClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
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
        /// 搜索商品For添加套餐卡商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<PackageCardProductDto>> SearchProductForPackageCard(
            SearchProductForPackageCardClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiPagedResult<PackageCardProductDto> result =
                await client
                    .GetAsJsonAsync<SearchProductForPackageCardClientRequest, ApiPagedResult<PackageCardProductDto>>(
                        _configuration["ProductServer:SearchProductForPackageCard"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"SearchProductForPackageCard_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 添加套餐卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddPackageCardProduct(AddPackageCardProductClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<AddPackageCardProductClientRequest, ApiResult<bool>>(
                    _configuration["ProductServer:AddPackageCardProduct"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"AddPackageCardProduct_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditPackageCardProduct(EditPackageCardProductClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<EditPackageCardProductClientRequest, ApiResult<bool>>(
                    _configuration["ProductServer:EditPackageCardProduct"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"EditPackageCardProduct_Error {msg}");
                throw new CustomException(msg);
            }
        }

        #endregion

        /// <summary>
        /// 根据category查询商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<ApiPagedResultData<ProductBaseInfoDto>> SelectProductsByCategory(
            ProductsByCategoryClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiPagedResult<ProductBaseInfoDto> result =
                await client
                    .GetAsJsonAsync<ProductsByCategoryClientRequest, ApiPagedResult<ProductBaseInfoDto>>(
                        _configuration["ProductServer:SelectProductsByCategory"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new ApiPagedResultData<ProductBaseInfoDto>();
            }
            else
            {
                _logger.Error($"SelectProductsByCategory_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 根据类目查产品
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<List<ProductBaseInfoDto>> SelectProductsByCategory(int categoryId)
        {
            int pageSize = 100;
            List<ProductBaseInfoDto> result = new List<ProductBaseInfoDto>();
            var productResultData = await SelectProductsByCategory(new ProductsByCategoryClientRequest()
            {
                CategoryId = categoryId,
                HasAttribute = false,
                HasInstallService = false,
                IsOnSale = 1,
                PageIndex = 1,
                PageSize = pageSize
            });
            if (productResultData != null)
            {
                var product1 = productResultData.Items?.ToList() ?? new List<ProductBaseInfoDto>();
                var totalCount = productResultData.TotalItems;
                result.AddRange(product1);

                int totalPage = (int)Math.Ceiling((decimal)totalCount / pageSize);
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
                            HasAttribute = false,
                            HasInstallService = false,
                            IsOnSale = 1,
                            PageIndex = _,
                            PageSize = pageSize
                        })));
                    foreach (var itemTask in allTask)
                    {
                        if (itemTask.Items != null && itemTask.Items.Any())
                        {
                            result.AddRange(itemTask.Items);
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 美容团购商品查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<GrouponProductDto>> GetGrouponProductPageList(
            GrouponProductPageListClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiPagedResult<GrouponProductDto> result =
                await client
                    .GetAsJsonAsync<GrouponProductPageListClientRequest, ApiPagedResult<GrouponProductDto>>(
                        _configuration["ProductServer:GetGrouponProductPageList"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"GetGrouponProductPageList_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 搜索产品For美容团购
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<GrouponProductDto>> SearchProductForGroupon(
            SearchProductForGrouponClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiPagedResult<GrouponProductDto> result =
                await client
                    .GetAsJsonAsync<SearchProductForGrouponClientRequest, ApiPagedResult<GrouponProductDto>>(
                        _configuration["ProductServer:SearchProductForGroupon"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"SearchProductForGroupon_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 新增团购商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddGrouponProduct(AddGrouponProductClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<AddGrouponProductClientRequest, ApiResult<bool>>(
                    _configuration["ProductServer:AddGrouponProduct"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"AddGrouponProduct_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 编辑美容团购商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditGrouponProduct(EditGrouponProductClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<EditGrouponProductClientRequest, ApiResult<bool>>(
                    _configuration["ProductServer:EditGrouponProduct"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"EditGrouponProduct_Error {msg}");
                throw new CustomException(msg);
            }
        }
    }
}
