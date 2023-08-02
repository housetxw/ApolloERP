using Ae.B.FMS.Api.Client.Request;
using Ae.B.FMS.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.B.FMS.Api.Core.Model.AccountCheck;
using Ae.B.FMS.Api.Core.Request.AccountCheck;

namespace Ae.B.FMS.Api.Client.Interface
{
   public interface IAccountCheckClient
    {
        Task<ApiPagedResult<AccountCheckDTO>> GetShopAccountUnChecks(GetAccountCheckClientRequest request);

        Task<ApiPagedResult<AccountCheckDTO>> GetShopAccountChecks(GetAccountCheckClientRequest request);

        Task<string> CreateAccountCheckException(AccountCheckExceptionClientRequest request);

        Task<List<AccountCheckLogDTO>> GetAccountCheckLogs(AccountCheckLogClientRequest request);

        Task<string> CreateAccountChecks(CreateAccountCheckClientRequest request);

        Task<ApiPagedResult<AccountCheckDTO>> GetRgAccountChecks(GetAccountCheckClientRequest request);

        Task<string> UpdateRgAccountCheckResult(RgAccountCheckConfirmClientRequest request);

        Task<ApiPagedResult<AccountCheckExceptionCollectClientDTO>> GetAccountCheckExceptionCollectList(GetAccountCheckClientRequest request);

        Task<string> RgAccountCheckWithdraw(RgAccountCheckWithdrawClientRequeset request);

        Task<ApiPagedResult<AccountCheckCollectDTO>> GetAccountCheckCollects(GetAccountCheckCollectClientRequest request);

        Task<string> AccountCheckExceptionHandle(AccountCheckExceptionHandleClientRequest request);

        Task<ApiPagedResult<AccountCheckExceptionCollectClientDTO>> GetAccountCheckExceptionMonitorList(GetAccountCheckClientRequest request);

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
