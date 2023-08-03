using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Core.Interfaces
{
    public interface IShopOperationService
    {
        Task<ApiPagedResult<GetOrderCommentForShopVO>> GetOrderCommentForShopList(ApiRequest<GetOrderCommentBaseRequest> request);
        Task<ApiResult<ReplyDetailResponse>> ReplyDetail(ReplyDetailRequest request);
        Task<ApiResult> ReplyComment(ReplyCommentRequest data);
        Task<ApiResult<GetCommentListResponse>> GetCommentList(GetCommentListRequest request);
    }
}
