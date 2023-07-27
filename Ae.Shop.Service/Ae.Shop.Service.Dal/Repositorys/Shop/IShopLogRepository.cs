using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys.Shop
{
    public interface IShopLogRepository : IRepository<ShopLogDO>
    {
        Task<List<ShopLogDTO>> GetShopLog(long ShopId);

    }
}
