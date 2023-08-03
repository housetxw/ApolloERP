using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.FMS.Service.Core.Request.Settlement;
using Ae.FMS.Service.Core.Response.Settlement;
using Ae.FMS.Service.Dal.Model;
using Ae.FMS.Service.Dal.Model.settlement;

namespace Ae.FMS.Service.Dal.Repositorys
{
    /// <summary>
    /// 结算仓库
    /// </summary>
    public interface ISettlementRepository
    {
        /// <summary>
        /// 得到门店结算账户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AccountSettlementDO> GetAccountSettlement(GetAccountInfoRequest request);

        /// <summary>
        /// 待提现的金额
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<decimal> GetAccountBalanceAmount(int shopId);

        /// <summary>
        /// 得到提现记录列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedEntity<SettlementBatchDO>> GetWithdrawalRecordList(GetWithdrawalRecordListRequest request);

        /// <summary>
        /// 得到订单提现记录列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedEntity<SettlementBatchDetailDO>> GetWithdrawalOrderRecordList(GetWithdrawalOrderRecordListRequest request);

        /// <summary>
        /// 得到财务账单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedEntity<GetFianceReconciliationStatusListDO>> GetFianceReconciliationStatusList(
            GetFianceReconciliationStatusListRequest request);

        /// <summary>
        /// 得到财务账单详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AccountCheckDO> GetFianceBillDetail(GetFianceBillDetailRequest request);

        /// <summary>
        /// 发起提现申请
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<long> SubmitWithdrawalApply(SettlementBatchDO settlementBatchDo,List<SettlementBatchDetailDO> settlementBatchDetailDoS);

        /// <summary>
        /// 得到可提现的列表
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<List<AccountCheckDO>> GetAccountWithdrawalList(int shopId);

        /// <summary>
        /// 对账列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedEntity<SettlementBatchDO>> GetSettlementList(GetSettlementListRequest request);

        /// <summary>
        /// 结算处理
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<long> SaveSettlementReview(SettlementPayReviewDO request);

        /// <summary>
        /// 结算单明细
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<GetSettlementDetailResponse>> GetSettlementDetail(GetSettlementDetailRequest request);

        /// <summary>
        /// 得到对账单的状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AccountCheckDO> GetAccountCheckInfo(GetFianceBillDetailRequest request);

        Task<SettlementBatchDO> GetSettlementBatchDetail(string batchNo);

        Task<SettlementPayReviewDO> SettlementPayReviewDO(string batchNo);
    }
}
