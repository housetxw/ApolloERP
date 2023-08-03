using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Core.Interfaces
{
  public  interface IOrderStockService
    {
        Task<ApiResult<string>> OrderOccupyStock(OrderStockRequest request);

        Task<ApiResult<string>> ReleaseStock(OrderStockRequest request);

        Task<ApiResult<string>> OrderInstallReduceStock(OrderStockRequest request);
        Task<ApiResult<string>> OrderRepeatReduceStock(OrderStockRequest request);

        Task<ApiPagedResult<OccupyQueueDTO>> GetOrderQueues(GetOrderQueueRequest request);

        Task<ApiResult<string>> CancelOrderQueue(GetOrderQueueRequest request);

        Task<ApiResult<string>> ReOccupyStock(GetOrderQueueRequest request);

        Task<ApiResult<List<OccupyStockLogDTO>>> GetOccupyStockLogs(OccupyStockLogDTO request);

        Task<ApiResult<List<OccupyItemDTO>>> GetOccupyItems(OccupyItemDTO request);
    }
}
