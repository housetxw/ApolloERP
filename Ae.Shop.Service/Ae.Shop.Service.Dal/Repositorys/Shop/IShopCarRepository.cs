using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys.Shop
{
    public interface IShopCarRepository : IRepository<ShopCarDO>
    {
        /// <summary>
        /// 查询门店车辆列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedEntity<ShopCarDO>> GetShopCarListByShopId(GetShopCarPageListRequest request);

        /// <summary>
        /// 修改门店车辆信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> ModifyShopCar(ShopCarDO model);
        /// <summary>
        /// 修改门店车辆状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<bool> UpdateShopCarStatus(long id, int status);
    }
}
