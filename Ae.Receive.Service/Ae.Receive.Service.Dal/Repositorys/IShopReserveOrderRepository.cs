using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Core.Request;
using Ae.Receive.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Dal.Repositorys
{
    public interface IShopReserveOrderRepository : IRepository<ShopReserveOrderDO>
    {
        /// <summary>
        /// 根据预约Id查询关联订单
        /// </summary>
        /// <param name="reserveId"></param>
        /// <returns></returns>
        Task<List<ShopReserveOrderDO>> GetReserveOrderByReserveId(long reserveId);

        /// <summary>
        /// 根据订单号查询关联订单
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        Task<List<ShopReserveOrderDO>> GetReserveOrderByOrderNo(string orderNo);

        /// <summary>
        /// 根据预约id或订单号查询预约关联订单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ShopReserveOrderDO>> GetReserveInfoByReserveIdOrOrderNum(GetReserveInfoByReserveIdOrOrderNum request);
    }
}
