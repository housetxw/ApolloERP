using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Request.Receive;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ConsumerOrder.Service.Client.Clients.ReceiveServer
{
    public interface IReceiveClient
    {
        Task<ApiResult<bool>> CheckTheDayReserveWithUserCarId(CheckReserveTimeRequest request);

        Task<ApiResult> AddShopReserveV3(AddReserveV3Request request);
    }
}
