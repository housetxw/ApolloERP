using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Client.Model.ShopProduc;
using Ae.B.Product.Api.Client.Request.ShopProduct;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Product.Api.Client.Interfaces
{
    public interface IShopManageClient
    {
        /// <summary>
        /// 获取门店信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResult<ShopSimpleInfoClientDto>> GetShopListAsync(ShopListClientRequest request);
        /// <summary>
        /// 获取门店开启的服务项目
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<List<ShopServiceTypeDto>> GetShopServiceTypeAsync(long shopId);

        /// <summary>
        /// 获取门店byIds
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<List<ShopSimpleInfoClientDto>> GetShopListByIdsAsync(List<long> shopIds);
        /// <summary>
        /// 获取门店简单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ShopBaseInfoDto> GetShopSimpleInfoAsync(ShopBaseInfoRequest request);

    }
}
