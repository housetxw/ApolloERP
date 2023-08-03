using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Model.Product;
using Ae.C.MiniApp.Api.Core.Model.ShopProduct;
using Ae.C.MiniApp.Api.Core.Request.Product;
using Ae.C.MiniApp.Api.Core.Request.ShopProduct;
using Ae.C.MiniApp.Api.Core.Response.PageConfig;
using Ae.C.MiniApp.Api.Core.Response.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Core.Interfaces
{
    public interface IShopProductService
    {
        /// <summary>
        /// 查询门店商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ShopProductVO>> SelectShopProduct(ShopProductQueryRequest request);

        /// <summary>
        /// 获取门店服务项目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ServiceProjectVo>> GetShopServiceProject(ShopServiceProjectRequest request);

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
        /// 分页获取热门商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<HotProductVo>> GetHotProductPageList(
            HotProductPageListRequest request);

        /// <summary>
        /// 获取套餐卡列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<PackageCardProductVo>> GetPackageCardProduct(PackageCardProductRequest request);

        /// <summary>
        /// 首页功能配置
        /// </summary>
        /// <returns></returns>
        Task<MainPageConfigResponse> GetMainPageConfig(MainPageConfigRequest request);
    }
}
