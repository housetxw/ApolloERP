using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Dal.Model;

namespace Ae.ShopOrder.Service.Dal.Repository
{
    /// <summary>
    /// 订单仓储
    /// </summary>
    public class OrderNotAdapterRepository : AbstractRepository<OrderNotAdapterDO>, IOrderNotAdapterRepository
    {
        public OrderNotAdapterRepository()
        {
            SetDbType(ApolloErp.Data.DapperExtensions.DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopOrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopOrderSqlReadOnly");
        }
        /// <summary>
        /// 得到不适配订单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<OrderNotAdapterDO> GetOrderNotAdapter(GetOrderNotAdapterRequest request)
        {
            var condition = new StringBuilder();
            var parameters = new DynamicParameters();
            condition.AppendLine(" where  shop_id=@ShopId");
            parameters.Add("@ShopId", request.ShopId);

            condition.AppendLine(" and  is_deleted=0");

            condition.AppendLine(" and order_no=@OrderNo");
            parameters.Add("@OrderNo", request.OrderNo);

            var getListAsync = await GetListAsync<OrderNotAdapterDO>(condition.ToString(), parameters, false);

            return getListAsync?.FirstOrDefault();
        }

        /// <summary>
        /// 保存不适配订单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<long> SaveOrderNotAdapter(OrderNotAdapterDO request)
        {
            long id = 0;
            try
            {
                string sql = "Update order_not_adapter set is_deleted=1,update_by=@UpdateBy,update_time=NOW(3) where order_no=@OrderNo";
                var parameters = new DynamicParameters();
                parameters.Add("@UpdateBy", request.CreateBy);
                parameters.Add("@OrderNo",request.OrderNo.Trim());
                await OpenSlaveConnectionAsync(async conn => { await conn.ExecuteAsync(sql, parameters); });
                id = await InsertAsync<long>(request);
            }
            catch (Exception ex)
            {

            }

            return id;
        }

    }
}
