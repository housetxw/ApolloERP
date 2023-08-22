using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.FileUpload.Api.Core.Interfaces;
using Ae.FileUpload.Api.Core.Model;
using Ae.FileUpload.Api.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ae.FileUpload.Api.Controllers
{
    /// <summary>
    /// sky报警
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(SwAlarmController))]
    public class SwAlarmController : ControllerBase
    {
        private readonly ISwAlarmService iSwAlarmService;


        //[HttpGet]
        //public ActionResult Test()
        //{
        //    Thread.Sleep(5000);
        //    throw new Exception("测试错误");
        //}

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="iSwAlarmService"></param>
        public SwAlarmController(ISwAlarmService iSwAlarmService)
        {
            this.iSwAlarmService = iSwAlarmService;
        }
        /// <summary>
        /// sky报警发邮件接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AlarmReceive([FromBody]List<SwAlarmRequest> request)
        {
            await iSwAlarmService.AlarmReceive(request);
            return Result.Success(true);
        }
    }
}
