using ApolloErp.Data.DapperExtensions;
using Ae.FMS.Service.Core.Model.AccountCheck;
using Ae.FMS.Service.Core.Request.AccountCheck;
using Ae.FMS.Service.Dal.Model.AccountCheck;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.FMS.Service.Dal.Repositorys
{
    public interface IPurchaseAccountCheckRepository : IRepository<PurchaseAccountCheckDO>
    {
        Task<PagedEntity<PurchaseAccountCheckDO>> GetPurchaseAccountList(GetPurchaseAccountListRequest request);

        Task<bool> UpdatePurchaseAccountStatusForHaveSettlement(long locationId, string startDate, string endDate,string updateBy);
       
    }
}
