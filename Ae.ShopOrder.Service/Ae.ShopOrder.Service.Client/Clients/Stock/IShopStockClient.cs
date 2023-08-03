using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Core.Model.Stock;
using Ae.ShopOrder.Service.Core.Request.Stock;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Client.Clients.Stock
{
    public interface IShopStockClient
    {
        /// <summary>
        /// 安装出库
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> OrderInstallReduceStock(
            OrderInstallReduceStockRequest request);

        /// <summary>
        /// 占库
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> OrderOccupyStock(
            OrderOccupyStockRequest request);

        Task<ApiResult> ReleaseStock(ReleaseStockRequest request);
        Task<ApiResult> OrderCancelReleaseStockNoBatch(ReleaseStockRequest request);

        Task<ApiResult<List<ProductStockDTO>>> GetTransferProductStocks(GetShopProductStocksRequest request);


    }
}
