using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Model.Fiance;
using Ae.Shop.Api.Core.Request.Fiance;
using Ae.Shop.Api.Core.Response.Fiance;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Core.Interfaces
{
    public interface IFianceService
    {
        Task<ApiResult> CreateAccountCheck(CreateAccountCheckClientRequest request);

        /// <summary>
        /// 查询门店未对账列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResult<AccountCheckDTO>> GetShopAccountUnChecks(GetAccountCheckRequest request);

        /// <summary>
        /// 门店已对账列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResult<AccountCheckDTO>> GetShopAccountChecks(GetAccountCheckRequest request);

        /// <summary>
        /// 已对账列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResult<AccountCheckDTO>> GetRgAccountChecks(GetAccountCheckRequest request);

        /// <summary>
        /// 异常订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResult<AccountCheckExceptionCollectDTO>> GetAccountCheckExceptionCollectList(GetAccountCheckRequest request);

        /// <summary>
        /// 查询日志
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<AccountCheckLogDTO>>> GetAccountCheckLogs(GetAccountCheckLogRequest request);

        /// <summary>
        /// 得到提现记录列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResult<GetWithdrawalRecordListResponse>> GetWithdrawalRecordList(
            GetWithdrawalRecordListRequest request);

        /// <summary>
        /// 得到开始提现申请的信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<GetWithdrawalApplyResponse>> GetWithdrawalApply(GetWithdrawalApplyRequest request);

        /// <summary>
        /// 结算单详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResult<GetWithdrawalOrderRecordListResponse>> GetWithdrawalOrderRecordList(GetWithdrawalOrderRecordListRequest request);

        /// <summary>
        /// 发起提现申请
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<SubmitWithdrawalApplyResponse>> SubmitWithdrawalApply(SubmitWithdrawalApplyRequest request);

        Task<ApiResult> CalculationReconciliationFee(
             CalculationReconciliationFeeRequest request);

        Task<ApiResult> CalculationReconciliationFeeBatch(
             CreateAccountCheckClientRequest request);
    }
}
