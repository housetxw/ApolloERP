using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Request.Service;
using Ae.C.MiniApp.Api.Core.Response.Service;
using Ae.C.MiniApp.Api.Filters;

namespace Ae.C.MiniApp.Api.Controllers
{
    /// <summary>
    /// 服务记录
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(ServiceController))]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IReceiveService _receiveService;
        public ServiceController(IReceiveService receiveService)
        {
            _receiveService = receiveService;
        }
        /// <summary>
        /// 得到服务记录信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetServiceRecordResponse>> GetServiceRecord([FromQuery]GetServiceRecordRequest request)
        {
            return await _receiveService.GetServiceRecord(request);
        }
    }
}