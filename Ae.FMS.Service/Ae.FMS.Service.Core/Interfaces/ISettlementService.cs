using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.FMS.Service.Core.Model;
using Ae.FMS.Service.Core.Model.AccountCheck;
using Ae.FMS.Service.Core.Request.Settlement;
using Ae.FMS.Service.Core.Response;
using Ae.FMS.Service.Core.Response.Settlement;

namespace Ae.FMS.Service.Core.Interfaces
{
    /// <summary>
    /// 结算服务
    /// </summary>
    public interface ISettlementService
    {
        /// <summary>
        /// 门店账户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetAccountInfoResponse> GetAccountInfo(GetAccountInfoRequest request);

        /// <summary>
        /// 得到开始提现申请的信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetWithdrawalApplyResponse> GetWithdrawalApply(GetWithdrawalApplyRequest request);

        /// <summary>
        /// 得到提现记录列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<GetWithdrawalRecordListResponse>> GetWithdrawalRecordList(
            GetWithdrawalRecordListRequest request);

        /// <summary>
        /// 得到订单提现记录列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<GetWithdrawalOrderRecordListResponse>> GetWithdrawalOrderRecordList(GetWithdrawalOrderRecordListRequest request);

        /// <summary>
        /// 得到财务账单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<GetFianceReconciliationStatusListResponse>> GetFianceReconciliationStatusList(
            GetFianceReconciliationStatusListRequest request);

        /// <summary>
        /// 得到财务账单详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetFianceBillDetailResponse> GetFianceBillDetail(GetFianceBillDetailRequest request);

        /// <summary>
        /// 发起提现申请
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<SubmitWithdrawalApplyResponse>> SubmitWithdrawalApply(
            SubmitWithdrawalApplyRequest request);

        /// <summary>
        /// 得到结算单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<GetSettlementListResponse>> GetSettlementList(GetSettlementListRequest request);

        /// <summary>
        /// 保存结算处理的结果
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<long> SaveSettlementReview(SaveSettlementReviewRequest request);

        /// <summary>
        /// 获得结算单明细
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<GetSettlementDetailResponse>> GetSettlementDetail(GetSettlementDetailRequest request);

        /// <summary>
        /// 得到AccountCheckInfo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AccountCheckDTO> GetAccountCheckInfo(GetFianceBillDetailRequest request);


        Task<SettlementPayReviewDTO> SettlementPayReviewDO(SettlementPayReviewDTO request);

        Task<ApiPagedResult<PurchaseSettlementBatchDTO>> GetPurchaseSettlementList(GetPurchaseSettlementListRequest request);

        Task<ApiResult<List<PurchaseSettlementDetailDTO>>> GetPurchaseSettlementDetail(GetPurchaseSettlementDetailRequest request);

        Task<ApiResult<string>> SavePurchaseSettlementReview(ApiRequest<SavePurchaseSettlementReviewRequest> request);

        Task<ApiResult<string>> SavePurchaseSettlementOrder(ApiRequest<SavePurchaseSettlementOrderRequest> request);

    }
}
