using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.ConsumerOrder.Service.Core.Request;
using Ae.ConsumerOrder.Service.Dal.Model;

namespace Ae.ConsumerOrder.Service.Dal.Repository
{
    public class OrderVerificationCodeRepository : AbstractRepository<OrderVerificationCodeDO>, IOrderVerificationCodeRepository
    {
        public OrderVerificationCodeRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderSqlReadOnly");
        }

        public async Task<bool> ActiveByOrderNo(string orderNo)
        {
            var sql = "update order_verification_code set is_deleted=0 where order_no=@OrderNo";
            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new { OrderNo = orderNo });
            });
            return count > 0;
        }

        public async Task<OrderVerificationCodeDO> GetByCode(string code)
        {
            var list = await GetListAsync("where code=@Code", new { Code = code });
            return list?.FirstOrDefault();
        }

        public async Task<List<OrderVerificationCodeDO>> GetListByUserIdAndOrderNo(string userId, string orderNo, bool useMaster = false)
        {
            var list = await GetListAsync("where user_id=@UserId and order_no=@OrderNo", new { UserId = userId, OrderNo = orderNo }, useMaster);
            return list?.ToList();
        }

        public async Task<PagedEntity<OrderVerificationCodeDO>> GetListPagedByCondition(GetVerificationCodeOrderListRequest request)
        {
            var sqlBuilder = new StringBuilder("where verify_shop_id=@VerifyShopId and status=1");

            if (!string.IsNullOrWhiteSpace(request.VerifyOrderNo))
            {
                sqlBuilder.Append(" and verify_order_no like concat('%',@VerifyOrderNo,'%')");
            }
            if (!string.IsNullOrWhiteSpace(request.Code))
            {
                sqlBuilder.Append(" and code like concat('%',@Code,'%')");
            }
            switch (request.DateRangeType)
            {
                case 1: sqlBuilder.Append($" and verify_time>='{DateTime.Today}' and verify_time<'{DateTime.Today.AddDays(1)}'"); break;
                case 2: sqlBuilder.Append($" and verify_time>='{DateTime.Today.AddDays(-1)}' and verify_time<'{DateTime.Today}'"); break;
                case 3:
                    var firstDayOfThisMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    sqlBuilder.Append($" and verify_time>='{firstDayOfThisMonth}' and verify_time<'{firstDayOfThisMonth.AddMonths(1).AddDays(1)}'"); break;
                default:
                    break;
            }

            var pageList = await GetListPagedAsync(request.PageIndex, request.PageSize, sqlBuilder.ToString(), "update_time desc,create_time desc", request);

            return pageList;
        }
    }
}
