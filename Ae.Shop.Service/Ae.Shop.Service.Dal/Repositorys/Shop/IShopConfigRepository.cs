using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Dal.Model;
using System.Threading.Tasks;
using Ae.Shop.Service.Core.Request;

namespace Ae.Shop.Service.Dal.Repositorys.Shop
{
    public interface IShopConfigRepository
    {
        /// <summary>
        /// 新增门店配置信息
        /// </summary>
        /// <param name="shopConfig"></param>
        /// <returns></returns>
        Task<long> AddAsync(ShopConfigDO shopConfig);

        /// <summary>
        /// 根据门店ID查询门店配置信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<ShopConfigDO> GetShopConfigByShopIdAsync(long shopId);
        Task<bool> ModifyShopConfigInfoAsync(ModifyShopConfigInfoRequest request);
        Task<bool> ModifyShopConfigExpenseAsync(ModifyShopConfigExpenseRequest request);

        Task<int> ModifyShopSuspendTime(ModifyShopSuspendTimeRequest request);

    }
}
