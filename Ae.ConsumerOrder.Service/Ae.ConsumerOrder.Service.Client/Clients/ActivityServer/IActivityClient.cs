using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Request.Activity;
using Ae.ConsumerOrder.Service.Client.Response.Activity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ConsumerOrder.Service.Client.Clients.ActivityServer
{
    public interface IActivityClient
    {
        Task<ApiResult<GetUserRefersResponse>> GetUserRefers(GetUserRefersRequest request);
    }
}
