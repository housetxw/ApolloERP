using ApolloErp.Web.WebApi;
using Ae.B.Order.Api.Client.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Order.Api.Client.Clients.Receive
{
    public interface IReceiveClient
    {
        /// <summary>
        /// 修改预约时间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> ModifyReserveTime(ModifyReserveTimeClientRequest request);
    }
}
