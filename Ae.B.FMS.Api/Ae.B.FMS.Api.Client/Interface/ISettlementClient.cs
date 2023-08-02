using Ae.B.FMS.Api.Client.Request;
using Ae.B.FMS.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.B.FMS.Api.Core.Request.Settlement;
using Ae.B.FMS.Api.Core.Model.Settlement;

namespace Ae.B.FMS.Api.Client.Interface
{
    public interface ISettlementClient
    {
        Task<ApiPagedResult<GetSettlementLisClientDTO>> GetSettlementList(GetSettlementListClientRequest request);

        Task<List<GetSettlementDetailDTO>> GetSettlementDetail(GetSettlementDetailClientRequest request);

        Task<ApiResult> SaveSettlementReview(SaveSettlementReviewClientRequest request);

        Task<ApiResult<SettlementPayReviewClientDTO>> SettlementPayReviewDO(GetSettlementDetailClientRequest request);

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
