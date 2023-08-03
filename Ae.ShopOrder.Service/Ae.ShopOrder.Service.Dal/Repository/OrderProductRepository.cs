using System;
using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Dal.Model;
using Ae.ShopOrder.Service.Dal.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Core.Model.Product;
using Ae.ShopOrder.Service.Core.Request;

namespace Ae.ShopOrder.Service.Dal.Repository
{
    public class OrderProductRepository : AbstractRepository<OrderProductDO>, IOrderProductRepository
    {
        public OrderProductRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderSqlReadOnly");
        }

        public async Task<int> DeleteOrderProducts(List<long> ids, string orderNo, string updateBy)
        {
            string sql = @"UPDATE order_product 
                        set update_by = @UpdateBy,
                        update_time = Now( 3 ),
                        is_deleted=1
                        WHERE
                            id In @Ids and
	                        order_no = @OrderNo";



            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    Ids = ids,
                    UpdateBy = updateBy,
                    OrderNo = orderNo
                });
            });
            return count;
        }

        public async Task<PagedEntity<OrderProductNewDTO>> GetOrderProductsReport(GetOrderProductsRequest request)
        {
            PagedEntity<OrderProductNewDTO> response = new PagedEntity<OrderProductNewDTO>();

            var parameters = new DynamicParameters();

            var builder = new StringBuilder();

            //builder.AppendLine(" where 1=1");

            if (!string.IsNullOrEmpty(request.OrderNo))
            {
                builder.AppendLine(" and a.order_no=@OrderNo");
                parameters.Add("@OrderNo", request.OrderNo);
            }
            if (!string.IsNullOrEmpty(request.StartCreateTime))
            {
                bool isSuccess = DateTime.TryParse(request.StartCreateTime, out var startOrderTime);
                if (isSuccess)
                {
                    builder.AppendLine(" and a.create_time>=@OrderTime");
                    parameters.Add("@OrderTime", startOrderTime);
                }
            }

            if (!string.IsNullOrEmpty(request.EndCreateTime))
            {
                bool isSuccess = DateTime.TryParse(request.EndCreateTime, out var endDateTime);
                if (isSuccess)
                {
                    builder.AppendLine(" and a.create_time<@EndTime");
                    parameters.Add("@EndTime", endDateTime);
                }
            }

            if (!string.IsNullOrEmpty(request.ProductCode))
            {
                builder.AppendLine(" and b.product_id=@ProductCode");
                parameters.Add("@ProductCode", request.ProductCode);
            }

            if (request.ShopIds!=null && request.ShopIds.Any())
            {
                builder.AppendLine(" and a.shop_id in @ShopIds");
                parameters.Add("@ShopIds", request.ShopIds);
            }

            string orderBy = " a.create_time desc";


            var sqlCount = @"select Count(1) from `order` a
                            inner join order_product b
                            on a.order_no = b.order_no
                            and a.is_deleted = 0 and b.is_deleted = 0 ";

            sqlCount = sqlCount + builder.ToString();

            var total = 0;
            await OpenSlaveConnectionAsync(async conn =>
            {
                total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, parameters);
            });

            var sql = @"select b.id Id,b.order_id OrderId,b.order_no OrderNo,b.product_id ProductId,b.product_name ProductName,
                        b.display_name DisplayName, b.product_attribute ProductAttribute,
                         b.parent_order_package_pid ParentOrderPackagePid, b.serve_for_order_pids ServeForOrderPids,
                          b.category_code CategoryCode, b.item_code ItemCode, b.label Label, b.image_url ImageUrl,
                             b.price_id PriceId, b.marketing_price MarketingPrice, b.price Price, b.total_cost_price TotalCostPrice,
                               b.unit Unit, b.number Number, b.total_number TotalNumber, b.stock_status StockStatus, b.amount Amount,
                                  b.total_amount TotalAmount, b.tax_rate TaxRate, b.share_discount_amount ShareDiscountAmount, b.actual_pay_amount ActualPayAmount,a.create_time CreateTime,
                                     a.shop_id shopId from `order` a
                                     inner join order_product b
                                     on a.order_no = b.order_no
                        and a.is_deleted = 0 and b.is_deleted = 0 " + builder.ToString() + "  ORDER BY a.id desc LIMIT @PageIndex, @PageSize";

            parameters.Add("@PageIndex", (request.PageIndex - 1) * request.PageSize);
            parameters.Add("@PageSize",  request.PageSize);
            IEnumerable< OrderProductNewDTO > orderDos = null;
            await OpenConnectionAsync(async conn => orderDos = await conn.QueryAsync<OrderProductNewDTO>(sql, parameters));

            response.TotalItems = total;
            response.Items = orderDos?.ToList();

          //  var list = await GetListPagedAsync<OrderProductDO>(request.PageIndex, request.PageSize, builder.ToString(),orderBy, parameters);
            return response;
        }

        public async Task<List<OrderProductDO>> GetOrderProducts(long orderId)
        {
            var list = await GetListAsync("where is_deleted=0 and order_id=@OrderId", new { OrderId = orderId });
            return list?.ToList();
        }

        public async Task<List<OrderProductDO>> GetOrderProducts(GetOrderBaseInfoRequest request)
        {
            var condition = new StringBuilder();
            var parameters = new DynamicParameters();

            condition.AppendLine("where   is_deleted=0");

            if (request.OrderNos?.Count > 0)
            {
                condition.AppendLine(" and order_no in @OrderNos");
                parameters.Add("@OrderNos", request.OrderNos);
            }

            if (request.OrderIds?.Count > 0)
            {
                condition.AppendLine(" and order_id in @OrderIds");
                parameters.Add("@OrderIds", request.OrderIds);
            }

            var list = await GetListAsync(condition.ToString(), parameters, false);
            return list?.ToList();
        }


        public async Task<bool> UpdateProductStockStatus(long orderId, sbyte stockStatus, string updateBy, List<long> orderPids = null)
        {
            string sql = @"UPDATE order_product 
                        SET stock_status = @StockStatus,
                        update_by = @UpdateBy,
                        update_time = Now( 3 ) 
                        WHERE
	                        order_id = @OrderId
	                        AND stock_status < @StockStatus";

            if (orderPids != null && orderPids.Any())
            {
                sql += @" AND id IN @OrderPids";
            }

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    OrderId = orderId,
                    StockStatus = stockStatus,
                    UpdateBy = updateBy,
                    OrderPids = orderPids
                });
            });
            return count > 0;
        }
    }
}
