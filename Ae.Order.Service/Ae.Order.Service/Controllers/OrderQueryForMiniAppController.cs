using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Order.Service.Core.Interfaces;
using Ae.Order.Service.Core.Model;
using Ae.Order.Service.Core.Model.ShopOrder;
using Ae.Order.Service.Core.Request;
using Ae.Order.Service.Core.Request.ShopOrder;
using Ae.Order.Service.Core.Response;
using Ae.Order.Service.Filters;

namespace Ae.Order.Service.Controllers
{
    /// <summary>
    /// 小程序订单查询
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(OrderQueryForMiniAppController))]
    public class OrderQueryForMiniAppController : Controller
    {
        private readonly IOrderQueryForMiniAppService orderQueryForMiniAppService;

        private readonly string constMessage = "无数据";

        public OrderQueryForMiniAppController(IOrderQueryForMiniAppService orderQueryForMiniAppService)
        {
            this.orderQueryForMiniAppService = orderQueryForMiniAppService;
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<GetOrderListForMiniAppResponse>> GetOrderListForMiniApp([FromQuery] GetOrderListForMiniAppRequest request)
        {
            return await orderQueryForMiniAppService.GetOrderListForMiniApp(request);
        }


        /// <summary>
        /// 批量获取订单集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<GetOrderListForMiniAppResponse>>> BatchGetOrderListForMiniApp([FromBody] BatchGetOrderListForMiniAppRequest request)
        {
            return await orderQueryForMiniAppService.BatchGetOrderListForMiniApp(request);
        }

        /// <summary>
        /// 获取订单数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<int>> GetOrderCountForMiniApp([FromQuery] GetOrderCountForMiniAppRequest request)
        {
            return await orderQueryForMiniAppService.GetOrderCountForMiniApp(request);
        }

        /// <summary>
        /// 批量获取我的页面各状态订单数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<GetEachStatusOrderCountForMiniAppResponse>>> GetEachStatusOrderCountForMiniApp([FromQuery] GetEachStatusOrderCountForMiniAppRequest request)
        {
            return await orderQueryForMiniAppService.GetEachStatusOrderCountForMiniApp(request);
        }
    }
}
