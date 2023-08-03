using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Core.Interfaces;
using Ae.ConsumerOrder.Service.Core.Request;
using Ae.ConsumerOrder.Service.Core.Response;
using Ae.ConsumerOrder.Service.Filters;

namespace Ae.ConsumerOrder.Service.Controllers
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
        /// 发起订单占库存请求
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> SendOrderUseStock([FromBody]SendOrderUseStockRequest request)
        {
            return await orderStockService.SendOrderUseStock(request);
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
        /// 发起订单释放库存请求
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> SendOrderReleaseStock([FromBody]SendOrderReleaseStockRequest request)
        {
            return await orderStockService.SendOrderReleaseStock(request);
        }
    }
}
