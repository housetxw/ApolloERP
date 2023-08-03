using ApolloErp.Data.DapperExtensions;
using Ae.Order.Service.Core.Request;
using Ae.Order.Service.Dal.Model;
using Ae.Order.Service.Dal.Model.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Linq;

namespace Ae.Order.Service.Dal.Repository.Order
{
    public class OrderForBossRepository : AbstractRepository<OrderDO>, IOrderForBossRepository
    {
        public OrderForBossRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderSqlReadOnly");
        }

        public async Task<PagedEntity<GetOrderListForBossDO>> GetOrderListForBoss(GetOrderListForBossRequest request)
        {
            var result = new PagedEntity<GetOrderListForBossDO>();
            var condition = new StringBuilder("WHERE a.is_deleted =0 ");

            #region 组装查询条件
            switch (request.LikeType)
            {
                case 1:
                    condition.Append("AND a.order_no LIKE CONCAT('%',@LikeValue,'%') ");
                    break;
                case 2:
                    condition.Append("AND b.product_name LIKE CONCAT('%',@LikeValue,'%') ");
                    break;
                case 3:
                    condition.Append("AND user_name LIKE CONCAT('%',@LikeValue,'%') ");
                    break;
                default:
                    break;
            }
            if (request.OrderStatus > 0)
            {
                condition.Append("AND order_status = @OrderStatus ");
            }
            if (request.OrderChannel > 0)
            {
                condition.Append("AND channel = @OrderChannel ");
            }
            if (request.DeliveryMethod > 0)
            {
                condition.Append("AND delivery_method = @DeliveryMethod ");
            }
            if (request.DeliveryStatus > -1)
            {
                condition.Append("AND delivery_status = @DeliveryStatus ");
            }
            if (request.IsNeedInstall > -1)
            {
                condition.Append("AND is_need_install = @IsNeedInstall ");
            }
            if (request.InstallStatus > -1)
            {
                condition.Append("AND install_status = @InstallStatus ");
            }
            if (request.PayMethod > -1)
            {
                condition.Append("AND pay_method = @PayMethod ");
            }
            if (request.PayStatus > -1)
            {
                condition.Append("AND pay_status = @PayStatus ");
            }
            if (request.OrderType > 0)
            {
                condition.Append("AND order_type = @OrderType ");
            }
            if (!string.IsNullOrEmpty(request.ContactPhone))
            {
                condition.Append("And contact_phone=@ContactPhone ");
            }
            if (request.ShopId > 0)
            {
                condition.Append(" And shop_id=@ShopId ");
            }

            switch (request.DateTimeType)
            {
                case 1:
                    if (request.StartDateTime > new DateTime(1900, 1, 1))
                    {
                        condition.Append("AND a.create_time >= @StartDateTime ");
                    }
                    if (request.EndDateTime > new DateTime(1900, 1, 1))
                    {
                        condition.Append("AND a.create_time <= @EndDateTime ");
                    }
                    request.EndDateTime = request.EndDateTime.AddDays(1);
                    break;
                default:
                    break;
            }

            if (!string.IsNullOrEmpty(request.InstallStartTime))
            {
                bool isSuccess = DateTime.TryParse(request.InstallStartTime, out var startOrderTime);
                if (isSuccess)
                {
                    condition.AppendLine(" and a.install_time>=@InstallStartTime");
                   
                }
            }
            if (!string.IsNullOrEmpty(request.InstallEndTime))
            {
                bool isSuccess = DateTime.TryParse(request.InstallEndTime, out var endOrderTime);
                if (isSuccess)
                {
                    condition.AppendLine(" and a.install_time<=@InstallEndTime");

                }
            }

            if (request.ProductType > -1)
            {
                condition.Append(" And produce_type=@ProductType ");
            }

            #endregion

            var countSql = $@"SELECT
	                            count( 0 ) 
                            FROM
	                            (
                            SELECT DISTINCT
	                            a.order_no OrderNo,
	                            a.order_status OrderStatus,
	                            a.channel Channel,
	                            a.order_type OrderType,
	                            a.actual_amount ActualAmount,
	                            a.contact_name UserName,
	                            a.create_time CreateTime,
	                            a.install_time InstallTime,
	                            a.delivery_method DeliveryMethod,
	                            a.delivery_status DeliveryStatus,
                                a.pay_status PayStatus,
                                a.pay_method PayMethod,
                                a.install_status InstallStatus,
                                a.install_type InstallType
                            FROM
	                            `order` a
	                            LEFT JOIN order_product b ON b.order_id = a.id
                                {condition.ToString()}
	                            ) c";

            int count = 0;
            await OpenSlaveConnectionAsync(async conn =>
            {
                count = await conn.ExecuteScalarAsync<int>(countSql, request);
            });

            if (count <= 0)
            {
                return result;
            }

            var sql = $@"SELECT DISTINCT
	                        a.order_no OrderNo,
	                        a.order_status OrderStatus,
	                        a.channel Channel,
	                        a.order_type OrderType,
	                        a.actual_amount ActualAmount,
	                        a.user_name UserName,
	                        a.create_time CreateTime,
	                        a.install_time InstallTime,
	                        a.delivery_method DeliveryMethod,
	                        a.delivery_status DeliveryStatus,
                            a.pay_status PayStatus,
                            a.pay_method PayMethod,
                            a.install_status InstallStatus,
                            a.shop_id ShopId,
                            s.simple_name ShopName,
                            a.install_type InstallType,a.sign_status SignStatus,a.user_phone ContactPhone,a.produce_type ProductType
                        FROM
	                        `order` a
	                        LEFT JOIN order_product b ON b.order_id = a.id 
                            LEFT JOIN shop_manage.shop s
                            on a.shop_id=s.id
                        {condition.ToString()}
                        ORDER BY
	                        a.id DESC
	                        LIMIT @PageIndex,@PageSize";

            IEnumerable<GetOrderListForBossDO> list = null;
            request.PageIndex = (request.PageIndex - 1) * request.PageSize;//重写页码用作偏移量参数
            await OpenSlaveConnectionAsync(async conn =>
            {
                list = await conn.QueryAsync<GetOrderListForBossDO>(sql, request);
            });

            result.TotalItems = count;
            result.Items = list?.ToList();

            return result;
        }
    }
}
