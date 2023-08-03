using ApolloErp.Web.WebApi;
using Ae.Order.Service.Core.Request;
using Ae.Order.Service.Core.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ae.Order.Service.Core.Interfaces
{
    public interface IOrderQueryForMiniAppService
    {
        Task<ApiPagedResult<GetOrderListForMiniAppResponse>> GetOrderListForMiniApp(GetOrderListForMiniAppRequest request);
        Task<ApiResult<int>> GetOrderCountForMiniApp(GetOrderCountForMiniAppRequest request);
        Task<ApiResult<List<GetEachStatusOrderCountForMiniAppResponse>>> GetEachStatusOrderCountForMiniApp(GetEachStatusOrderCountForMiniAppRequest request);
        Task<ApiResult<List<GetOrderListForMiniAppResponse>>> BatchGetOrderListForMiniApp(BatchGetOrderListForMiniAppRequest request);
    }
}
