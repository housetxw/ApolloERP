using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.ConsumerOrder.Service.Core.Request.SharingPromotion;
using Ae.ConsumerOrder.Service.Dal.Model.SharingPromotin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ConsumerOrder.Service.Dal.Repository.SharingPromtion
{
    public class OrderUserShareDetailRepository : AbstractRepository<OrderUserShareDetailDO>, IOrderUserShareDetailRepository
    {
        public OrderUserShareDetailRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderSqlReadOnly");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<OrderUserShareDetailDO>> GetOrderUserShareDetail(GetSharingOrdersRequest request)
        {
            PagedEntity<OrderUserShareDetailDO> response = new PagedEntity<OrderUserShareDetailDO>();
            if (string.IsNullOrWhiteSpace(request.UserId))
                return await Task.FromResult(new PagedEntity<OrderUserShareDetailDO>(totalItems: 0, items: null));
            else
            {
                var parameters = new DynamicParameters();
                var builder = new StringBuilder();
                builder.AppendLine("where 1=1");

                builder.AppendLine(" and source_user_id=@UserId");
                parameters.Add("@UserId", request.UserId);

                if (request.ExchangeStatus)
                {
                    builder.AppendLine(" and exchange_status =0");
                   // parameters.Add("@UserId", request.UserId);
                }

                response = await GetListPagedAsync<OrderUserShareDetailDO>(request.PageIndex, request.PageSize, builder.ToString(), "create_time desc",
                      parameters, false);

                return response;

            }
        }

        public async Task<OrderUserShareDetailDO> GetOrderUserShareDetailDoByUserId(string sourceUserId, string destinationUserId)
        {
            var getUserShare =await GetListAsync("Where source_user_id=@SourceUserId and destination_user_id=@DestinationUserId", new { SourceUserId = sourceUserId, DestinationUserId = destinationUserId }, true);
            return getUserShare?.ToList()?.FirstOrDefault();
        }

        public async Task<int> UpdateOrderUserShareDetailStatus(List<string> orderNos,long couponId)
        {
            string sql = @"UPDATE order_user_share_detail 
                            SET exchange_status=1,coupon_id=@CouponId
                                
                            WHERE
                                 order_no in @OrderNos";
            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new {   OrderNos = orderNos, CouponId= couponId });
            });
            return count;
        }
    }
}
