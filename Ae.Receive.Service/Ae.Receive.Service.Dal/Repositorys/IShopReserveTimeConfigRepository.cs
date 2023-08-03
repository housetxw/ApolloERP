using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Dal.Repositorys
{
    public interface IShopReserveTimeConfigRepository : IRepository<ShopReserveTimeConfigDO>
    {
        Task<List<ShopReserveTimeConfigDO>> GetReserveTimeConfigByDateAsync(long shopId, int weekDay, string shortDate);

        /// <summary>
        /// 根据门店查 通用配置
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<List<ShopReserveTimeConfigDO>> GetDefaultReserveTimeConfigByShopId(long shopId);

        /// <summary>
        /// 根据门店和时间查自定义配置
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Task<List<ShopReserveTimeConfigDO>> GetDefinedReserveTimeConfigByShopId(long shopId, string startDate,
            string endDate);

        Task<List<ShopReserveTimeConfigDO>> GetReserveTimeConfigByDateTypeAsync(long shopId, int weekDay, string shortDate,int type);
        Task<int> DeleteReserveTimeConfigAsync(long shopId, int weekDay, string shortDate, int type);

        /// <summary>
        /// 根据门店查一段时间配置
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<List<ShopReserveTimeConfigDO>> GetReserveTimeConfigByShopId(long shopId, string startDate, string endDate);
    }
}
