using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Model.ShopProduct;
using Ae.Shop.Api.Core.Request.Porduct;
using Ae.Shop.Api.Core.Request.ShopProduct;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Core.Interfaces
{
    /// <summary>
    /// 门店商品
    /// </summary>
    public interface IShopProductService
    {
        /// <summary>
        /// 外采商品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<ShopProductListVo>> GetShopProductList(ShopProductListRequest request);

        /// <summary>
        /// 商品详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ShopProductDetailVo> GetShopProductDetail(ShopProductDetailRequest request);

        /// <summary>
        /// 删除商品 禁用
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> DeleteShopProduct(DeleteShopProductRequest request);

        /// <summary>
        /// 新增产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> SaveShopProduct(AddShopProductRequest request);

        /// <summary>
        /// 获取门店服务大类
        /// </summary>
        /// <returns></returns>
        Task<List<ShopServiceTypeVo>> GetShopServiceType();

        /// <summary>
        /// 获取商品单位
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetShopProductUnit();

        /// <summary>
        /// 获取外采产品类目
        /// </summary>
        /// <returns></returns>
        Task<List<ProductCategoryVo>> GetShopProductCategory();

        /// <summary>
        /// 获取商品品牌
        /// </summary>
        /// <returns></returns>
        Task<List<CatalogBrandVo>> GetBrandList();

        /// <summary>
        ///   查询类目息 by 类目 和 类目 level 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        Task<List<CategoryInfoVo>> GetCategorysById(int? categoryId, int? level);

        /// <summary>
        /// 搜索商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<ProductAllInfoVo>> SearchProductPageList(
            SearchProductPageListRequest request);

        /// <summary>
        /// 查询单位信息
        /// </summary>
        /// <returns></returns>
        Task<List<UnitVo>> GetUnitList();

        /// <summary>
        ///  编辑商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> SaveProduct(ProductEditRequest request);

        /// <summary>
        /// 查询商品信息
        /// </summary>
        /// <param name="productCode"></param>
        Task<ProductDetailVo> GetProductByProductCode(string productCode);
    }
}
