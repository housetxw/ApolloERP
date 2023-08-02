using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Core.Interfaces;
using Ae.B.Product.Api.Core.Model;
using Ae.B.Product.Api.Core.Model.Product;
using Ae.B.Product.Api.Core.Request;
using Ae.B.Product.Api.Core.Request.Product;
using Ae.B.Product.Api.Core.Response;
using Ae.B.Product.Api.Filters;

namespace Ae.B.Product.Api.Controllers
{
    /// <summary>
    /// 商品配置相关接口
    /// </summary>
    [Route("[controller]/[action]")]
    public class ProductConfigController : ControllerBase
    {
        private readonly IProductManageService _productManageService;

        public ProductConfigController(IProductManageService productManageService)
        {
            _productManageService = productManageService;
        }

        /// <summary>
        /// 搜索商品
        /// </summary>
        /// <param name="request">搜索关键字</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ProductSearchResponse>> Search([FromQuery]ProductSearchRequest request)
        {
            var result = await _productManageService.SearchProduct(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 查询商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ProductDetailVo>> GetProductByProductCode(string productCode)
        {
            var result = await _productManageService.GetProductByProductCode(productCode);
            return Result.Success(result);
        }


        /// <summary>
        ///   查询类目息 by 类目 和 类目 level 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<CategoryInfoVo>>> GetCategorysById(int? categoryId, int? level)
        {
            var result = await _productManageService.GetCategorysById(categoryId, level);
            return Result.Success(result);
        }

        /// <summary>
        /// 查询单位信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<UnitVo>>> GetUnitList()
        {
            var result = await _productManageService.GetUnits();
            return Result.Success(result);
        }

        /// <summary>
        /// 获取商品品牌
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<CatalogBrandVo>>> GetBrandList()
        {
            var result = await _productManageService.GetBrandList();
            return Result.Success(result);
        }

        /// <summary>
        ///  根据类目Id获取属性信息
        /// </summary>
        /// <param name="categoryId">三级类目Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<CategoryAttributeVo>>> GetAttributesByCategoryId(string categoryId)
        {
            var result = await _productManageService.GetAttributesByCategoryId(categoryId);
            return Result.Success(result);
        }


        /// <summary>
        ///  编辑商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> SaveProduct([FromBody]ApiRequest<ProductEditRequest> request)
        {
            var result = await _productManageService.SaveProduct(request);
            return Result.Success<bool>(result);
        }

        /// <summary>
        ///  导入商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]

        public async Task<ApiResult<List<ProductImportVo>>> ImportProduct(IFormFile file)
        {
            var result = await _productManageService.ImportProduct(file);
            return Result.Success(result);
        }

        /// <summary>
        ///  导入商品属性
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<ProductAttributeImportVo>>> ImportProductAttribute(IFormFile file)
        {
            var result = await _productManageService.ImportProductAttribute(file);
            return Result.Success(result);
        }

        ///  分页查询商品品牌信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<GetProductBrandVo>> GetProductBrandList([FromQuery]GetProductBrandListRequest request)
        {
            return await _productManageService.GetProductBrandList(request);

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
            return await _productManageService.GetProductUnitList(request);
        }

        /// <summary>
        /// 添加单位
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> AddUnit([FromBody] ApiRequest<AddUnitRequest> request)
        {
            return await _productManageService.AddUnit(request.Data);
        }

        /// <summary>
        /// 编辑品牌
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> EditUnit([FromBody] ApiRequest<AddUnitRequest> request)
        {
            return await _productManageService.EditUnit(request.Data);
        }

        /// <summary>
        /// 添加品牌
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> AddBrand([FromBody] ApiRequest<AddBrandRequest> request)
        {
            return await _productManageService.AddBrand(request.Data);
        }

        /// <summary>
        /// 编辑品牌
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> EditBrand([FromBody] ApiRequest<AddBrandRequest> request)
        {
            return await _productManageService.EditBrand(request.Data);
        }

        /// <summary>
        /// 列出类目树
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ListCategoryVo>>> ListCategory([FromQuery] GetCategoryListRequest request)
        {
            return await _productManageService.ListCategory(request);
        }

        /// <summary>
        /// 获取类目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<DimCategoryVo>> GetCategory([FromQuery]int id)
        {
            return await _productManageService.GetCategory(id);
        }

        /// <summary>
        /// 类目树选项查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<CategoryTreeSelectVo>>> CategoryTreeSelect([FromQuery] GetCategoryListRequest request)
        {
            return await _productManageService.CategoryTreeSelect(request);
        }

        /// <summary>
        /// 修改类目
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> UpdateCategory([FromBody]ApiRequest<DimCategoryVo> request)
        {
            return await _productManageService.UpdateCategory(request.Data);
        }

        /// <summary>
        /// 新增类目
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> AddCategory([FromBody]ApiRequest<DimCategoryVo> request)
        {
            return await _productManageService.AddCategory(request.Data);
        }

        /// <summary>
        /// 删除类目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> DeleteCategory([FromBody]ApiRequest<int> request)
        {
            return await _productManageService.DeleteCategory(request.Data);
        }


        /// <summary>
        /// 分页查询商品属性信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<AttributeSearchResponse>> SearchAttribute([FromQuery]AttributeSearchRequest request)
        {
            var result = await _productManageService.SearchAttribute(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 根据属性Id 获取属性名称
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<AttributeResponse>> GetAttributeNameById(int attributeNameId)
        {
            var result = await _productManageService.GetAttributeNameById(attributeNameId);
            return Result.Success(result);
        }

        /// <summary>
        ///  编辑属性
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> SaveAttribute([FromBody]ApiRequest<AttributeEditRequest> request)
        {
            var result = await _productManageService.SaveAttribute(request);
            return Result.Success<bool>(result);
        }

        /// <summary>
        /// 获取所有的属性名信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<AttributeNameVo>>> GetAttributeNames()
        {
            var result = await _productManageService.GetAttributeNames();
            return Result.Success(result);
        }

        /// <summary>
        /// 获取类目属性信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<CategoryAttributeVo>>> SelectCategoryAttribute([FromQuery]CategoryAttributeRequest request)
        {
            var result = await _productManageService.SelectCategoryAttribute(request);
            return Result.Success(result);
        }

        /// <summary>
        ///  编辑类目属性
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> SaveCategoryAttribute([FromBody]ApiRequest<CategoryAttributeEditRequest> request)
        {
            var result = await _productManageService.SaveCategoryAttribute(request.Data);
            return Result.Success<bool>(result);
        }

        /// <summary>
        /// 根据父级类目获取子类目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<DimCategoryVo>>> GetCategoryByParentId(
            [FromQuery] CategoryByParentIdRequest request)
        {
            var result = await _productManageService.GetCategoryByParentId(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 获取所有品牌
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<GetProductBrandVo>>> GetAllProductBrandList()
        {
            var result = await _productManageService.GetAllProductBrandList();

            return Result.Success(result);
        }

        #region 上门产品管理

        /// <summary>
        /// 获取上门产品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<DoorProductVo>> GetDoorProductPageList(
            [FromQuery] DoorProductPageListRequest request)
        {
            var result = await _productManageService.GetDoorProductPageList(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 编辑上门产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditDoorProduct([FromBody] ApiRequest<EditDoorProductRequest> request)
        {
            var result = await _productManageService.EditDoorProduct(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 新增上门产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AddDoorProducts([FromBody] ApiRequest<AddDoorProductsRequest> request)
        {
            var result = await _productManageService.AddDoorProducts(request.Data);

            return Result.Success(result);
        }

        #endregion

        #region 安装服务

        /// <summary>
        /// 查询安装服务列表
        /// </summary>
        [HttpGet]
        public async Task<ApiPagedResult<ProductInstallServiceVo>> GetProductInstallService(
            [FromQuery] ProductInstallServiceRequest request)
        {
            var result = await _productManageService.GetProductInstallService(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 查询安装服务详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ProductInstallServiceVo>> GetProductInstallServiceDetail(
            [FromQuery] ProductInstallServiceDetailRequest request)
        {
            var result = await _productManageService.GetProductInstallServiceDetail(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 搜索安装服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ProductBaseInfoVo>>> SearchInstallService(
            [FromQuery] SearchInstallServiceRequest request)
        {
            var result = await _productManageService.SearchInstallService(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 编辑安装服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditInstallService([FromBody] ApiRequest<EditInstallServiceRequest> request)
        {
            var result = await _productManageService.EditInstallService(request.Data);

            return Result.Success(result);
        }

        #endregion

        #region 热销产品

        /// <summary>
        /// 分页获取热门商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<ConfigHotProductVo>> GetHotProductPageList(
            [FromQuery] HotProductPageListRequest request)
        {
            var result = await _productManageService.GetHotProductPageList(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 搜索商品For添加热门商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<ConfigHotProductVo>> SearchProductForHot(
            [FromQuery] SearchProductForHotRequest request)
        {
            var result = await _productManageService.SearchProductForHot(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 新增热销产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AddHotProduct([FromBody] ApiRequest<AddHotProductRequest> request)
        {
            var result = await _productManageService.AddHotProduct(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 编辑热销产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditHotProduct([FromBody] ApiRequest<EditHotProductRequest> request)
        {
            var result = await _productManageService.EditHotProduct(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 获取终端类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<TerminalTypeVo>>> GetTerminalTypeEnum()
        {
            var result = await _productManageService.GetTerminalTypeEnum();

            return Result.Success(result);
        }

        #endregion

        #region 套餐卡

        /// <summary>
        /// 套餐卡查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<PackageCardProductVo>> GetPackageCardProductPageList(
            [FromBody] ApiRequest<PackageCardProductPageListRequest> request)
        {
            var result = await _productManageService.GetPackageCardProductPageList(request.Data);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 搜索商品For添加套餐卡商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<PackageCardProductVo>> SearchProductForPackageCard(
            [FromQuery] SearchProductForPackageCardRequest request)
        {
            var result = await _productManageService.SearchProductForPackageCard(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 添加套餐卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AddPackageCardProduct(
            [FromBody] ApiRequest<AddPackageCardProductRequest> request)
        {
            var result = await _productManageService.AddPackageCardProduct(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 编辑套餐卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditPackageCardProduct(
            [FromBody] ApiRequest<EditPackageCardProductRequest> request)
        {
            var result = await _productManageService.EditPackageCardProduct(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 获取套餐卡类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<PackageCardTypeVo>>> GetPackageCardTypeEnum()
        {
            var result = await _productManageService.GetPackageCardTypeEnum();

            return Result.Success(result);
        }

        /// <summary>
        /// 门店套餐卡查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<GetShopPackageCardProductPageListVo>> GetShopPackageCardProductPageList(
            [FromBody] ApiRequest<GetShopPackageCardProductPageListRequest> request)
        {
           return await _productManageService.GetShopPackageCardProductPageList(request);
        }


        /// <summary>
        /// 保存门店套餐卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> SaveShopPackageCard([FromBody] ApiRequest<ConfigShopPackageCardDTO> request)
        {
            return await _productManageService.SaveShopPackageCard(request);
        }

        /// <summary>
        /// 门店套餐卡查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<GetShopPackageCardProductPageListVo>> GetShopCardDetail(
            [FromBody] ApiRequest<GetShopCardDetailRequest> request)
        {
            return await _productManageService.GetShopCardDetail(request);
        }


        #endregion
    }
}
