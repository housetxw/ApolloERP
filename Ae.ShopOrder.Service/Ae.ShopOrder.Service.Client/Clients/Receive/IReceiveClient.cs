using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Client.Request.Receive;
using Ae.ShopOrder.Service.Client.Response.Receive;
using Ae.ShopOrder.Service.Core.Model.Arrival;
using Ae.ShopOrder.Service.Core.Request.Arrival;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Core.Response;
using Ae.ShopOrder.Service.Core.Response.Order;

namespace Ae.ShopOrder.Service.Client.Clients.Receive
{
    public interface IReceiveClient
    {
        Task<List<ShopReserveOrderDTO>> GetReserveInfoByReserveIdOrOrderNum(GetReserveInfoByReserveIdOrOrderNum req);

        Task<ApiResult<List<ShopArrivalOrderDTO>>> GetShopArrivalOrder(GetShopArrivalOrderRequest request);

        Task<ApiResult<GetArrivalListResponse>> GetLastArrival(GetLastArrivalRequest request);

        Task<ApiResult<bool>> ArrivalRelateOrder(ArrivalRelateOrderRequest request);

        Task<ApiResult<ShopReserveDTO>> GetShopReserveDO(ReserveDetailRequest request);

        Task<ApiResult<bool>> CheckTheDayReserveWithUserCarId(CheckReserveTimeRequest request);

        Task<ApiResult> AddShopReserveV3(AddReserveV3Request request);

        Task<ApiResult<List<ShopArrivalVideoResponse>>> GetShopArrivalVideoForOrder(
           GetShopArrivalVideoForOrderRequest request);
    }
}
