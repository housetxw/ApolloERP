using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys.Shop
{
    public interface IShopServiceCategoryRepository : IRepository<ShopServiceCategoryDO>
    {
        /// <summary>
        /// 根据分类ID查询门店开通的服务分类
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Task<List<ShopServiceCategoryDO>> ServiceCategoryByShopId(long shopId, long categoryId);
    }
}
