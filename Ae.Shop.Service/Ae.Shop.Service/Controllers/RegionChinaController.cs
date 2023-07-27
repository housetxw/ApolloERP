using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Core.Interfaces;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Controllers
{
    [Route("[controller]/[action]")]
    public class RegionChinaController : ControllerBase
    {
        private readonly IRegionChinaService regionChinaService;
        private readonly ApolloErpLogger<RegionChinaController> logger;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="shopService"></param>
        public RegionChinaController(
            IRegionChinaService regionChinaService,
            ApolloErpLogger<RegionChinaController> logger
            )
        {
            this.regionChinaService = regionChinaService;
            this.logger = logger;
        }

        /// <summary>
        /// 根据RegionId获取子一级区域
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<RegionChinaVO>>> GetRegionChinaListByRegionId([FromQuery] GetRegionChinaListByRegionIdRequest request) 
        {
            var result = await regionChinaService.GetRegionChinaListByRegionId(request);
            return Result.Success(result);
        }
    }
}
