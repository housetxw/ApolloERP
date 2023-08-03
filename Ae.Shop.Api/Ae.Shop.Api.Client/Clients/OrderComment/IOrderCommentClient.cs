using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Client.Model;
using Ae.Shop.Api.Client.Request;
using Ae.Shop.Api.Client.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Client.Clients
{
    public interface IOrderCommentClient
    {
        Task<ApiPagedResult<GetOrderCommentForShopDTO>> GetOrderCommentForShopList(ApiRequest<GetOrderCommentBaseClientRequest> request);
        Task<ApiResult<ReplyDetailClientResponse>> ReplyDetail(ReplyDetailClientRequest request);
        Task<ApiResult> ReplyComment(ReplyCommentClientRequest request);
        Task<ApiResult<GetCommentListClientResponse>> GetCommentList(GetCommentListClientRequest request);
    }
}
