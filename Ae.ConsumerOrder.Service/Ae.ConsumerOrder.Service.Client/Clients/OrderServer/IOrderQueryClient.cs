using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Core.Model;
using Ae.ConsumerOrder.Service.Core.Request.OrderQuery;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ConsumerOrder.Service.Client.Clients.OrderServer
{
    public interface IOrderQueryClient
    {
        Task<ApiResult<List<OrderDTO>>> GetOrderBaseInfo(GetOrderBaseInfoClientRequest request);
    }
}
