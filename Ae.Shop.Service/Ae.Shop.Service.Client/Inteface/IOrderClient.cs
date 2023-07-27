using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Client.Model;
using Ae.Shop.Service.Client.Model.Order;
using Ae.Shop.Service.Client.Request;
using Ae.Shop.Service.Client.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Client.Inteface
{
    public interface IOrderClient
    {
        Task<List<GetOrderCountByShopIdClientResponse>> BatchGetOrderCountByShopId(GetOrderCountByShopIdClientRequest request);
        Task<List<OrderClientDTO>> GetOrderBaseInfo(GetOrderBaseInfoClientRequest request);

        Task<ApiResult<List<OrderProductDTO>>> GetOrderProduct(GetOrderProductRequest request);
    }
}
