using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Request.Arrival;
using Ae.C.MiniApp.Api.Core.Response.Arrival;
using Ae.C.MiniApp.Api.Filters;

namespace Ae.C.MiniApp.Api.Controllers
{
    /// <summary>
    /// 到店相关
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(ArrivalController))]
    [ApiController]
    public class ArrivalController : ControllerBase
    {
        private readonly IReceiveService _receiveService;
        public ArrivalController(IReceiveService receiveService)
        {
            _receiveService = receiveService;
        }
        /// <summary>
        /// 快速排队接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<QuickQueueResponse>> QuickQueue([FromQuery]QuickQueueRequest request)
        {
            return await _receiveService.QuickQueue(request);
        }

        /// <summary>
        /// 快速排队请求弹层
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<QuickTakeNumberLayerResponse>> QuickTakeNumberLayer([FromBody]ApiRequest<QuickTakeNumberLayerRequest> request)
        {
            return await _receiveService.QuickTakeNumberLayer(request);
        }

        /// <summary>
        /// 排队仅拿号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<QuickTakeNumberLayerResponse>> QuickTakeNumber([FromBody]ApiRequest<QuickTakeNumberRequest> request)
        {
            return await _receiveService.QuickTakeNumber(request);
        }

        /// <summary>
        /// 预约排队拿号
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<QuickTakeNumberLayerResponse>> ReserveTakeNumber([FromBody]ApiRequest<ReserveTakeNumberRequest> request)
        {
            return await _receiveService.ReserveTakeNumber(request);
        }
    }
}