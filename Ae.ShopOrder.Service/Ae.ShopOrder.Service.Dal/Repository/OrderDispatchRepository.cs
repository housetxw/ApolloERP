using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Core.Response.Order;
using Ae.ShopOrder.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Dal.Repository
{
    public class OrderDispatchRepository : AbstractRepository<OrderDispatchDO>, IOrderDispatchRepository
    {
        public OrderDispatchRepository()
        {
            SetDbType(ApolloErp.Data.DapperExtensions.DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopOrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopOrderSqlReadOnly");
        }


        /// <summary>
        ///  得到派工订单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<OrderDispatchDO>> GetOrderDispatch(GetOrderDispatchRequest request)
        {
            var condition = new StringBuilder();
            var parameters = new DynamicParameters();
           

            condition.AppendLine("where   is_deleted=0");

            if (request.ShopId > 0)
            {
                condition.AppendLine(" and shop_id = @ShopId");
                parameters.Add("@ShopId", request.ShopId);
            }

            if (request.OrderNos?.Count > 0)
            {
                condition.AppendLine(" and order_no in @OrderNos");
                parameters.Add("@OrderNos", request.OrderNos);
            }

            if (request.TechIds?.Count > 0)
            {
                condition.AppendLine(" and tech_id in @TechIds");
                parameters.Add("@TechIds", request.TechIds);
            }

            if (!string.IsNullOrWhiteSpace(request.StartTime))
            {

                bool isSuccess = DateTime.TryParse(request.StartTime, out var startTime);
                if (isSuccess)
                {
                    condition.AppendLine(" and create_time>=@StartTime");
                    parameters.Add("@StartTime", startTime);
                }
            }


            if (!string.IsNullOrWhiteSpace(request.EndTime))
            {

                bool isSuccess = DateTime.TryParse(request.EndTime, out var endTime);
                if (isSuccess)
                {
                    condition.AppendLine(" and create_time<=@EndTime");
                    parameters.Add("@EndTime", endTime);
                }
            }

            var getListAsync = await GetListAsync<OrderDispatchDO>(condition.ToString(), parameters, false);

            return getListAsync.AsList();
        }

        /// <summary>
        /// 删除订单派工记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteOrderDispatch(UpdateOrderDispatchRequest request)
        {
            string sql = "Update order_dispatch set is_deleted=1,update_by=@UpdateBy,update_time=NOW(3) where order_no=@OrderNo";
            var parameters = new DynamicParameters();
            parameters.Add("@UpdateBy", request.CreateBy);
            parameters.Add("@OrderNo", request.OrderNo.Trim());
            var result = 0;
            await OpenSlaveConnectionAsync(async conn => { result=await conn.ExecuteAsync(sql, parameters); });
            return result > 0;
        }
    }
}
