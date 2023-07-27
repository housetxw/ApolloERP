using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys.Shop
{
    public interface IBaseServiceRepository : IRepository<BaseServiceDO>
    {
        Task<BaseServiceDO> GetBaseServiceByNameAsync(string name, string code);
        Task<ShopServiceInfoVO> GetShopServiceInfoAsync(GetShopServiceInfoRequest request); 
        Task<List<BaseServiceDO>> GetBaseServiceListAsync(GetBaseServiceListRequest request);
    }
}
