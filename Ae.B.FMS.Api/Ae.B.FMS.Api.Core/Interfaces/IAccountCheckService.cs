using ApolloErp.Web.WebApi;
using Ae.B.FMS.Api.Core.Model.AccountCheck;
using Ae.B.FMS.Api.Core.Request;
using Ae.B.FMS.Api.Core.Request.AccountCheck;
using Ae.B.FMS.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.FMS.Api.Core.Interfaces
{
    public interface IAccountCheckService
    {
        Task<ApiPagedResult<AccountCheckResponse>> GetShopAccountUnChecks(GetAccountCheckRequest request);

        Task<ApiPagedResult<AccountCheckResponse>> GetShopAccountChecks(GetAccountCheckRequest request);

        Task<string> CreateAccountCheckException(AccountCheckExceptionRequest request);

        Task<List<AccountCheckLogResponse>> GetAccountCheckLogs(AccountCheckLogRequest request);

        Task<string> CreateAccountCheck(CreateAccountCheckRequest request);

        Task<ApiPagedResult<AccountCheckResponse>> GetRgAccountChecks(GetAccountCheckRequest request);

        Task<string> UpdateRgAccountCheckResult(RgAccountCheckConfirmRequest request);

        Task<ApiPagedResult<AccountCheckExceptionCollectResponse>> GetAccountCheckExceptionCollectList(GetAccountCheckRequest request);

        Task<ApiPagedResult<AccountCheckCollectResponse>> GetAccountCheckCollects(GetAccountCheckCollectRequest request);

        Task<string> RgAccountCheckWithdraw(RgAccountCheckWithdrawRequeset request);

        Task<string> AccountCheckExceptionHandle(AccountCheckExceptionHandleRequest request);

        Task<ApiPagedResult<AccountCheckExceptionCollectResponse>> GetAccountCheckExceptionMonitorList(GetAccountCheckRequest request);

        /// <summary>
        /// 得到采购对账单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        Task<ApiPagedResult<PurchaseAccountCheckDTO>> GetPurchaseAccountList(GetPurchaseAccountListRequest request);


        /// <summary>
        /// 生成采购已对账的记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        Task<ApiResult<string>> SavePurchaseAccountRecord(ApiRequest<SavePurchaseAccountRecordRequest> request);


        /// <summary>
        /// 生成采购异常对账的记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        Task<ApiResult<string>> SavePurchaseExceptionAccount(ApiRequest<SavePurchaseExceptionAccountRequest> request);



    }
}
