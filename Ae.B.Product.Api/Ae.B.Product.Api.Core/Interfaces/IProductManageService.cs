using Microsoft.AspNetCore.Http;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Core.Model;
using Ae.B.Product.Api.Core.Model.Product;
using Ae.B.Product.Api.Core.Request;
using Ae.B.Product.Api.Core.Request.Product;
using Ae.B.Product.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Product.Api.Core.Interfaces
{
    /// <summary>
    /// 产品管理
    /// </summary>
    public interface IProductManageService
    {
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="request">搜索关键字</param>
        /// <returns></returns>
        Task<ProductSearchResponse> SearchProduct(ProductSearchRequest request);

        /// <summary>
        /// 查询商品信息
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        Task<ProductDetailVo> GetProductByProductCode(string productCode);

        /// <summary>
        /// 查询类目息 by 类目 和 类目 level  查询类目
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        Task<List<CategoryInfoVo>> GetCategorysById(int? categoryId, int? level);

        /// <summary>
        /// 查询单位信息
        /// </summary>
        /// <returns></returns>
        Task<List<UnitVo>> GetUnits();

        /// <summary>
        /// 查询商品品牌
        /// </summary>
        /// <returns></returns>
        Task<List<CatalogBrandVo>> GetBrandList();

        /// <summary>
        ///  根据类目Id获取属性信息
        /// </summary>
        /// <param name="categoryId">三级类目Id</param>
        /// <returns></returns>
        Task<List<CategoryAttributeVo>> GetAttributesByCategoryId(string categoryId);

        /// <summary>
        /// 新增商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> SaveProduct(ApiRequest<ProductEditRequest> request);
        /// <summary>
        /// 导入商品
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<List<ProductImportVo>> ImportProduct(IFormFile file);
        /// <summary>
        /// 导入商品属性
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<List<ProductAttributeImportVo>> ImportProductAttribute(IFormFile file);

        /// <summary>
        /// 分页查询商品品牌信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResult<GetProductBrandVo>> GetProductBrandList(GetProductBrandListRequest request);

        /// <summary>
        /// 分页查询商品单位信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResult<GetProductUnitListVo>> GetProductUnitList(
            GetProductUnitListRequest request);

        /// <summary>
        /// 添加品牌
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> AddBrand(AddBrandRequest request);

        /// <summary>
        /// 添加品牌
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> EditBrand(AddBrandRequest request);

        /// <summary>
        /// 添加单位
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> AddUnit(AddUnitRequest request);

        /// <summary>
        /// 编辑单位
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> EditUnit(AddUnitRequest request);

        /// <summary>
        /// 列出类目树
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<ListCategoryVo>>> ListCategory(GetCategoryListRequest request);
        /// <summary>
        /// 获取类目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResult<DimCategoryVo>> GetCategory(int id);
        /// <summary>
        /// 类目树选项查询
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<CategoryTreeSelectVo>>> CategoryTreeSelect(GetCategoryListRequest request);
        /// <summary>
        /// 修改类目
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateCategory(DimCategoryVo vo);
        /// <summary>
        /// 新增类目
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        Task<ApiResult> AddCategory(DimCategoryVo vo);
        /// <summary>
        /// 删除类目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResult> DeleteCategory(int id);

        /// <summary>
        /// 分页查询商品属性信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AttributeSearchResponse> SearchAttribute(AttributeSearchRequest request);
        /// <summary>
        /// 根据属性Id 获取属性名称
        /// </summary>
        /// <param name="attributeNameId"></param>
        /// <returns></returns>
        Task<AttributeResponse> GetAttributeNameById(int attributeNameId);
        /// 新增商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> SaveAttribute(ApiRequest<AttributeEditRequest> request);

        /// <summary>
        ///  获取所有的属性名信息
        /// </summary>
        /// <returns></returns>
        Task<List<AttributeNameVo>> GetAttributeNames();

        /// <summary>
        ///获取类目属性信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<CategoryAttributeVo>> SelectCategoryAttribute(CategoryAttributeRequest request);

        /// 新增类目属性信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> SaveCategoryAttribute(CategoryAttributeEditRequest request);

        /// <summary>
        /// 根据父级类目获取子类目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<DimCategoryVo>> GetCategoryByParentId(CategoryByParentIdRequest request);

        /// <summary>
        /// 获取所有品牌
        /// </summary>
        /// <returns></returns>
        Task<List<GetProductBrandVo>> GetAllProductBrandList();

        /// <summary>
        /// 获取上门产品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<DoorProductVo>> GetDoorProductPageList(
            DoorProductPageListRequest request);

        /// <summary>
        /// 编辑上门产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditDoorProduct(EditDoorProductRequest request);

        /// <summary>
        /// 新增上门产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddDoorProducts(AddDoorProductsRequest request);

        /// <summary>
        /// 查询安装服务列表
        /// </summary>
        Task<ApiPagedResultData<ProductInstallServiceVo>>
            GetProductInstallService(ProductInstallServiceRequest request);

        /// <summary>
        /// 查询安装服务详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ProductInstallServiceVo> GetProductInstallServiceDetail(ProductInstallServiceDetailRequest request);

        /// <summary>
        /// 搜索安装服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ProductBaseInfoVo>> SearchInstallService(SearchInstallServiceRequest request);

        /// <summary>
        /// 编辑安装服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditInstallService(EditInstallServiceRequest request);

        /// <summary>
        /// 分页获取热门商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<ConfigHotProductVo>> GetHotProductPageList(
            HotProductPageListRequest request);

        /// <summary>
        /// 搜索商品For添加热门商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<ConfigHotProductVo>> SearchProductForHot(
            SearchProductForHotRequest request);

        /// <summary>
        /// 新增热销产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddHotProduct(AddHotProductRequest request);

        /// <summary>
        /// 编辑热销产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditHotProduct(EditHotProductRequest request);

        /// <summary>
        /// 获取终端类型
        /// </summary>
        /// <returns></returns>
        Task<List<TerminalTypeVo>> GetTerminalTypeEnum();

        /// <summary>
        /// 套餐卡查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<PackageCardProductVo>> GetPackageCardProductPageList(
            PackageCardProductPageListRequest request);

        /// <summary>
        /// 搜索商品For添加套餐卡商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<PackageCardProductVo>> SearchProductForPackageCard(
            SearchProductForPackageCardRequest request);

        /// <summary>
        /// 添加套餐卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddPackageCardProduct(AddPackageCardProductRequest request);

        /// <summary>
        /// 编辑套餐卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditPackageCardProduct(EditPackageCardProductRequest request);

        /// <summary>
        /// 获取套餐卡类型
        /// </summary>
        /// <returns></returns>
        Task<List<PackageCardTypeVo>> GetPackageCardTypeEnum();

        Task<ApiPagedResult<GetShopPackageCardProductPageListVo>> GetShopPackageCardProductPageList(
            ApiRequest<GetShopPackageCardProductPageListRequest> request);

        Task<ApiResult> SaveShopPackageCard(ApiRequest<ConfigShopPackageCardDTO> request);

        Task<ApiResult<GetShopPackageCardProductPageListVo>> GetShopCardDetail(
           ApiRequest<GetShopCardDetailRequest> request);
    }
}
