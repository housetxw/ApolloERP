using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Receive.Service.Core.Interfaces;
using Ae.Receive.Service.Core.Model;
using Ae.Receive.Service.Core.Model.Arrival;
using Ae.Receive.Service.Core.Request;
using Ae.Receive.Service.Filters;

namespace Ae.Receive.Service.Controllers
{
    /// <summary>
    /// 到店相关服务
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(ReceiveController))]
    public class ReceiveController : ControllerBase
    {
        private readonly ApolloErpLogger<ReserveController> _logger;
        private readonly IShopReceiveService _shopReceiveService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="shopReceiveService"></param>
        public ReceiveController(ApolloErpLogger<ReserveController> logger, IShopReceiveService shopReceiveService)
        {
            this._logger = logger;
            _shopReceiveService = shopReceiveService;
        }

        /// <summary>
        /// 历史到店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<HistoryArrivalVo>>> GetHistoryReceive(
            [FromQuery] HistoryReceiveRequest request)
        {
            var result = await _shopReceiveService.GetHistoryReceive(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 根据到店记录Id查询到店记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<ShopReceiveVo>>> GetReceiveByIds([FromBody] ReceiveByIdsRequest request)
        {
            var result = await _shopReceiveService.GetReceiveByIds(request);

            return Result.Success(result);
        }
    }
}