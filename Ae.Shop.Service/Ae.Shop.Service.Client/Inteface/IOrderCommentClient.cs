using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Client.Model.OrderComment;
using Ae.Shop.Service.Client.Request;
using Ae.Shop.Service.Client.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Client.Inteface
{
    public interface IOrderCommentClient
    {
        Task<ApiResult<GetShopCommentNumClientResponse>> GetShopCommentNum(BaseGetShopClientRequest request);

        /// <summary>
        /// 获取门店评分
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ShopScoreDto>> GetShopScore(ShopScoreClientRequest request);
    }
}
