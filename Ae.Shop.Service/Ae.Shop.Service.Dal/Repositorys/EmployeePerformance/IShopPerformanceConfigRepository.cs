using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys
{
    public interface IShopPerformanceConfigRepository : IRepository<ShopPerformanceConfigDO>
    {
        Task<List<ShopPerformanceConfigDO>> GetShopPerformanceConfig(GetBasicPerformanceConfigRequest request);

        Task<int> UpdateShopPerformanceConfig(ShopPerformanceConfigDO request);

        Task<int> DeleteShopPerformanceConfig(ShopPerformanceConfigDO request);
    }
}
