using ApolloErp.Web.WebApi;
using Ae.Order.Service.Core.Model.Order;
using Ae.Order.Service.Core.Request.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Order.Service.Client.Interface
{
    public interface IShopOrderClient
    {
        Task<ApiResult<List<OrderCarDTO>>> GetOrderCarsInfo(GetOrderCarsRequest request);
    }
}
