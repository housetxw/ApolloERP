using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Client.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.C.MiniApp.Api.Client.Model.OrderComment;
using Ae.C.MiniApp.Api.Client.Request.OrderComment;

namespace Ae.C.MiniApp.Api.Client.Inteface
{
    public interface IOrderCommentClient
    {
        Task<ApiResult<OrderCommentClientResponse>> OrderComment(OrderCommentClientRequest request);
        Task<ApiResult<List<GetCommentLabelsClientResponse>>> GetCommentLabels(GetCommentLabelsClientRequest request);

        /// <summary>
        /// 商品评论数查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ProductCommentTotalDto>> GetProductCommentTotalAsync(
            ProductCommentTotalClientRequest request);
        Task<ApiResult> SubmitOrderComment(SubmitOrderCommentClientRequest request);
        Task<ApiPagedResultData<GetMyCommentListClientResponse>> GetMyCommentList(GetMyCommentListClientRequest request);
        Task<bool> LikeComment(LikeCommentClietRequest request);
        Task<BaseCommentNumAndApplauseRateClientResponse> GetProductCommentNumAndApplauseRate(BaseGetProductCommentClientRequest request);
        Task<ApiPagedResult<BaseCommentLabelListClientResponse>> GetProductCommentLabelList(GetProductCommentListClientRequest request);
        Task<ApiPagedResult<BaseCommentListClientResponse>> GetProductCommentList(GetProductCommentListClientRequest request);
        Task<BaseCommentNumAndApplauseRateClientResponse> GetTechCommentNumAndApplauseRate(BaseGetTechCommentClientRequest request);
        Task<ApiPagedResult<BaseCommentLabelListClientResponse>> GetTechCommentLabelList(GetTechCommentListClientRequest request);
        Task<ApiPagedResult<BaseCommentListClientResponse>> GetTechCommentList(GetTechCommentListClientRequest request);
        Task<BaseCommentNumAndApplauseRateClientResponse> GetShopCommentNumAndApplauseRate(BaseGetShopCommentClientRequest request);
        Task<ApiPagedResult<BaseCommentLabelListClientResponse>> GetShopCommentLabelList(GetShopCommentListClientRequest request);
        Task<ApiPagedResult<BaseCommentListClientResponse>> GetShopCommentList(GetShopCommentListClientRequest request);
        Task<ApiResult<AppendCommentClientResponse>> AppendComment(AppendCommentClientRequest request);
        Task<ApiResult<bool>> SubmitAppendComment(SubmitAppendCommentClientRequest request);
    }
}
