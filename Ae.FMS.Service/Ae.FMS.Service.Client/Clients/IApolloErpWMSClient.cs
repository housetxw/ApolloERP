using ApolloErp.Web.WebApi;
using Ae.FMS.Service.Core.Request;
using Ae.FMS.Service.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.FMS.Service.Client.Clients
{
    public interface IApolloErpWMSClient
    {
        Task<ApiResult<List<GetOrderOccupyShopProductPurchaseResponse>>> GetOrderOccupyShopProductPurchaseInfo(
         GetOrderOccupyShopProductPurchaseInfoReqeust reqeust);
    }
}
