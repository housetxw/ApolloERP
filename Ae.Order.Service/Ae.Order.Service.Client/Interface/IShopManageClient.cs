using ApolloErp.Web.WebApi;
using Ae.Order.Service.Client.Model;
using Ae.Order.Service.Client.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Order.Service.Client.Interface
{
    public interface IShopManageClient
    {
        Task<ApiResult<ShopSimpleInfoClientDTO>> GetShopSimpleInfoAsync(GetShopRequest request);
    }
}
