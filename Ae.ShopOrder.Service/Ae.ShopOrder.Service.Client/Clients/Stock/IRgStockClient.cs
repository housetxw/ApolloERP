using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Core.Request.Stock;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Client.Clients.Stock
{
    /// <summary>
    /// 库存Client
    /// </summary>
    public interface IRgStockClient
    {
        /// <summary>
        /// 释放库存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateOrderInstallReleaseStock(
            UpdateOrderInstallReleaseStockRequest request);
    }
}
