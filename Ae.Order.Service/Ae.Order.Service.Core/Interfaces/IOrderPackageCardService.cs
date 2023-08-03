using ApolloErp.Web.WebApi;
using Ae.Order.Service.Core.Request;
using Ae.Order.Service.Core.Request.Order;
using Ae.Order.Service.Core.Response;
using Ae.Order.Service.Core.Response.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Order.Service.Core.Interfaces
{
    public interface IOrderPackageCardService
    {
        Task<ApiPagedResult<GetOrderPackageCardsResponse>> GetOrderPackageCards(GetOrderPackageCardsRequest request);

        Task<ApiResult<List<GetPackageCardMainInfoResponse>>> GetPackageCardMainInfo(GetPackageCardMainInfoRequest request);

        Task<ApiResult> UpdateOrderPackage( ApiRequest<UpdateOrderPackageRequest> request);
    }
}
