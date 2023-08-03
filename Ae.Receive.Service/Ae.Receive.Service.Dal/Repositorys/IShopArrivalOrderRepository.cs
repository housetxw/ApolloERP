using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Core.Request.Arrival;
using Ae.Receive.Service.Dal.Model;

namespace Ae.Receive.Service.Dal.Repositorys
{
    /// <summary>
    /// 
    /// </summary>
    public interface IShopArrivalOrderRepository : IRepository<ShopArrivalOrderDO>
    {
        /// <summary>
        /// 根据到店记录Id查询关联订单
        /// </summary>
        /// <param name="recIds"></param>
        /// <returns></returns>
        Task<List<ShopArrivalOrderDO>> GetReceiveOrderByRecIds(List<long> recIds);

        /// <summary>
        /// 得到到店记录订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ShopArrivalOrderDO>> GetShopArrivalOrder(GetShopArrivalOrderRequest request);

    }
}
