using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys.Test
{
    public interface ITestRespository : IRepository<PurchaseInfo>
    {
        Task<PagedEntity<PurchaseInfo>> PurchaseInfo(int pageIndex, int pageSize, int shopId);
    }
}
