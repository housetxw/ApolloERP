using ApolloErp.Web.WebApi;
using Ae.Product.Service.Client.Model;
using Ae.Product.Service.Client.Request;
using Ae.Product.Service.Client.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Product.Service.Client.Interface
{
    public interface IShopManageClient
    {
        /// <summary>
        /// 根据ShopId查询门店主表信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<GetShopByIdResponse> GetShopById(int shopId);

        /// <summary>
        /// 查询门店服务上下架
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ShopServiceDto>> GetShopServiceListAsync(ShopServiceListClientRequest request);

        Task<ApiResult<List<ShopGrouponProductDTO>>> GetShopGrouponProduct(GetShopGrouponProductRequest request);
    }
}
