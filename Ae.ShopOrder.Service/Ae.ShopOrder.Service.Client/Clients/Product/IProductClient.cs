using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Client.Request.Product;
using Ae.ShopOrder.Service.Client.Response.Product;
using Ae.ShopOrder.Service.Core.Model.Order;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Core.Request.Product;
using Ae.ShopOrder.Service.Core.Response.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Client.Clients.Product
{
    public interface IProductClient
    {
        Task<ApiResult<List<GetShopProductByCodeResponse>>> GetShopProductByCodes(GetShopProductByCodeRequest request);

        Task<ApiResult> InsertPromotionActivityOrder(InsertPromotionActivityOrderRequest request);

        /// <summary>
        /// 根据商品Pid查询促销信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<GetPromotionActivitByProductCodeListResponse>>> GetPromotionActivitByProductCodeList(GetPromotionActivitByProductCodeListRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<ProductDetailDTO>>> GetProductsByProductCodes(ProductDetailSearchRequest request);
        Task<ApiResult<List<ProductPackageDTO>>> GetPackageProductsByCodes(PackageProductSearchRequest request);
    }
}
