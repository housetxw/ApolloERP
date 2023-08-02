using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.B.User.Api.Core.Interfaces;
using Ae.B.User.Api.Core.Request.Reserve;
using Ae.B.User.Api.Core.Response.Reserve;

namespace Ae.B.User.Api.Controllers
{
    /// <summary>
    /// 预约相关
    /// </summary>
    [Route("[controller]/[action]")]
    public class ReserveController : ControllerBase
    {
        private readonly IReserveService reserveService;
        /// <summary>
        /// 构造
        /// </summary>
        public ReserveController(IReserveService reserveService)
        {
            this.reserveService = reserveService;
        }

        /// <summary>
        /// 预约时间看板 Web端
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ReserveDateForWebResponse>> GetReserveDateForWeb(
            [FromQuery] ReserveDateForWebRequest request)
        {
            var result = await reserveService.GetReserveDateForWeb(request);

            return Result.Success(result);
        }
    }
}