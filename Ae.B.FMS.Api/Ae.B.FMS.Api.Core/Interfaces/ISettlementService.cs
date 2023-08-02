using ApolloErp.Web.WebApi;
using Ae.B.FMS.Api.Core.Model.Settlement;
using Ae.B.FMS.Api.Core.Request;
using Ae.B.FMS.Api.Core.Request.Settlement;
using Ae.B.FMS.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.FMS.Api.Core.Interfaces
{
    public interface ISettlementService
    {
        Task<ApiPagedResult<GetSettlementListResponse>> GetSettlementList(GetSettlementListRequest request);

        Task<List<GetSettlementDetailResponse>> GetSettlementDetail(GetSettlementDetailRequest request);

        Task<ApiResult> SaveSettlementReview(SaveSettlementReviewRequest request);

        Task<ApiResult<SettlementPayReviewResponse>> SettlementPayReviewDO(GetSettlementDetailRequest request);

        /// <summary>
        /// 得到采购结算单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        Task<ApiPagedResult<PurchaseSettlementBatchDTO>> GetPurchaseSettlementList(GetPurchaseSettlementListRequest request);


        /// <summary>
        /// 得到采购结算单详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        Task<ApiResult<List<PurchaseSettlementDetailDTO>>> GetPurchaseSettlementDetail(GetPurchaseSettlementDetailRequest request);


        /// <summary>
        /// 生成结算单订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<string>> SavePurchaseSettlementOrder(ApiRequest<SavePurchaseSettlementOrderRequest> request);


        /// <summary>
        /// 审核结算单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<string>> SavePurchaseSettlementReview(ApiRequest<SavePurchaseSettlementReviewRequest> request);

    


    }
}
