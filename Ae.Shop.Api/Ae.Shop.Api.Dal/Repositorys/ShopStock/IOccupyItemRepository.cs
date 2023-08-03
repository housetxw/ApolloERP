using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys
{
    public interface IOccupyItemRepository : IRepository<OccupyItemDO>
    {
        Task<int> ReleaseOccupyItemStatus(OccupyItemDO request);

        Task<int> UpdateOccupyItemValid(OccupyItemDO request);
    }
}
