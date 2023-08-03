using RedGoose.Data.DapperExtensions;
using Rg.C.MiniApp.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rg.C.MiniApp.Api.Dal.Repositorys.Test
{
    public interface ITestRespository
    {
        Task<PagedEntity<PurchaseInfo>> PurchaseInfo(int pageIndex, int pageSize, int shopId);
    }
}
