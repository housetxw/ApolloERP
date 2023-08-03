using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Web.WebApi;
using Ae.FMS.Service.Core.Model.AccountCheck;
using Ae.FMS.Service.Core.Request.AccountCheck;
using Ae.FMS.Service.Dal.Model.AccountCheck;

namespace Ae.FMS.Service.Dal.Repositorys
{
    public interface IReconciliationFeeRepository : IRepository<ReconciliationFeeDO>
    {
        Task<List<ReconciliationFeeDO>> GetReconciliationFees(GetReconciliationFeesRequest request);

        Task<List<GetNoReconciliationAmountDO>> GetNoReconciliationAmount(GetNoReconciliationAmountRequest request);
        Task<int> DeleteReconciliationFee(CalculationReconciliationFeeRequest request);
    }
}
