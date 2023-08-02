using Microsoft.AspNetCore.Mvc;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.B.Order.Api.Core.Interfaces;
using Ae.B.Order.Api.Core.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ae.B.Order.Api.Controllers
{
    [Route("[controller]/[action]")]
    public class ReceiveController
    {
        private IReceiveService receiveService;
        private readonly ApolloErpLogger<ReceiveController> _logger;

        public ReceiveController(IReceiveService receiveService,
            ApolloErpLogger<ReceiveController> logger
            )
        {
            this.receiveService = receiveService;
            _logger = logger;
        }

        /// <summary>
        /// 修改预约时间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> ModifyReserveTime([FromBody] ApiRequest<ModifyReserveTimeRequest> request)
        {
            var result = await receiveService.ModifyReserveTime(request.Data);
            return Result.Success(result);
        }
    }
}
