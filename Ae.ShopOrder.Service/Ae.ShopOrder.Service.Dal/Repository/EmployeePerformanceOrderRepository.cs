using Dapper;
using System.Linq;
using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Core.Model;
using Ae.ShopOrder.Service.Core.Request;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Core.Response.Order;
using Ae.ShopOrder.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Dal.Repository
{
    public class EmployeePerformanceOrderRepository : AbstractRepository<EmployeePerformanceOrderDO>, IEmployeePerformanceOrderRepository
    {
        public EmployeePerformanceOrderRepository()
        {
            SetDbType(ApolloErp.Data.DapperExtensions.DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopOrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopOrderSqlReadOnly");
        }

        /// <summary>
        /// 获取员工绩效列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<EmployeePerformanceOrderDO>> GetEmployeePerformanceOrderList(EmployeePerformanceRequest request)
        {
            var condition = new StringBuilder();
            var parameters = new DynamicParameters();

            condition.AppendLine("where   is_deleted=0  and create_time >=@startDate and create_time <= @endDate ");
            parameters.Add("@startDate", request.StartDate.ToString("yyyy-MM-dd"));
            parameters.Add("@endDate", request.EndDate.ToString("yyyy-MM-dd"));

            if (request.ShopId > 0)
            {
                condition.AppendLine(" and shop_id = @ShopId");
                parameters.Add("@ShopId", request.ShopId);
            }

            if (request.ShopIds != null && request.ShopIds.Any())
            {
                condition.AppendLine(" and shop_id in @ShopIds");
                parameters.Add("@ShopIds", request.ShopIds);
            }

            if (!string.IsNullOrEmpty(request.OrderNo))
            {
                condition.AppendLine(" and order_no = @OrderNo");
                parameters.Add("@OrderNo", request.OrderNo);
            }

            var key = request.Key?.Trim();
            if (!string.IsNullOrEmpty(key))
            {
                condition.AppendLine(" and ( employee_name like @employeeName or employee_phone = @mobile) ");
                parameters.Add("@employeeName", '%' + key + '%');
                parameters.Add("@mobile", key);
            }

            var productName = request.ProductName?.Trim();
            if (!string.IsNullOrEmpty(productName))
            {
                condition.AppendLine(" and ( product_name like @productName or product_id = @productId) ");
                parameters.Add("@productName", '%' + productName + '%');
                parameters.Add("@productId", productName);
            }

            if (request.TabType > 0)
            {
                condition.AppendLine(" and tab_type = @TabType");
                parameters.Add("@TabType", request.TabType);
            }

            if (request.OrderType > -1)
            {
                condition.AppendLine(" and order_type = @OrderType");
                parameters.Add("@OrderType", request.OrderType);
            }

            if (request.ProduceType > -1)
            {
                condition.AppendLine(" and produce_type = @ProduceType");
                parameters.Add("@ProduceType", request.ProduceType);
            }

            var list = await GetListAsync(condition.ToString(), parameters, false);
            return list?.ToList();
        }

        public async Task<int> DeleteShopPerformanceConfig(BatchUpdateCompleteStatusRequest request)
        {
            var condition = new StringBuilder();
            var parameters = new DynamicParameters();

            parameters.Add("@updateBy", request.UpdateBy);

            if (request.ShopId > 0)
            {
                condition.AppendLine(" and shop_id = @ShopId");
                parameters.Add("@ShopId", request.ShopId);
            }

            //if (request.OrderNo?.Count > 0) 安全期间，防止全删
            {
                condition.AppendLine(" and order_no in @OrderNos");
                parameters.Add("@OrderNos", request.OrderNo);
            }

            var sql = @"UPDATE employee_performance_order 
	                SET update_by = @updateBy,
	                update_time = SYSDATE( ),
                    is_deleted =1 
                WHERE is_deleted =0 " + condition.ToString();

            int result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, parameters));
            return result;
        }

    }
}
