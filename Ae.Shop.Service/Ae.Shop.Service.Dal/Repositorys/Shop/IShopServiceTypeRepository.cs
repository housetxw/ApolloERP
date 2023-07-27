using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys.Shop
{
    public interface IShopServiceTypeRepository: IRepository<ShopServiceTypeDO>
    {
        Task<List<ShopServiceTypeDO>> GetShopServiceTypeListAsync(long shopId);

        Task<PagedEntity<ShopServiceTypeDO>> GetShopServiceTypePagesAsync(GetShopServiceTypePageRequest request);

        Task<int> DeleteShopServiceType(ShopServiceTypeDO request);

        Task<int> CreateShopServiceType(ShopServiceTypeDO request);
        Task<List<ShopServiceTypeDO>> GetShopServiceTypeListByIdsAsync(List<long> shopIds);

        Task<List<ShopServiceTypeDO>> GetServiceType(List<long> shopId, string serviceType);
    }
}
