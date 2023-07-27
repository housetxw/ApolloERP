using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Client.Request;
using Ae.Shop.Service.Client.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Client.Inteface
{
  public  interface IActivityClient
    {
        Task<ApiResult<GenMinAppCodeResponse>> GenMinAppCode(GenMinAppCodeClientRequest request);

    }
}
