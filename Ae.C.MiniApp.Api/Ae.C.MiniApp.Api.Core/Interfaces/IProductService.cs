using System.Collections.Generic;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Model.Product;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Request.Product;
using Ae.C.MiniApp.Api.Core.Response.Product;

namespace Ae.C.MiniApp.Api.Core.Interfaces
{
    public interface IProductService
    {
        /// <summary>
        /// 商品搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ProductSearchResponse> ProductSearch(ProductSearchRequest request);

        /// <summary>
        ///  商品详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ProductDetailResponse> ProductDetail(ProductDetailRequest request);

        /// <summary>
        ///  商品规格接口
        ///  查询是否设置关联商品
        /// </summary>
        /// <param name="productCode">商品编码</param>
        /// <returns></returns>
        Task<ProductSpecificationResponse> Specifications(string productCode);

        /// <summary>
        ///  获取首页美容洗车类目信息
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        List<CategoryInfoVo> GetBeautyWashCategorys(int categoryId);

        /// <summary>
        /// 根据类目查询商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<CategoryProductVo>> SelectProductsByCategory(CategoryProductRequest request);

        Task<ApiResult<List<FlashSaleConfigVo>>> GetActiveFlashSaleConfigs(GetActiveFlashSaleConfigsRequest request);
    }
}