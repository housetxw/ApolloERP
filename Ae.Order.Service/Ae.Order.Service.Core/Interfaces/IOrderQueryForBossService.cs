using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.Order.Service.Core.Request;
using Ae.Order.Service.Core.Response;

namespace Ae.Order.Service.Core.Interfaces
{
    public interface IOrderQueryForBossService
    {
        Task<ApiPagedResult<GetOrderListForBossResponse>> GetOrderListForBoss(GetOrderListForBossRequest request);

        Task<ApiPagedResult<GetPackageCardRecordsResponse>> GetPackageCardRecords(GetPackageCardRecordsRequest request);
    }
}
