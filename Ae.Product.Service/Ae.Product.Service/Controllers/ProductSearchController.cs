using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Product.Service.Core.Interfaces;
using Ae.Product.Service.Core.Model;
using Ae.Product.Service.Core.Model.Category;
using Ae.Product.Service.Core.Model.Config;
using Ae.Product.Service.Core.Request;
using Ae.Product.Service.Core.Request.Product;
using Ae.Product.Service.Core.Response;
using Ae.Product.Service.Filters;

namespace Ae.Product.Service.Controllers
{
    // <summary>
    /// 商品查询相关接口
    /// </summary>
    [Route("[controller]/[action]")]
    // [Filter(nameof(ProductSearchController))]
    public class ProductSearchController : ControllerBase
    {

        private readonly IProductManageService _productManageService;

        public ProductSearchController(IProductManageService productManageService)
        {
            _productManageService = productManageService;
        }

        /// <summary>
        /// 搜索商品
        /// </summary>
        /// <param name="request">搜索关键字</param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<ProductSearchResponse> Search([FromBody] ProductSearchRequest request)
        {
            var result = _productManageService.SearchProduct(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 产品搜索-子产品搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<ProductBaseInfoVo>> SearchProductCommon(
            [FromBody] SearchProductCommonRequest request)
        {
            var result = await _productManageService.SearchProductCommon(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 搜索产品-子产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<BaoYangPackageProductModel>> SearchProduct([FromQuery] SearchProductRequest request)
        {
            var result = await _productManageService.SearchProduct(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 获取产品属性
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<ProductAttributeVo>>> GetProductAttribute(
            [FromBody] GetProductAttributeRequest request)
        {
            var result = await _productManageService.GetProductAttribute(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 获取产品类目分级展示
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<ProductCategoryVo>>> GetProductCategory(
            [FromBody] GetProductCategoryRequest request)
        {
            var result = await _productManageService.GetProductCategory(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 查询商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<List<ProductDetailVo>> GetProductsByProductCodes([FromBody] ProductDetailSearchRequest request)
        {
            var result = _productManageService.GetProductsByProductCode(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 根据类目查询商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<ProductSearchInfoVo>> SelectProductsByCategory(
            [FromQuery] CategoryProductRequest request)
        {
            var result = await _productManageService.SelectProductsByCategory(request);
            return Result.Success(result.Items, result.TotalItems);
        }


        /// <summary>
        ///  查询类目息 by 类目 和 类目 level 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<List<CategoryInfoVo>> GetCategorysById(int? categoryId, int? level)
        {
            var result = _productManageService.GetCategorysById(categoryId, level);
            return Result.Success(result);
        }

        /// <summary>
        /// 查询套餐商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<List<ProductPackageVo>> GetPackageProductsByCodes(
            [FromBody] PackageProductSearchRequest request)
        {
            var result = _productManageService.GetPackageProductsByCode(request.ProductCodes);
            return Result.Success(result);
        }

        /// <summary>
        /// 查询关联商品信息
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<AssociateProductVo> GetAssociateProductsByCode(string productCode)
        {
            var result = _productManageService.GetAssociateProductsByCodes(productCode);
            return Result.Success(result);
        }

        /// <summary>
        /// 查询单位信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<List<UnitVo>> GetUnitList()
        {
            var result = _productManageService.GetUnits();
            return Result.Success(result);
        }

        /// <summary>
        /// 根据 partNo 查询商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<List<ProductSearchInfoVo>> SelectProductsByPartNos([FromBody] ProductPartRequest request)
        {
            var result = _productManageService.SelectProductsByPartNos(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取商品品牌
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<BrandVO>>> GetBrandList()
        {
            var result = await _productManageService.GetCatalogBrandAsync();
            return Result.Success(result);
        }

        /// <summary>
        ///  根据类目Id获取属性信息
        /// </summary>
        /// <param name="categoryId">三级类目Id</param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<List<CategoryAttributeVo>> GetAttributesByCategoryId(string categoryId)
        {
            var result = _productManageService.GetAttributesByCategoryId(categoryId);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取所有的属性信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<List<AttributeNameVo>> SelectAttributeNames([FromBody] AttributeNameSearchRequest request)
        {
            var result = _productManageService.SelectAttributeNames(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 分页查询商品属性信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiPagedResult<AttributeNameVo> SearchAttribute([FromQuery] AttributeSearchRequest request)
        {
            var result = _productManageService.SearchAttribute(request);
            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        ///  分页查询商品品牌信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<GetProductBrandVo>> GetProductBrandList(
            [FromQuery] GetProductBrandListRequest request)
        {
            var result = await _productManageService.GetProductBrandList(request);
            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        ///   分页查询商品单位信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<GetProductUnitListVo>> GetProductUnitList(
            [FromQuery] GetProductUnitListRequest request)
        {
            var result = await _productManageService.GetProductUnitList(request);
            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 获取单个属性信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<AttributeResponse> GetAttributeById(int attributeNameId)
        {
            var result = _productManageService.GetAttributeById(attributeNameId);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取类目属性信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<List<CategoryAttributeVo>> SelectCategoryAttribute(
            [FromQuery] CategoryAttributeRequest request)
        {
            var result = _productManageService.SelectCategoryAttribute(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 按服务大类取商品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<ProductListByServiceTypeResponse>> GetProductListByServiceType(
            [FromBody] ProductListByServiceTypeRequest request)
        {
            var result = await _productManageService.GetProductListByServiceType(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 商品详情页 （自营商品、门店服务项目）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ProductDetailPageDataResponse>> GetProductDetailPageData(
            [FromQuery] ProductDetailPageDataRequest request)
        {
            var result = await _productManageService.GetProductDetailPageData(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 获取上门服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<ProductBaseInfoVo>> GetShangMenService([FromBody] ShangMenServiceRequest request)
        {
            var result = await _productManageService.GetShangMenService(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 判断是否上门产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<CheckDoorProductVo>>> CheckDoorProduct(
            [FromBody] CheckDoorProductRequest request)
        {
            var result = await _productManageService.CheckDoorProduct(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 获取上门服务费
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<ProductBaseInfoVo>> GetDoorService([FromBody] DoorServiceRequest request)
        {
            var result = await _productManageService.GetDoorService(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 获取批发商品列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<ProductBaseInfoVo>> GetWholesaleProducts(
            [FromQuery] WholesaleProductsRequest request)
        {
            var result = await _productManageService.GetWholesaleProducts(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 搜索替换商品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<BaoYangPackageProductModel>> SearchReplaceProduct(
            [FromQuery] SearchReplaceProductRequest request)
        {
            var result = await _productManageService.SearchReplaceProduct(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 搜索商品通用：平台产品or门店外采
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<BaoYangPackageProductModel>> SearchShopProductCommon(
            [FromBody] SearchShopProductCommon request)
        {
            var result = await _productManageService.SearchShopProductCommon(request);

            return Result.Success(result.Items, result.TotalItems);
        }
    }
}