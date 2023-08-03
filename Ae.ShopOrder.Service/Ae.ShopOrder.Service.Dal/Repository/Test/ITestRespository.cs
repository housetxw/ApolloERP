using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Dal.Repositorys.Test
{
    public interface ITestRespository
    {
        Task<PagedEntity<PurchaseInfo>> PurchaseInfo(int pageIndex, int pageSize, int shopId);
    }
}
