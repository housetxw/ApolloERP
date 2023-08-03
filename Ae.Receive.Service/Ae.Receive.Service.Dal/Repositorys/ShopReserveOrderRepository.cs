using System;
using System.Collections.Generic;
using System.Text;
using Ae.Receive.Service.Dal.Model;
using Dapper;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using DynamicParameters = Dapper.DynamicParameters;
using System.Linq;
using Ae.Receive.Service.Core.Request;

namespace Ae.Receive.Service.Dal.Repositorys
{
    public class ShopReserveOrderRepository : AbstractRepository<ShopReserveOrderDO>, IShopReserveOrderRepository
    {
        public ShopReserveOrderRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSqlReadOnly");
        }

        /// <summary>
        /// 根据预约Id查询关联订单
        /// </summary>
        /// <param name="reserveId"></param>
        /// <returns></returns>
        public async Task<List<ShopReserveOrderDO>> GetReserveOrderByReserveId(long reserveId)
        {
            var para = new DynamicParameters();
            para.Add("@reserveId", reserveId);
            var result = await GetListAsync<ShopReserveOrderDO>("WHERE reserve_id = @reserveId", para);
            return result?.ToList() ?? new List<ShopReserveOrderDO>();
        }

        /// <summary>
        /// 根据订单号查询关联订单
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public async Task<List<ShopReserveOrderDO>> GetReserveOrderByOrderNo(string orderNo)
        {
            var para = new DynamicParameters();

            para.Add("@orderNo", orderNo);

            var result = await GetListAsync<ShopReserveOrderDO>("WHERE order_no = @orderNo", para);

            return result?.ToList() ?? new List<ShopReserveOrderDO>();
        }

        /// <summary>
        /// 根据预约id或订单号查询预约关联订单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ShopReserveOrderDO>> GetReserveInfoByReserveIdOrOrderNum(GetReserveInfoByReserveIdOrOrderNum request)
        {
            StringBuilder condition = new StringBuilder();
            if (request.ReserveIds != null && request.ReserveIds.Any()) 
            {
                condition.Append("WHERE reserve_id in @ReserveIds");
                if (request.OrderNumbers != null && request.OrderNumbers.Any())
                {
                    condition.Append(" AND order_no in @OrderNumbers");
                }
            }
            if (request.OrderNumbers != null && request.OrderNumbers.Any() && (request.ReserveIds == null || !request.ReserveIds.Any()))
            {
                condition.Append("WHERE order_no in @OrderNumbers");
            }
            var para = new DynamicParameters();
            para.Add("@ReserveIds", request.ReserveIds);
            para.Add("@OrderNumbers", request.OrderNumbers);

            var result = await GetListAsync<ShopReserveOrderDO>(condition.ToString(), para);

            return result?.ToList() ?? new List<ShopReserveOrderDO>();
        }

        
    }
}
