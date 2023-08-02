using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ApolloErp.Web.WebApi;
using Ae.Product.Service.Core.Model;
using Ae.Product.Service.Core.Model.Category;
using Ae.Product.Service.Core.Model.Config;
using Ae.Product.Service.Core.Model.DoorProduct;
using Ae.Product.Service.Core.Model.InstallService;
using Ae.Product.Service.Core.Request;
using Ae.Product.Service.Core.Request.Config;
using Ae.Product.Service.Core.Request.DoorProduct;
using Ae.Product.Service.Core.Request.InstallService;
using Ae.Product.Service.Core.Request.Product;
using Ae.Product.Service.Core.Response;

namespace Ae.Product.Service.Core.Interfaces
{
    /// <summary>
    /// 产品管理
    /// </summary>
    public interface IProductManageService
    {
        /// <summary>
        /// 查询商品品牌
        /// </summary>
        /// <returns></returns>
        Task<List<BrandVO>> GetCatalogBrandAsync();

        /// <summary>
        /// 添加品牌
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddBrandAsync(AddBrandRequest request);

        /// <summary>
        /// 获取所有产品类目
        /// </summary>
        /// <returns></returns>
        Task<List<CategoryInfoVo>> GetAllCategoryFromCache(long shopId);

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="request">搜索关键字</param>
        /// <returns></returns>
        ProductSearchResponse SearchProduct(ProductSearchRequest request);

        /// <summary>
        /// 产品搜索-子产品搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<ProductBaseInfoVo>> SearchProductCommon(SearchProductCommonRequest request);

        /// <summary>
        /// 搜索产品-子产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<BaoYangPackageProductModel>> SearchProduct(SearchProductRequest request);

        /// <summary>
        /// 获取产品属性
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ProductAttributeVo>> GetProductAttribute(GetProductAttributeRequest request);

        /// <summary>
        /// 获取产品类目分级展示
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ProductCategoryVo>> GetProductCategory(GetProductCategoryRequest request);

        /// <summary>
        /// 查询商品信息
        /// </summary>
        /// <param name="productCodes"></param>
        /// <returns></returns>
        List<ProductDetailVo> GetProductsByProductCode(ProductDetailSearchRequest request);

        /// <summary>
        /// 根据类目查询商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<ProductSearchInfoVo>> SelectProductsByCategory(CategoryProductRequest request);


        /// <summary>
        /// 查询类目息 by 类目 和 类目 level  查询类目
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        List<CategoryInfoVo> GetCategorysById(int? categoryId, int? level);

        /// <summary>
        /// 查询套餐商品信息
        /// </summary>
        /// <param name="packageCodes"></param>
        /// <returns></returns>
        List<ProductPackageVo> GetPackageProductsByCode(List<string> packageCodes);

        /// <summary>
        /// 查询关联商品信息
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        AssociateProductVo GetAssociateProductsByCodes(string productCode);

        /// <summary>
        /// 查询单位信息
        /// </summary>
        /// <returns></returns>
        List<UnitVo> GetUnits();

        /// <summary>
        /// 批量生成字产品
        /// </summary>

        void BatchGenerationProducts(BatchGenerationProductsRequest request);

        /// <summary>
        /// 根据 partNo 查询商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        List<ProductSearchInfoVo> SelectProductsByPartNos(ProductPartRequest request);

        /// <summary>
        ///  根据类目Id获取属性信息
        /// </summary>
        /// <param name="categoryId">三级类目Id</param>
        /// <returns></returns>
        List<CategoryAttributeVo> GetAttributesByCategoryId(string categoryId);


        /// <summary>
        /// 获取所有的属性信息
        /// </summary>
        /// <returns></returns>
        List<AttributeNameVo> SelectAttributeNames(AttributeNameSearchRequest request);

        /// <summary>
        /// 新增商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool SaveProduct(ProductEditRequest request);

        /// <summary>
        /// 批量导入商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool ImportProduct(ImportProductRequest request);

        /// <summary>
        /// 初始化轮胎数据
        /// </summary>
        /// <param name="specifications"></param>
        /// <param name="pattern"></param>
        /// <param name="price"></param>
        /// <param name="zaiZhong"></param>
        /// <param name="suDu"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<bool> IniTireProduct(string specifications, string pattern, string price, string zaiZhong, string suDu, string remark);

        /// <summary>
        /// 批量导入商品属性
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool ImportProductAttribute(ImportProductAttributeRequest request);

        /// <summary>
        ///  批量更新商品
        /// </summary>
        /// <param name="request">商品信息</param>
        /// <returns></returns>
        bool BatchUpdateProduct(BatchUpdateProductRequest request);

        /// <summary>
        ///  批量更新商品图片接口
        /// </summary>
        /// <returns></returns>
        bool BatchUpdateProductImage();

        /// <summary>
        /// 分页查询商品属性信息
        /// </summary>
        /// <returns></returns>
        ApiPagedResultData<AttributeNameVo> SearchAttribute(AttributeSearchRequest request);

        /// 分页查询商品的品牌
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<GetProductBrandVo>> GetProductBrandList(GetProductBrandListRequest request);

        /// <summary>
        /// 添加品牌
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditBrandAsync(AddBrandRequest request);


        /// <summary>
        /// 分页查询商品的品牌
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<GetProductUnitListVo>> GetProductUnitList(GetProductUnitListRequest request);

        /// <summary>
        /// 添加品牌
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddUnitAsync(AddUnitRequest request);


        /// <summary>
        /// 编辑单位
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditUnitAsync(AddUnitRequest request);

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
        /// 父级类目查子集类目集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<DimCategoryVo>>> GetCategoryByParentId(CategoryByParentIdRequest request);

        /// <summary>
        /// 获取所有品牌
        /// </summary>
        /// <returns></returns>
        Task<List<GetProductBrandVo>> GetAllProductBrandList();

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
        /// <param name="vo"></param>
        /// <returns></returns>
        Task<ApiResult> DeleteCategory(DimCategoryVo vo);

        /// <summary>
        /// 保存 编辑属性信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool SaveAttribute(AttributeEditRequest request);

        /// <summary>
        /// 获取单个属性信息
        /// </summary>
        /// <returns></returns>
        AttributeResponse GetAttributeById(int attributeNameId);

        /// <summary>
        /// 编辑类目属性
        /// </summary>
        /// <param name="request"></param>
        bool SaveCategoryAttribute(CategoryAttributeEditRequest request);

        /// <summary>
        /// 获取类目属性信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        List<CategoryAttributeVo> SelectCategoryAttribute(CategoryAttributeRequest request);

        /// <summary>
        /// 服务大类 商品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ProductListByServiceTypeResponse> GetProductListByServiceType(ProductListByServiceTypeRequest request);

        /// <summary>
        /// 商品详情页 （自营商品、门店服务项目）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ProductDetailPageDataResponse> GetProductDetailPageData(ProductDetailPageDataRequest request);

        /// <summary>
        /// 获取上门服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ProductBaseInfoVo> GetShangMenService(ShangMenServiceRequest request);

        /// <summary>
        /// 判断是否上门产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<CheckDoorProductVo>> CheckDoorProduct(CheckDoorProductRequest request);

        /// <summary>
        /// 获取上门服务费
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ProductBaseInfoVo> GetDoorService(DoorServiceRequest request);

        /// <summary>
        /// 获取批发商品列表
        /// </summary>
        /// <returns></returns>
        Task<ApiPagedResultData<ProductBaseInfoVo>> GetWholesaleProducts(WholesaleProductsRequest request);

        /// <summary>
        /// 前台类目搜索产品
        /// 子产品-上架-实物产品
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="pid"></param>
        /// <param name="brand"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<ProductBaseInfoVo>> GetProductByFrontCategoryConfig(
            List<string> categoryId, List<string> pid, List<string> brand, int pageIndex, int pageSize);

        /// <summary>
        /// 获取上门产品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<DoorProductVo>> GetDoorProductPageList(DoorProductPageListRequest request);

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
        Task<ApiPagedResultData<ProductInstallServiceVo>> GetProductInstallService(
            ProductInstallServiceRequest request);

        /// <summary>
        /// 查询安装服务详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ProductInstallServiceVo> GetProductInstallServiceDetail(
            ProductInstallServiceDetailRequest request);

        /// <summary>
        /// 添加安装服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddInstallService(AddInstallServiceRequest request);

        /// <summary>
        /// 编辑安装服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditInstallService(EditInstallServiceRequest request);

        /// <summary>
        /// 获取产品对应安装服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<InstallServiceByProductResponse> GetInstallServiceByProduct(InstallServiceByProductRequest request);

        /// <summary>
        /// 搜索替换商品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<BaoYangPackageProductModel>> SearchReplaceProduct(SearchReplaceProductRequest request);

        /// <summary>
        /// 搜索商品通用：平台产品or门店外采
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<BaoYangPackageProductModel>> SearchShopProductCommon(SearchShopProductCommon request);

        /// <summary>
        /// 获取前台类目列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<FrontCategoryVo>> GetFrontCategoryPageList(FrontCategoryPageListRequest request);

        /// <summary>
        /// 前台分类详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<FrontCategoryDetailVo> GetFrontCategoryDetail(FrontCategoryDetailRequest request);

        /// <summary>
        /// 新增前台分类配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddFrontCategory(AddFrontCategoryRequest request);

        /// <summary>
        /// 编辑前台分类配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditFrontCategory(EditFrontCategoryRequest request);
    }
}
