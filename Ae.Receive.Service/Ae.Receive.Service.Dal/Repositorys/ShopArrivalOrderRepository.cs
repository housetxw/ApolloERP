using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Core.Request.Arrival;
using Ae.Receive.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Dal.Repositorys
{

    public class ShopArrivalOrderRepository : AbstractRepository<ShopArrivalOrderDO>, IShopArrivalOrderRepository
    {
        public ShopArrivalOrderRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSqlReadOnly");
        }

        /// <summary>
        /// 根据到店记录Id查询关联订单
        /// </summary>
        /// <param name="recIds"></param>
        /// <returns></returns>
        public async Task<List<ShopArrivalOrderDO>> GetReceiveOrderByRecIds(List<long> recIds)
        {
            var para = new DynamicParameters();
            para.Add("@recIds", recIds);

            var result = await GetListAsync<ShopArrivalOrderDO>("WHERE is_deleted = 0 and arrival_id IN @recIds", para);

            return result?.ToList() ?? new List<ShopArrivalOrderDO>();
        }

        /// <summary>
        /// 得到到店记录订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ShopArrivalOrderDO>> GetShopArrivalOrder(GetShopArrivalOrderRequest request)
        {
            var para = new DynamicParameters();
            para.Add("@OrderNos", request.OrderNos);

            var result = await GetListAsync<ShopArrivalOrderDO>("WHERE is_deleted = 0 and order_no IN @OrderNos", para);

            return result?.ToList() ?? new List<ShopArrivalOrderDO>();
        }
    }
}
