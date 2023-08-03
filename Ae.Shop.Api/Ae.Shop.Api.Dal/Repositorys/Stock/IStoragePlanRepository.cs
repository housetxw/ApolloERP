using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys
{
    public interface IStoragePlanRepository : IRepository<StoragePlanDO>
    {
        Task<int> UpdateStoragePlanStatus(StoragePlanDO request);
    }
}
