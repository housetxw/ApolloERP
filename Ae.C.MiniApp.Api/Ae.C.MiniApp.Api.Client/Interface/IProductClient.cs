using System.Collections.Generic;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Model.Product;
using Ae.C.MiniApp.Api.Client.Model.ShopProduct;
using Ae.C.MiniApp.Api.Client.Request.Product;
using Ae.C.MiniApp.Api.Client.Request.ShopProduct;
using Ae.C.MiniApp.Api.Client.Response.Product;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Model.Product;
using Ae.C.MiniApp.Api.Core.Request;

namespace Ae.C.MiniApp.Api.Client.Clients.Interface
{
    public interface IProductClient
    {
        /// <summary>
        /// 商品搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ProductSearchClientResponse> ProductSearch(ProductSearchClientRequest request);

        /// <summary>
        /// 查询商品详情
        /// </summary>
        /// <param name="productCodes"></param>
        /// <returns></returns>
        Task<List<ProductDetailClientResponse>> ProductDetail(ProductDetailSearchClientRequest request);

        /// <summary>
        /// 查询关联商品信息
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        Task<AssociateProductDto> GetAssociateProduct(string productCode);

        /// <summary>
        /// 根据类目查询商品信息
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        Task<List<ProductBaseInfoDto>> SelectProductsByCategory(CategoryProductClientRequest request);

        /// <summary>
        /// 查询门店商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ShopProductDto>> SelectShopProduct(ShopProductQueryClientRequest request);

        /// <summary>
        /// 获取门店上架的服务项目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ShopProductDetailDto>> GetOnSaleShopProduct(OnSaleShopProductClientRequest request);

        /// <summary>
        /// 服务大类 商品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ProductListByServiceTypeClientResponse> GetProductListByServiceType(
            ProductListByServiceTypeClientRequest request);

        /// <summary>
        /// 商品详情页 （自营商品、门店服务项目）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ProductDetailPageDataClientResponse> GetProductDetailPageData(
            ProductDetailPageDataClientRequest request);

        /// <summary>
        /// 分页获取热门商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<ConfigHotProductDto>> GetHotProductPageList(
            HotProductPageListClientRequest request);

        /// <summary>
        /// 套餐卡查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<PackageCardProductDto>> GetPackageCardProductPageList(
            PackageCardProductPageListClientRequest request);

        /// <summary>
        /// 首页活动配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ConfigAdvertisementDto>> QueryConfigAdvertisement(
            QueryConfigAdvertisementClientRequest request);

        /// <summary>
        /// 获取前台一级类目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ConfigFrontCategoryDto>> GetMainFrontCategory(MainFrontCategoryClientRequest request);

        /// <summary>
        /// 根据产品查秒杀信息
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        Task<FlashSaleProductDto> GetFlashSaleProduct(string productCode);

        Task<ApiResult<List<FlashSaleConfigVo>>> GetActiveFlashSaleConfigs(GetActiveFlashSaleConfigsRequest request);
    }
}