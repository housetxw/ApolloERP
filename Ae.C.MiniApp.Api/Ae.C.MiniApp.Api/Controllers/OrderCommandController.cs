using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Response;
using Ae.C.MiniApp.Api.Filters;

namespace Ae.C.MiniApp.Api.Controllers
{
    /// <summary>
    /// 订单操作
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(OrderCommandController))]
    public class OrderCommandController : ControllerBase
    {
        private readonly IOrderCommandService orderCommandService;

        public OrderCommandController(IOrderCommandService orderCommandService)
        {
            this.orderCommandService = orderCommandService;
        }

        /// <summary>
        /// 提交订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<SubmitOrderResponse>> SubmitOrder([FromBody]ApiRequest<SubmitOrderRequest> request)
        {
            return await orderCommandService.SubmitOrder(request);
        }

        /// <summary>
        /// 再次购买
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Obsolete("流程调整暂不上线")]
        [HttpPost]
        public async Task<ApiResult<BuyAgainResponse>> BuyAgain([FromBody]ApiRequest<BuyAgainRequest> request)
        {
            return await orderCommandService.BuyAgain(request);
        }
    }
}
