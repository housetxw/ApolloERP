using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys.Shop
{
    public interface IShopPointDetailRepository : IRepository<ShopPointDetailDo>
    {
        /// <summary>
        /// 获取门店积分详情
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<List<ShopPointDetailDo>> GetShopPointDetailByShopId(int shopId);
    }
}
