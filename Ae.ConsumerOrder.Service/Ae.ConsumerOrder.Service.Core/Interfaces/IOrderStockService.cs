using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Core.Request;
using Ae.ConsumerOrder.Service.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ConsumerOrder.Service.Core.Interfaces
{
    public interface IOrderStockService
    {
        Task<ApiResult> SendOrderUseStock(SendOrderUseStockRequest request);
        Task<ApiResult<QueryUseStockOrderDetailResponse>> QueryUseStockOrderDetail(QueryUseStockOrderDetailRequest request);
        Task<ApiResult> SendOrderReleaseStock(SendOrderReleaseStockRequest request);
        Task<ApiResult> OrderUseStockNotify(OrderUseStockNotifyRequest request);
    }
}
