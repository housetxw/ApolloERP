using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys.Shop
{
    public interface IShopServiceRepository : IRepository<ShopServiceDO>
    {
        Task<List<ShopServiceDO>> GetShopServiceListWithPID(long shopId, List<string> productIds);

        /// <summary>
        /// 获取门店所有上架的服务
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<List<ShopServiceDO>> GetAllOnLineServicesByShopId(long shopId);

        Task<List<ShopServiceInfoVO>> GetShopServiceListAsync(long shopId, long categoryId);
        Task<bool> AddShopServiceAsync(AddShopServiceRequest request);
        Task<List<BaseServiceDO>> GetShopServiceCategory(long shopId);
        Task<List<ShopCategoryServiceDO>> GetServiceCategoryWithShops(List<long> shopIds);

        Task<List<ShopServiceBrandDO>> GetShopServiceBrands(List<long> shopIds);

        Task<int> CreateShopServiceBrand(ShopServiceBrandDO request);
        Task<int> DeleteShopServiceBrand(ShopServiceBrandDO request);


    }
}
