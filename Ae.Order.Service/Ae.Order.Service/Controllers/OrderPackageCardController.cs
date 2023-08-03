using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Order.Service.Core.Interfaces;
using Ae.Order.Service.Core.Request;
using Ae.Order.Service.Core.Request.Order;
using Ae.Order.Service.Core.Response;
using Ae.Order.Service.Core.Response.Order;
using Ae.Order.Service.Filters;

namespace Ae.Order.Service.Controllers
{
    [Route("[controller]/[action]")]
    [Filter(nameof(OrderPackageCardController))]
    public class OrderPackageCardController : Controller
    {
        private readonly IOrderPackageCardService orderPackageCardService;

        public OrderPackageCardController(IOrderPackageCardService orderPackageCardService)
        {
            this.orderPackageCardService = orderPackageCardService;
        }

        [HttpGet]
        public async Task<ApiPagedResult<GetOrderPackageCardsResponse>> GetOrderPackageCards([FromQuery] GetOrderPackageCardsRequest request)
        {
            return await orderPackageCardService.GetOrderPackageCards(request);
        }

        [HttpGet]
        public async Task<ApiResult<List<GetPackageCardMainInfoResponse>>> GetPackageCardMainInfo([FromQuery] GetPackageCardMainInfoRequest request)
        {
            return await orderPackageCardService.GetPackageCardMainInfo(request);
        }

        [HttpPost]
        public async Task<ApiResult> UpdateOrderPackage([FromBody] ApiRequest<UpdateOrderPackageRequest> request)
        {
            return await orderPackageCardService.UpdateOrderPackage(request);
        }
    }
}