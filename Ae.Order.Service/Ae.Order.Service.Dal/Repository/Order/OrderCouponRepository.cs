using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Order.Service.Core.Model.Order;
using Ae.Order.Service.Core.Request.Order;
using Ae.Order.Service.Dal.Model.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Order.Service.Dal.Repository.Order
{
    public class OrderCouponRepository : AbstractRepository<OrderCouponDO>, IOrderCouponRepository
    {
        public OrderCouponRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderSqlReadOnly");
        }

        public async Task<List<OrderCouponDTO>> GetOrderCoupons(GetOrderCouponsRequest request)
        {
            var parameters = new DynamicParameters();
            var builder = new StringBuilder();

            if (request.OrderIds != null && request.OrderIds.Any())
            {
                builder.AppendLine(" and A.order_id in @OrderIds");
                parameters.Add("@OrderIds", request.OrderIds);
            }

            if (request.CouponIds != null && request.CouponIds.Any())
            {
                builder.AppendLine(" and A.user_coupon_id in @CouponIds");
                parameters.Add("@CouponIds", request.CouponIds);
            }

            if (request.CouponRuleIds != null && request.CouponRuleIds.Any())
            {
                builder.AppendLine(" and A.coupon_rule_id in @CouponRuleIds");
                parameters.Add("@CouponRuleIds", request.CouponRuleIds);
            }

            if (string.IsNullOrEmpty(parameters.ToString()))
                return new List<OrderCouponDTO>();
            else
            {
                var sql = @"select A.id Id,
                            A.order_id OrderId,
                            B.order_no OrderNo ,
                            A.user_coupon_id UserCouponId,
                            A.coupon_rule_id CouponRuleId,
                            A.coupon_name CouponName,
                            A.coupon_amount CouponAmount,
                            R.`category`  Category,
                            R.`type` Type,
                            R.`range_type` RangeType
                            from order_coupon A
                            inner join `order` B ON A.order_id=B.id and  B.is_deleted=0
							inner join coupon.coupon_rule R ON R.id = A.coupon_rule_id and  R.is_deleted=0
                            where A.is_deleted=0 " + builder.ToString();

                IEnumerable<OrderCouponDTO> orderCouponDTOs = null;
                await OpenConnectionAsync(async conn => orderCouponDTOs = await conn.QueryAsync<OrderCouponDTO>(sql , parameters));
                return orderCouponDTOs?.ToList();

            }
        }
    }
}
