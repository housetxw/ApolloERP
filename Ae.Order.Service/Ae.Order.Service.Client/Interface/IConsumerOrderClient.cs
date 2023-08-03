using ApolloErp.Web.WebApi;
using Ae.Order.Service.Core.Model.Order;
using Ae.Order.Service.Core.Request.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Order.Service.Client.Interface
{
    public interface IConsumerOrderClient
    {
        Task<ApiResult<List<OrderCarDTO>>> GetOrderCarsInfo(GetOrderCarsRequest request);


        Task<ApiResult> UpdateOrderDispatchStatus(UpdateOrderDispatchStatusRequest request);

        Task<ApiResult> CancelOrder(ApiRequest<CancelOrderRequest> request);

        Task<ApiResult> UpdatePayStatus(UpdatePayStatusRequest request);
    }
}
