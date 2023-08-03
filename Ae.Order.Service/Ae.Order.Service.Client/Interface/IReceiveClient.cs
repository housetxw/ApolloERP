using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.Order.Service.Client.Request.Receive;
using Ae.Order.Service.Client.Response.Receive;

namespace Ae.Order.Service.Client.Interface
{
    public interface IReceiveClient
    {
        Task<List<ShopReserveOrderDTO>> GetReserveInfoByReserveIdOrOrderNum(GetReserveInfoByReserveIdOrOrderNum req);

    }
}
