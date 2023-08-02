using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Client.Model;
using Ae.B.Product.Api.Client.Request;
using Ae.B.Product.Api.Client.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Core.Model;
using Ae.B.Product.Api.Core.Request;
using Ae.B.Product.Api.Client.Model.ShopProduc;
using Ae.B.Product.Api.Client.Request.ShopProduct;
using Ae.B.Product.Api.Client.Request.Product;
using Ae.B.Product.Api.Client.Model.Product;

namespace Ae.B.Product.Api.Client.Interfaces
{
    public interface IProductClient
    {

        /// <summary>
        /// 搜索商品
        /// </summary>
        /// <param name="request">搜索关键字</param>
        /// <returns></returns>
        Task<ProductSearchClientResponse> SearchProduct(ProductSearchClientRequest request);

        /// <summary>
        /// 查询单位信息
        /// </summary>
        /// <returns></returns>
        Task<List<UnitDto>> GetUnits();

        /// <summary>
        /// 查询商品信息
        /// </summary>
        /// <param name="productCodes"></param>
        /// <returns></returns>
        Task<List<ProductDetailDto>> GetProductsByProductCodes(ProductDetailSearchClientRequest request);


        /// <summary>
        /// 查询类目息 by 类目 和 类目 level  查询类目
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        Task<List<CategoryInfoDto>> GetCategorysById(int? categoryId, int? level);


        /// <summary>
        /// 查询商品品牌
        /// </summary>
        /// <returns></returns>
        Task<List<BrandDto>> GetBrandList();

        /// <summary>
        ///  根据类目Id获取属性信息
        /// </summary>
        /// <param name="categoryId">三级类目Id</param>
        /// <returns></returns>
        Task<List<CategoryAttributeDto>> GetAttributesByCategoryId(string categoryId);

        /// <summary>
        ///  编辑商品
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveProduct(ProductEditClientRequest request);

        /// <summary>
        /// 查询套餐商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ProductPackageDto>> GetPackageProductsByCodes(ProductDetailSearchClientRequest request);

        /// <summary>
        ///  批量导入商品
        /// </summary>
        /// <returns></returns>
        Task<bool> ImportProduct(ImportProductClientRequest request);

        /// <summary>
        ///  批量导入商品属性
        /// </summary>
        /// <returns></returns>
        Task<bool> ImportProductAttribute(ImportProductAttributeClientRequest request);

        /// <summary>
        ///  获取所有的属性信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<AttributeNameDto>> SelectAttributeNames(AttributeNameSearchClientRequest request);

        /// <summary>
        /// 根据 partNo 查询商品信息
        /// </summary>
        /// <returns></returns>
        Task<List<ProductBaseInfoDto>> SelectProductsByPartNos(ProductPartClientRequest request);

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
        Task<ApiResult<List<ListCategoryDto>>> ListCategory(GetCategoryListRequest request);

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
        /// <param name="vo"></param>
        /// <returns></returns>
        Task<ApiResult> DeleteCategory(DimCategoryVo vo);

        /// <summary>
        ///  分页查询商品属性信息
        /// </summary>
        /// <param name="categoryId">三级类目Id</param>
        /// <returns></returns>
        Task<ApiPagedResult<AttributeNameDto>> SearchAttribute(AttributeSearchClientRequest request);

        /// <summary>
        ///  编辑属性信息
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveAttribute(AttributeEditClientRequest request);

        /// <summary>
        ///  获取单个属性信息
        /// </summary>
        /// <returns></returns>
        Task<AttributeClientResponse> GetAttributeById(int attributeNameId);

        /// <summary>
        ///  获取类目属性信息
        /// </summary>
        /// <param name="categoryId">三级类目Id</param>
        /// <returns></returns>
        Task<List<CategoryAttributeDto>> SelectCategoryAttribute(CategoryAttributeClientRequest clientRequest);

        /// <summary>
        ///  编辑类目属性
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveCategoryAttribute(CategoryAttributeEditClientRequest request);

        /// <summary>
        ///  分页查询门店商品信息
        /// </summary>
        /// <returns></returns>
        Task<ApiPagedResult<ShopProductDto>> SearchShopProduct(ShopProductSearchClientRequest request);

        /// <summary>
        ///  保存门店商品信息
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveShopProduct(ShopProductEditClientRequest request);

        /// <summary>
        /// 查询单个门店商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ShopProductDto> GetShopProductByCode(string shopProductCode);

        /// <summary>
        ///  批量导入门店商品信息
        /// </summary>
        /// <returns></returns>
        Task<bool> ImportShopProduct(ImportShopProductClientRequest request);

        /// <summary>
        ///  审核门店商品上架
        /// </summary>
        /// <returns></returns>
        Task<bool> AuditShopProduct(ShopProductAuditClientRequest request);

        /// <summary>
        /// 防伪码查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ProductSecurityCodeDto> GetSecurityCode(SecurityCodeClientRequest request);

        /// <summary>
        /// 根据父级类目获取子类目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<DimCategoryVo>> GetCategoryByParentId(CategoryByParentIdClientRequest request);

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
        Task<ApiPagedResultData<DoorProductDto>> GetDoorProductPageList(
            DoorProductPageListClientRequest request);

        /// <summary>
        /// 编辑上门产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditDoorProduct(EditDoorProductClientRequest request);

        /// <summary>
        /// 新增上门产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddDoorProducts(AddDoorProductsClientRequest request);

        /// <summary>
        /// 查询安装服务列表
        /// </summary>
        Task<ApiPagedResultData<ProductInstallServiceDto>> GetProductInstallService(
            ProductInstallServiceClientRequest request);

        /// <summary>
        /// 查询安装服务详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ProductInstallServiceDto> GetProductInstallServiceDetail(
            ProductInstallServiceDetailClientRequest request);

        /// <summary>
        /// 搜索安装服务
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        Task<List<ProductBaseInfoDto>> SearchInstallService(string content);

        /// <summary>
        /// 编辑安装服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditInstallService(EditInstallServiceClientRequest request);

        /// <summary>
        /// 分页获取热门商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<ConfigHotProductDto>> GetHotProductPageList(
            HotProductPageListClientRequest request);

        /// <summary>
        /// 新增热销产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddHotProduct(AddHotProductClientRequest request);

        /// <summary>
        /// 编辑热销产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditHotProduct(EditHotProductClientRequest request);

        /// <summary>
        /// 搜索商品For添加热门商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<ConfigHotProductDto>> SearchProductForHot(
            SearchProductForHotClientRequest request);

        /// <summary>
        /// 套餐卡查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<PackageCardProductDto>> GetPackageCardProductPageList(
            PackageCardProductPageListClientRequest request);

        /// <summary>
        /// 搜索商品For添加套餐卡商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<PackageCardProductDto>> SearchProductForPackageCard(
            SearchProductForPackageCardClientRequest request);

        /// <summary>
        /// 添加套餐卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddPackageCardProduct(AddPackageCardProductClientRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditPackageCardProduct(EditPackageCardProductClientRequest request);

        /// <summary>
        /// 根据类目查产品
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Task<List<ProductBaseInfoDto>> SelectProductsByCategory(int categoryId);

        /// <summary>
        /// 美容团购商品查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<GrouponProductDto>> GetGrouponProductPageList(
            GrouponProductPageListClientRequest request);

        /// <summary>
        /// 搜索产品For美容团购
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<GrouponProductDto>> SearchProductForGroupon(
            SearchProductForGrouponClientRequest request);

        /// <summary>
        /// 新增团购商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddGrouponProduct(AddGrouponProductClientRequest request);

        /// <summary>
        /// 编辑美容团购商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditGrouponProduct(EditGrouponProductClientRequest request);
    }
}
