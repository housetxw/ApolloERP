using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.BasicData.Service.Common.Constant;
using Ae.BasicData.Service.Core.Interfaces;
using Ae.BasicData.Service.Core.Model;
using Ae.BasicData.Service.Core.Request;
using Ae.BasicData.Service.Core.Response;
using Ae.BasicData.Service.Filters;

namespace Ae.BasicData.Service.Controllers
{
    [Route("[controller]/[action]")]
    //[Filter(nameof(AppVersionController))]
    public class AppVersionController : Controller
    {
        private readonly IAppVersionService appVerSvc;

        public AppVersionController(IAppVersionService appVerSvc)
        {
            this.appVerSvc = appVerSvc;
        }


        /// <summary>
        /// 获取鹅管家App版本信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<AppVersionEntityResDTO>> GetAppVersionInfo([FromQuery] AppVersionEntityReqDTO req)
        {
            var res = await appVerSvc.GetAppVersionInfo(req);
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 获取鹅管家App历史版本信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<AppVersionListResDTO>>> GetAppVersionHistory()
        {
            var res = await appVerSvc.GetAppVersionHistory();
            return Result.Success(res, CommonConstant.QuerySuccess);
        }


    }
}