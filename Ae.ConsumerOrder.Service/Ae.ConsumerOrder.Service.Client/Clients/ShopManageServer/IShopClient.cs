using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Model;
using Ae.ConsumerOrder.Service.Client.Request;
using Ae.ConsumerOrder.Service.Client.Response;
using Ae.ConsumerOrder.Service.Core.Model.OrderQuery;
using Ae.ConsumerOrder.Service.Core.Request.OrderQuery;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ConsumerOrder.Service.Client.Clients
{
    public interface IShopClient
    {
        /// <summary>
        /// 获取门店单表信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<ShopDTO>> GetShopById(GetShopRequest request);
        /// <summary>
        /// 查询门店简单信息-列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResult<ShopSimpleInfoDTO>> GetShopListAsync(GetShopListRequest request);
        /// <summary>
        /// 获取门店配置表信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<ShopConfigDTO>> GetShopConfigByShopId(GetShopRequest request);

        Task<ApiResult<List<ShopGrouponProductDTO>>> GetShopGrouponProduct(GetShopGrouponProductRequest request);
    }
}
