using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Dal.Model;
using Ae.Shop.Api.Dal.Model.WMS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys
{
    public interface IAllotTaskRepository : IRepository<AllotTaskDO>
    {
        Task<int> CreateWmsLog(WmsLogDO request);
        Task<PagedEntity<AllotTaskDO>> GetAllotPageData(AllotPageRequest request);

        Task<int> UpdateAllotTaskNo(AllotTaskDO request);

        Task<int> AuditAllotTask(AllotTaskDO request);

        Task<int> UpdateAllotTask(AllotTaskDO request);

        Task<int> UpdateAllotTaskStatus(AllotTaskDO request);

    }
}
