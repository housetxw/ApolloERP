using AutoMapper;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Web.WebApi;
using Ae.FMS.Service.Core.Model.AccountCheck;
using Ae.FMS.Service.Core.Request.Settlement;
using Ae.FMS.Service.Core.Response;
using Ae.FMS.Service.Core.Response.Settlement;
using Ae.FMS.Service.Dal.Model;
using Ae.FMS.Service.Dal.Model.AccountCheck;
using Ae.FMS.Service.Dal.Model.settlement;

namespace Ae.FMS.Service.Imp.Mappers
{
    /// <summary>
    /// 结算映射
    /// </summary>
    public class SettlementProfile : Profile
    {
        public SettlementProfile()
        {
            CreateMap<AccountSettlementDO, GetAccountInfoResponse>();

            CreateMap<SettlementBatchDO, GetWithdrawalRecordListResponse>();

            CreateMap<PagedEntity<SettlementBatchDO>, ApiPagedResultData<GetWithdrawalRecordListResponse>>();

            CreateMap<SettlementBatchDetailDO, GetWithdrawalOrderRecordListResponse>();

            CreateMap<PagedEntity<SettlementBatchDetailDO>, ApiPagedResultData<GetWithdrawalOrderRecordListResponse>>();

            CreateMap<GetFianceReconciliationStatusListDO,GetFianceReconciliationStatusListResponse>();

            CreateMap<PagedEntity<GetFianceReconciliationStatusListDO>, ApiPagedResultData<GetFianceReconciliationStatusListResponse>>();

            CreateMap<AccountCheckDO, GetFianceBillDetailResponse>();

            CreateMap<SettlementBatchDO, GetSettlementListResponse>();

            CreateMap<PagedEntity<SettlementBatchDO>, ApiPagedResultData<GetSettlementListResponse>>();

            CreateMap<SettlementPayReviewDO, SaveSettlementReviewRequest>();

            CreateMap<SaveSettlementReviewRequest, SettlementPayReviewDO>();

            CreateMap<SettlementPayReviewDO, SettlementPayReviewDTO>();


            CreateMap<SettlementBatchDO, GetSettlementListResponse>();

            CreateMap<PagedEntity<SettlementBatchDO>, ApiPagedResultData<GetSettlementListResponse>>();

            CreateMap<PurchaseSettlementDetailDO, PurchaseSettlementDetailDTO>();

            CreateMap<PurchaseAccountCheckDO, PurchaseSettlementDetailDO>();
        }

    }
}
