using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.BasicData.Service.Common.Constant;
using Ae.BasicData.Service.Core.Interfaces;
using Ae.BasicData.Service.Core.Request;
using Ae.BasicData.Service.Filters;

namespace Ae.BasicData.Service.Controllers
{
    [Route("[controller]/[action]")]
    //[Filter(nameof(AppOperationLogController))]
    public class AppOperationLogController : Controller
    {
        private readonly IAppOperationLogService appOptLogSvc;

        public AppOperationLogController(IAppOperationLogService appOptLogSvc)
        {
            this.appOptLogSvc = appOptLogSvc;
        }

        /// <summary>
        /// 创建移动端操作日志
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> CreateAppOperationLog([FromBody] CreateAppOperationLogReqDTO req)
        {
            var res = await appOptLogSvc.CreateAppOperationLog(req);
            return Result.Success(res, CommonConstant.OperateSuccess);
        }


    }
}