using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys.Shop
{
    public interface IShopServiceAreaRepository : IRepository<ShopServiceAreaDO>
    {
        /// <summary>
        /// 门店服务区域配置
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Task<List<ShopServiceAreaDO>> GetShopServiceAreaByShopId(long shopId, bool readOnly = true);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        Task<bool> DeleteShopServiceArea(List<long> idList, string updateBy);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="type"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        Task<bool> DeleteShopServiceAreaByShopId(long shopId, int type, string updateBy);

        /// <summary>
        /// shopId批量查询
        /// </summary>
        /// <param name="shopIds"></param>
        /// <returns></returns>
        Task<List<ShopServiceAreaDO>> GetShopServiceAreaByShopIds(List<long> shopIds);
    }
}
