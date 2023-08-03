using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys
{
    public interface IShopAssetTypeRepository : IRepository<ShopAssetTypeDO>
    {
        /// <summary>
        /// 根据ID批量查询资产类别
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        Task<List<ShopAssetTypeDO>> GetShopAssetTypesByValues(List<string> values);
    }
}
