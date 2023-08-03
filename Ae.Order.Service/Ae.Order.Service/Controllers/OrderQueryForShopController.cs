using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NLog.Filters;
using ApolloErp.Web.WebApi;
using Ae.Order.Service.Core.Interfaces;
using Ae.Order.Service.Core.Model.Order;
using Ae.Order.Service.Core.Request.Order;

namespace Ae.Order.Service.Controllers
{
    /// <summary>
    /// 订单基础操作服务
    /// </summary>
    [Route("[controller]/[action]")]
   // [Filter(nameof(OrderQueryForShopController))]
    public class OrderQueryForShopController : Controller
    {
        private readonly IOrderQueryForShopService _orderQueryForShopService;

        /// <summary>
        /// 依赖注入构造函数
        /// </summary>
        /// <param name="orderCommandService"></param>
        public OrderQueryForShopController(IOrderQueryForShopService orderQueryForShopService)
        {
            _orderQueryForShopService = orderQueryForShopService;
        }
        /// <summary>
        /// 得到订单列表        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<OrderDTO>> GetOrderInfoListForShop(
             [FromBody] GetOrderInfoListForShopRequest request)
        {
            return await _orderQueryForShopService.GetOrderInfoListForShop(request);
        }
    }
}