using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Core.Interfaces
{
    public interface IOrderCommentService
    {
        Task<ApiResult<OrderCommentResponse>> OrderComment(OrderCommentRequest request);
        Task<ApiResult<List<GetCommentLabelsResponse>>> GetCommentLabels(GetCommentLabelsRequest request);
        Task<ApiResult> SubmitOrderComment(SubmitOrderCommentRequest request);
        Task<ApiPagedResultData<GetMyCommentListResponse>> GetMyCommentList(BasePageRequest request);
        Task<bool> LikeComment(LikeCommentRequest request);
        Task<BaseCommentNumAndApplauseRateResponse> GetProductCommentNumAndApplauseRate(BaseGetProductCommentRequest request);
        Task<ApiPagedResult<BaseCommentLabelListResponse>> GetProductCommentLabelList(GetProductCommentListRequest request);
        Task<ApiPagedResult<BaseCommentListResponse>> GetProductCommentList(GetProductCommentListRequest request);
        Task<BaseCommentNumAndApplauseRateResponse> GetTechCommentNumAndApplauseRate(BaseGetTechCommentRequest request);
        Task<ApiPagedResult<BaseCommentLabelListResponse>> GetTechCommentLabelList(GetTechCommentListRequest request);
        Task<ApiPagedResult<BaseCommentListResponse>> GetTechCommentList(GetTechCommentListRequest request);
        Task<BaseCommentNumAndApplauseRateResponse> GetShopCommentNumAndApplauseRate(BaseGetShopCommentRequest request);
        Task<ApiPagedResult<BaseCommentLabelListResponse>> GetShopCommentLabelList(GetShopCommentListRequest request);
        Task<ApiPagedResult<BaseCommentListResponse>> GetShopCommentList(GetShopCommentListRequest request);
        Task<ApiResult<AppendCommentResponse>> AppendComment(AppendCommentRequest request);
        Task<ApiResult<bool>> SubmitAppendComment(SubmitAppendCommentRequest request);
    }
}
