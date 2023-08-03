using ApolloErp.Data.DapperExtensions;
using Ae.Order.Service.Core.Request;
using Ae.Order.Service.Dal.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Order.Service.Dal.Repository.ConsumerOrder
{
    public class OrderForMiniAppRepository : AbstractRepository<OrderDO>, IOrderForMiniAppRepository
    {
        public OrderForMiniAppRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderSqlReadOnly");
        }

        public async Task<List<OrderDO>> BatchGetOrderListForMiniApp(BatchGetOrderListForMiniAppRequest request)
        {
            var condition = new StringBuilder("where 1=1 ");
            if (request.OrderNos != null && request.OrderNos.Any())
            {
                condition.Append("and order_no in @OrderNos ");
            }
            else
            {
                return new List<OrderDO>();
            }
            var list = await GetListAsync(condition.ToString(), request);
            return list?.ToList();
        }

        public async Task<int> GetOrderCountForMiniApp(GetOrderCountForMiniAppRequest request)
        {
            var condition = new StringBuilder("where user_id = @UserId");

            if (request.OrderType > 0)
            {
                condition.Append(" and order_type = @OrderType");
            }
            switch (request.OrderListStatus)
            {
                case 1://待付款
                    condition.Append(" and order_status = 10 and pay_status = 0 and ( is_occur_reverse = 0 or reverse_status = 30 )");
                    break;
                case 2://待收货
                    condition.Append(" and order_status = 20 and delivery_status = 1 and sign_status = 0 and ( is_occur_reverse = 0 or reverse_status = 30 )");
                    break;
                case 3://待安装
                    condition.Append(" and order_status = 20 and sign_status = 1 and ( is_occur_reverse = 0 or reverse_status = 30 )");
                    break;
                case 4://待评价
                    condition.Append(" and order_status = 30 and comment_status = 0 and ( is_occur_reverse = 0 or reverse_status = 30 )");
                    break;
                case 5://售后/退货
                    condition.Append(" and is_occur_reverse = 1 and reverse_status <> 30");
                    break;
                default:
                    break;
            }

            var count = await RecordCountAsync(condition.ToString(), request);
            return count;
        }

        public async Task<PagedEntity<OrderDO>> GetOrderListForMiniApp(GetOrderListForMiniAppRequest request)
        {
            var condition = new StringBuilder("where user_id = @UserId");

            if (request.OrderType > 0)
            {
                condition.Append(" and order_type = @OrderType");
            }
            if (request.ProductType > -1)
            {
                condition.Append(" and produce_type = @ProductType");
            }
            switch (request.OrderListStatus)
            {
                case 1://待付款
                    condition.Append(" and order_status = 10 and pay_status = 0 and ( is_occur_reverse = 0 or reverse_status = 30 )");
                    break;
                case 2://待收货
                    if (request.MiniAppType == 1)
                    {
                        condition.Append(" and order_status = 20 and produce_type !=5 and delivery_status = 1 and sign_status = 0 and ( is_occur_reverse = 0 or reverse_status = 30 )");
                    }
                    else
                    {
                        condition.Append(" and order_status = 20 and delivery_status = 1 and sign_status = 0 and ( is_occur_reverse = 0 or reverse_status = 30 )");
                    }
                    break;
                case 3://待安装
                    condition.Append(" and order_status = 20 and sign_status = 1 and ( is_occur_reverse = 0 or reverse_status = 30 )");
                    break;
                case 4://待评价
                    condition.Append(" and order_status = 30 and comment_status = 0 and ( is_occur_reverse = 0 or reverse_status = 30 )");
                    break;
                case 5://售后/退货
                    condition.Append(" and is_occur_reverse = 1 and reverse_status <> 30");
                    break;
                case 6://已完成
                    condition.Append(" and order_status = 30");
                    break;
                case 7://已取消
                    condition.Append(" and order_status = 40");
                    break;
                default:
                    break;
            }

            var pageList = await GetListPagedAsync(request.PageIndex, request.PageSize, condition.ToString(), "id desc", request);
            return pageList;
        }
    }
}
