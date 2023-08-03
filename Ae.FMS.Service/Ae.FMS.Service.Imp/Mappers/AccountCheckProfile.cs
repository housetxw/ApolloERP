using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Ae.FMS.Service.Core.Model;
using Ae.FMS.Service.Dal.Model;
using Ae.FMS.Service.Core.Request;
using ApolloErp.Web.WebApi;
using ApolloErp.Data.DapperExtensions;
using Ae.FMS.Service.Core.Model.AccountCheck;
using Ae.FMS.Service.Dal.Model.AccountCheck;

namespace Ae.FMS.Service.Imp.Mappers
{
    public class AccountCheckProfile : Profile
    {
        public AccountCheckProfile()
        {
            CreateMap<AccountCheckDTO, AccountCheckDO>();

            CreateMap<CreateAccountCheckDTO, AccountCheckDO>();

            CreateMap<AccountCheckExceptionDTO, AccountCheckExceptionDO>();

            CreateMap<AccountCheckLogDO, AccountCheckLogDTO>();

            CreateMap<AccountCheckLogDTO, AccountCheckLogDO>();

            CreateMap<AccountCheckDO, AccountCheckDTO>();

            CreateMap<PagedEntity<AccountCheckDO>, ApiPagedResultData<AccountCheckDTO>>();
            CreateMap<AccountCheckExceptionCollectDO, AccountCheckExceptionCollectDTO>();
            CreateMap<PagedEntity<AccountCheckExceptionCollectDO>, ApiPagedResultData<AccountCheckExceptionCollectDTO>>();

            CreateMap<AccountCheckDO, UpdateAccountCheckRequest>();
            CreateMap<CreateAccountCheckLogRequest, AccountCheckLogDO>();
            CreateMap<ReconciliationFeeDO, ReconciliationFeeDTO
            >();


            CreateMap<PurchaseSettlementBatchDO, PurchaseSettlementBatchDTO>();

            CreateMap<PagedEntity<PurchaseSettlementBatchDO>, ApiPagedResultData<PurchaseSettlementBatchDTO>>();

            CreateMap<PurchaseAccountCheckDO, PurchaseAccountCheckDTO>();

            CreateMap<PagedEntity<PurchaseAccountCheckDO>, ApiPagedResultData<PurchaseAccountCheckDTO>>();
      

        }
    }
}
