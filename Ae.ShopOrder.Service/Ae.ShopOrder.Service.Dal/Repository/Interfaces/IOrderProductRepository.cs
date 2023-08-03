using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Dal.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Core.Model.Product;
using Ae.ShopOrder.Service.Core.Request;

namespace Ae.ShopOrder.Service.Dal.Repository.Interfaces
{
    public interface IOrderProductRepository :IRepository<OrderProductDO>
    {
        Task<List<OrderProductDO>> GetOrderProducts(long orderId);
        Task<List<OrderProductDO>> GetOrderProducts(GetOrderBaseInfoRequest request);

        /// <summary>
        /// 更新商品库存状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="stockStatus"></param>
        /// <param name="updateBy"></param>
        /// <param name="orderPids"></param>
        /// <returns></returns>
        Task<bool> UpdateProductStockStatus(long orderId, sbyte stockStatus, string updateBy, List<long> orderPids = null);

        Task<int> DeleteOrderProducts(List<long> ids, string orderNo,string updateBy);

        Task<PagedEntity<OrderProductNewDTO>> GetOrderProductsReport(GetOrderProductsRequest request);
    }
}