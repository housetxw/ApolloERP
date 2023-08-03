using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.FMS.Service.Core.Model;
using Ae.FMS.Service.Core.Model.AccountCheck;
using Ae.FMS.Service.Core.Request;
using Ae.FMS.Service.Core.Request.AccountCheck;

namespace Ae.FMS.Service.Core.Interfaces
{
   public interface IAccountCheckService
    {
        Task<int> CreateAccountCheckException(AccountCheckExceptionDTO request);

        Task<int> CreateAccountCheck(CreateAccountCheckRequest request);
        Task<int> CreateAccountCheckLog(CreateAccountCheckLogRequest request);

        Task<List<AccountCheckLogDTO>> GetAccountCheckLogs(GetAccountCheckLogRequest request);

        Task<ApiPagedResultData<AccountCheckDTO>> GeShoptAccountChecks(GetAccountCheckRequest request);

        Task<ApiPagedResultData<AccountCheckDTO>> GeShoptAccountChecksList(GetAccountCheckRequest request);

        Task<ApiPagedResultData<AccountCheckDTO>> GeRgAccountChecks(GetAccountCheckRequest request);
        Task<ApiPagedResultData<AccountCheckExceptionCollectDTO>> GetAccountCheckExceptionCollectList(GetAccountCheckRequest request);

        Task<ApiPagedResultData<AccountCheckExceptionCollectDTO>> GetAccountCheckExceptionMonitorList(GetAccountCheckRequest request);


        Task<ApiPagedResultData<AccountCheckDTO>> GetShopAccountUnChecks(GetAccountCheckRequest request);

        Task<int> UpdateRgAccountCheckResult(RgAccountCheckConfirmRequest request);

         Task<ApiPagedResult<AccountCheckCollectDTO>> GetAccountCheckCollects(AccountCheckCollectRequest request);

        /// <summary>
        /// 申请提现
        /// </summary>
        /// <param name="requeset"></param>
        /// <returns></returns>
        Task<int> RgAccountCheckWithdraw(RgAccountCheckWithdrawRequeset request);

        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="requeset"></param>
        /// <returns></returns>
        Task<int> RgAccountCheckSettlement(RgAccountCheckWithdrawRequeset requeset);

        Task<int> AccountCheckExceptionHandle(AccountCheckExceptionHandleRequest request);

        Task<AccountCheckDTO> GetAccountCheckDetail(GetAccountCheckDetailRequest request);

        Task<string> BatchHandelAccountCheckData(int hour);

        Task<ApiResult> CalculationReconciliationFee(
            CalculationReconciliationFeeRequest request);

        Task<ApiResult<List<ReconciliationFeeDTO>>> GetReconciliationFees(
            GetReconciliationFeesRequest request);

        Task<ApiResult<List<GetNoReconciliationAmountDO>>> GetNoReconciliationAmount(GetNoReconciliationAmountRequest request);

        Task<ApiResult> CalculationPurchaseReconciliationFee(ApiRequest<CalculationPurchaseReconciliationFeeRequest> request);

        Task<ApiPagedResult<PurchaseAccountCheckDTO>> GetPurchaseAccountList(GetPurchaseAccountListRequest request);

        Task<ApiResult<string>> SavePurchaseAccountRecord(ApiRequest<SavePurchaseAccountRecordRequest> request);

        Task<ApiResult<string>> SavePurchaseExceptionAccount(ApiRequest<SavePurchaseExceptionAccountRequest> request);

        Task<ApiResult<string>> DeleteAccountCheck(CalculationReconciliationFeeRequest request);

    }
}
