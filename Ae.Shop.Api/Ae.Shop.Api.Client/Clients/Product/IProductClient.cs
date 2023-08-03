using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Client.Model.Product;
using Ae.Shop.Api.Client.Request;
using Ae.Shop.Api.Client.Request.Product;
using Ae.Shop.Api.Client.Response.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Client.Clients.Product
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
        ///  分页查询门店商品信息
        /// </summary>
        /// <returns></returns>
        Task<ApiPagedResult<ShopProductDto>> SearchShopProduct(ShopProductSearchClientRequest request);

        Task<List<ProductDetailClientVo>> GetProductsByProductCodes(ProductDetailSearchClientRequest request);

        /// <summary>
        /// 获取产品类目分级展示
        /// </summary>
        /// <returns></returns>
        Task<List<ProductCategoryDto>> GetProductCategory(ProductCategoryClientRequest request);

        /// <summary>
        /// 查询商品品牌
        /// </summary>
        /// <returns></returns>
        Task<List<BrandDto>> GetBrandList();

        /// <summary>
        /// 查询类目息 by 类目 和 类目 level 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        Task<List<CategoryInfoDto>> GetCategorysById(int? categoryId, int? level);

        /// <summary>
        /// 查询单位信息
        /// </summary>
        /// <returns></returns>
        Task<List<UnitDto>> GetUnitList();

        /// <summary>
        /// 编辑商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> SaveProduct(ProductEditClientRequest request);

        /// <summary>
        /// 查询套餐商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ProductPackageDto>> GetPackageProductsByCodes(ProductDetailSearchClientRequest request);

        /// <summary>
        ///  根据类目Id获取属性信息
        /// </summary>
        /// <param name="categoryId">三级类目Id</param>
        /// <returns></returns>
        Task<List<CategoryAttributeDto>> GetAttributesByCategoryId(string categoryId);
    }
}
