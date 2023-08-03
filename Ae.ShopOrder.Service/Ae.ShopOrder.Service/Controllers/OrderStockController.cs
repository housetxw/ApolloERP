using Microsoft.AspNetCore.Mvc;
using NLog.Filters;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Core.Interfaces;
using Ae.ShopOrder.Service.Core.Request.Stock;
using Ae.ShopOrder.Service.Core.Response.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Controllers
{
    /// <summary>
    /// 订单库存
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(OrderStockController))]
    public class OrderStockController : ControllerBase
    {
        private readonly IOrderStockService orderStockService;

        public OrderStockController(IOrderStockService orderStockService)
        {
            this.orderStockService = orderStockService;
        }

        /// <summary>
        /// 查询占库存订单详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<QueryUseStockOrderDetailResponse>> QueryUseStockOrderDetail([FromQuery]QueryUseStockOrderDetailRequest request)
        {
            return await orderStockService.QueryUseStockOrderDetail(request);
        }
        /// <summary>
        /// 订单占库存结果通知
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> OrderUseStockNotify([FromBody]OrderUseStockNotifyRequest request)
        {
            return await orderStockService.OrderUseStockNotify(request);
        }

        /// <summary>
        /// 查询订单实物详情，含外采产品
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<QueryOrderDetailUseStockResponse>> QueryOrderRealProductDetail([FromQuery] QueryUseStockOrderDetailRequest request)
        {
            return await orderStockService.QueryOrderRealProductDetail(request);
        }
    }
}
