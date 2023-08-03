using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.FMS.Service.Core.Request;
using Ae.FMS.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.FMS.Service.Dal.Repositorys
{
    public interface IAccountCheckRepository
    {
        Task<int> CreateAccountCheckLog( AccountCheckLogDO request);
        Task<int> CreateAccountCheckException(AccountCheckExceptionDO request);

        Task<int> CreateAccountCheck(AccountCheckDO request);
        Task<int> DeleteAccountCheck(ShopAccountCheckConfirmRequest request);

        Task<List<AccountCheckLogDO>> GetAccountCheckLogs(AccountCheckLogDO request);

        Task<PagedEntity<AccountCheckDO>> GetAccountChecks(GetAccountCheckRequest request);

        Task<int> RgAccountCheckConfirm(RgAccountCheckConfirmRequest request);

        /// <summary>
        /// 总部核对订单异常
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<int> RgAccountCheckException(RgAccountCheckConfirmRequest request);

        /// <summary>
        /// 门店核对订单异常
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<int> ShopAccountCheckException(ShopAccountCheckConfirmRequest request);

        Task<PagedEntity<AccountCheckDO>> GetShopAccountChecks(GetAccountCheckRequest request);

        Task<PagedEntity<AccountCheckDO>> GetShopAccountChecksList(GetAccountCheckRequest request);

        Task<PagedEntity<AccountCheckExceptionCollectDO>> GetAccountCheckExceptionCollectList(GetAccountCheckRequest request);

        Task<PagedEntity<AccountCheckExceptionCollectDO>> GetAccountCheckExceptionMonitorList(GetAccountCheckRequest request);

        Task<List<AccountCheckCollectDO>> GetAccountCheckCountByStatus(List<long> locationIds,int requestType);

        Task<List<AccountCheckCollectDO>> GetAccountCheckExceptionCount(List<long> locationIds);

        Task<int> RgAccountCheckWithdraw(RgAccountCheckWithdrawRequeset request);

        Task<int> AccountCheckExceptionHandle(AccountCheckExceptionHandleRequest request);

        Task<AccountCheckDO> GetAccountCheckDetail(GetAccountCheckDetailRequest request);

        Task<int> UpdateAccountCheckInfo(UpdateAccountCheckRequest request);

        Task<int> RgAccountCheckSettlement(RgAccountCheckWithdrawRequeset request);

        Task<string> BatchHandelAccountCheckData(int hour);

    }
}
