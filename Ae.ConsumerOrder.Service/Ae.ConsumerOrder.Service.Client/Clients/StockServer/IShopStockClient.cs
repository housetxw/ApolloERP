using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Request.Stock;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ConsumerOrder.Service.Client.Clients.StockServer
{
    public interface IShopStockClient
    {
        /// <summary>
        /// 占库
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> OrderOccupyStock(
            OrderOccupyStockRequest request);
    }
}
