using ApolloErp.Web.WebApi;
using Ae.OrderComment.Service.Client.Model;
using Ae.OrderComment.Service.Client.Request;
using Ae.OrderComment.Service.Client.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.OrderComment.Service.Client.Interface
{
    public interface IShopManageClient
    {
        Task<ApiResult<GetShopInfoClientResponse>> GetShopSimpleInfoAsync(GetShopSimpleInfoClientRequest request);
        Task<ApiPagedResult<ShopSimpleInfoClientDTO>> GetShopListAsync(GetShopListClientRequest request);
        /// <summary>
        /// 更新门店好评率
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateShopScoreAsync(UpdateShopScoreByShopIdsRequest request);
    }
}
