using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Common.Constant;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Response;
using Ae.C.MiniApp.Api.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Controllers
{
    [Route("[controller]/[action]")]
    //[Filter(nameof(BasicDataController))]
    public class BasicDataController : ControllerBase
    {

        private readonly IBasicDataService _basicDataService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="basicDataService"></param>
        public BasicDataController(IBasicDataService basicDataService)
        {
            _basicDataService = basicDataService;
        }
        /// <summary>
        /// 获取省市区地址
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResult<GetRegionChinaResponse>> GetAllRegionChinaWithThreeLayer()
        {
            var result = await _basicDataService.GetAllRegionChinaWithThreeLayer();
            return Result.Success(result);
        }

        
        /// <summary>
        /// 定位
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResult<LocationResponse>> Location([FromQuery] LocationRequest request)
        {
            var result = await _basicDataService.GetPosition(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取省市区三级地址
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResult<ThreeLevelRegionChinaResponse>> GetThreeLevelRegionChina()
        {
            var result = await _basicDataService.GetThreeLevelRegionChina();
            return Result.Success(result);
        }

    }
}
