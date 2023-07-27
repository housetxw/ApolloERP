using ApolloErp.Data.DapperExtensions;
using Ae.BasicData.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.BasicData.Service.Dal.Repositorys.IDAL
{
    public interface ITestRespository
    {
        Task<PagedEntity<PurchaseInfo>> PurchaseInfo(int pageIndex, int pageSize, int shopId);
    }
}
