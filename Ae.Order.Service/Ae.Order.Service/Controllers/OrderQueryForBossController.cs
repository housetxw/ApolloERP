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
    /// BOSS订单查询
    /// </summary>
    [Route("[controller]/[action]")]
  //  [Filter(nameof(OrderQueryForBossController))]
    public class OrderQueryForBossController : Controller
    {
        private readonly IOrderQueryForBossService orderQueryForBossService;

        public OrderQueryForBossController(IOrderQueryForBossService orderQueryForBossService)
        {
            this.orderQueryForBossService = orderQueryForBossService;
        }

        /// <summary>
        /// 获取BOSS订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<GetOrderListForBossResponse>> GetOrderListForBoss([FromQuery]GetOrderListForBossRequest request)
        {
            return await orderQueryForBossService.GetOrderListForBoss(request);
        }

        [HttpGet]
        public async Task<ApiPagedResult<GetPackageCardRecordsResponse>> GetPackageCardRecords([FromQuery]GetPackageCardRecordsRequest request)
        {
            return await orderQueryForBossService.GetPackageCardRecords(request);

        }

    }
}
