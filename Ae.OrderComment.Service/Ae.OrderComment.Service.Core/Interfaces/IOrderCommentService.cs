using ApolloErp.Web.WebApi;
using Ae.OrderComment.Service.Core.Model;
using Ae.OrderComment.Service.Core.Request;
using Ae.OrderComment.Service.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.OrderComment.Service.Core.Interfaces
{
   public interface IOrderCommentService
    {
        Task<ApiPagedResultData<GetOrderCommentForProductDTO>> GetOrderCommentForProductList(GetOrderCommentForProductRequest request);

        Task<ApiPagedResultData<GetOrderCommentForShopDTO>> GetOrderCommentForShopList(GetOrderCommentForShopRequest request);

        Task<ApiPagedResultData<GetOrderCommentForTechDTO>> GetOrderCommentForTechList(GetOrderCommentForTechRequest request);

        Task<List<CommentImageDTO>> GetCommentImages(GetCommentImageRequest request);

        Task<int> CheckOrderComment(CheckOrderCommentRequest request);

        Task<List<BasicInfoDTO>> GetBaseInfos(BasicInfoRequest request);
        Task<ApiResult<OrderCommentResponse>> OrderComment(OrderCommentRequest request);
        Task<List<ProductCommentTotalDTO>> GetProductCommentTotal(GetProductCommentTotal request);

        Task<ApiPagedResultData<GetOrderCommentForReplyDTO>> GetOrderCommentForReplyList(GetOrderCommentForReplyRequest request);

        Task<int> CheckOrderReply(CheckOrderCommentRequest request);
        Task<List<GetCommentLabelsResponse>> GetCommentLabels(GetCommentLabelsRequest request);
        Task<ApiResult> SubmitOrderComment(SubmitOrderCommentRequest request);
        Task<ApiPagedResultData<GetMyCommentListResponse>> GetMyCommentList(GetMyCommentListRequest request);
        Task<bool> LikeComment(LikeCommentRequest request);
        Task<BaseCommentNumAndApplauseRateResponse> GetProductCommentNumAndApplauseRate(BaseGetProductCommentRequest request);
        Task<ApiPagedResultData<BaseCommentLabelListResponse>> GetProductCommentLabelList(GetProductCommentListRequest request);
        Task<ApiPagedResultData<BaseCommentListResponse>> GetProductCommentList(GetProductCommentListRequest request);
        Task<BaseCommentNumAndApplauseRateResponse> GetShopCommentNumAndApplauseRate(BaseGetShopCommentRequest request);
        Task<ApiPagedResultData<BaseCommentLabelListResponse>> GetShopCommentLabelList(GetShopCommentListRequest request);
        Task<ApiPagedResultData<BaseCommentListResponse>> GetShopCommentList(GetShopCommentListRequest request);
        Task<AppendCommentResponse> AppendComment(AppendCommentRequest request);
        Task<bool> SubmitAppendComment(SubmitAppendCommentRequest request);
        Task<ApiResult> DailyCommentStatistics();

        /// <summary>
        /// 获取门店评分
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ShopScoreVo>> GetShopScore(ShopScoreRequest request);
    }
}
