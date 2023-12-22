using Dapper;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Log;
using Ae.Shop.Api.Core.Request.ShopReport;
using Ae.Shop.Api.Dal.Model.ShopReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys.ShopReport
{
    public class ShopReportRepository : AbstractRepository<ShopOrderDO>, IShopReportRepository
    {
        private readonly ApolloErpLogger<ShopReportRepository> logger;
        private readonly string className;
        public ShopReportRepository(ApolloErpLogger<ShopReportRepository> logger)
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderSqlReadOnly");

            this.logger = logger;
            className = this.GetType().Name;
        }

        /// <summary>
        /// 得到订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ShopOrderDO>> GetShopOrderList(GetShopSalesMonthRequest request)
        {
            var parameters = new DynamicParameters();
            var builder = new StringBuilder();
            builder.AppendLine(" and a.is_deleted=0 ");
            builder.AppendLine(" and a.shop_id=@ShopId");
            parameters.Add("@ShopId", request.ShopId);

            if (!string.IsNullOrEmpty(request.StartDate))
            {
                bool isSuccessStart = DateTime.TryParse(request.StartDate, out var startDate);
                bool isSuccessEnd = DateTime.TryParse(request.EndDate, out var endDate);
                if (isSuccessStart && isSuccessEnd)
                {
                    builder.AppendLine($" and  a.install_time between @StartDate and @EndDate");

                    parameters.Add("@StartDate", startDate.ToString("yyyy-MM-dd 00:00:00"));

                    parameters.Add("@EndDate", endDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00"));
                }
            }
            //2022-7-21 不统计套餐卡核销，统计套餐卡销售
            var sql = @"select a.id AS Id,a.shop_id AS ShopId,a.order_no AS OrderNo, a.order_status AS OrderStatus,
                        case when a.produce_type = 14 then 7 else a.order_type end AS OrderType,
                        case when a.produce_type = 15 then c.avg_price * c.num else a.`actual_amount` end as ActualAmount,a.install_status AS InstallStatus,DATE_FORMAT(a.install_time,'%Y-%m-%d') AS InstallTime, 
                        a.user_id AS UserId from `order`  a
                        left join order_package_card c on a.order_no = c.verify_order_no 
                        where a.produce_type  in (0,2,15,16,17,18,19,20)
                        and a.order_status=30
                        " + builder.ToString();
            IEnumerable<ShopOrderDO> costTypeDos = null;
            await OpenConnectionAsync(async conn => costTypeDos = await conn.QueryAsync<ShopOrderDO>(sql,parameters));

            return costTypeDos?.ToList();
        }
    }
}
