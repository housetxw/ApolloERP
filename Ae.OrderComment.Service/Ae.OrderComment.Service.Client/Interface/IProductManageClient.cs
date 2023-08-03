using ApolloErp.Web.WebApi;
using Ae.OrderComment.Service.Client.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.OrderComment.Service.Client.Interface
{
    public interface IProductManageClient
    {
        Task<ApiResult<bool>> BatchUpdateProduct(BatchUpdateProductRequest request);
    }
}
