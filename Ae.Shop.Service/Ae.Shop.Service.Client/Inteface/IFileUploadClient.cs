using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Client.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Client.Inteface
{
  public  interface IFileUploadClient
    {
        Task<ApiResult<string>> UploadBytes(UploadByteRequest request);
    }
}
