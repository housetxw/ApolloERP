using ApolloErp.Data.DapperExtensions;
using Ae.FMS.Service.Core.Request.Settlement;
using Ae.FMS.Service.Dal.Model.AccountCheck;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.FMS.Service.Dal.Repositorys
{
    public interface IPurchaseSettlementBatchRepository : IRepository<PurchaseSettlementBatchDO>
    {
        Task<PagedEntity<PurchaseSettlementBatchDO>> GetPurchaseSettlementList(GetPurchaseSettlementListRequest request);
    }
}
