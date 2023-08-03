using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Microsoft.AspNetCore.Mvc;
using Ae.ShopOrder.Service.Core.Interfaces;
using Ae.ShopOrder.Service.Core.Request;
using Ae.ShopOrder.Service.Core.Response;
using Ae.ShopOrder.Service.WebApi.Filters;
using Ae.ShopOrder.Service.Core.Model;
using System.Reflection;
using System.Text;
using Ae.ShopOrder.Service.Imp.Helpers;
using Microsoft.AspNetCore.Cors;
using log4net;
using log4net.Repository;
using log4net.Config;
using System.IO;
using ApolloErp.Log;

namespace Ae.ShopOrder.Service.WebApi.Controllers
{
    /// <summary>
    /// 示例API
    /// </summary>
    [Route("[controller]/[action]")]
    [Filter(nameof(DemoController))]
    public class DemoController : ControllerBase
    {
        private readonly ITestService TestService;
        //private readonly ILogger<DemoController> logger;
        private readonly ApolloErpLogger<DemoController> logger;

        public DemoController(ITestService iTestService, ApolloErpLogger<DemoController> logger)
        {
            this.TestService = iTestService;
            this.logger = logger;
        }
        /// <summary>
        /// Get接口带分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<Purchase>> GetPageList([FromQuery] GetListRequest request)
        {
            var result = await TestService.GetPurchase(request);
            return Result.Success(result.ShopList, result.TotalItems);
        }

        /// <summary>
        /// Get接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetShopInfoResponse>> GetShopInfo([FromQuery] GetShopInfoRequest request)
        {
            logger.Error("封装测试");
            //throw new Exception("异常了");
            return Result.Success(new GetShopInfoResponse() { ShopID = 123, Tel = "1213444324", PKID = 1, Supplier = "供应商" });
        }

        /// <summary>
        /// POST接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> UpdateShopInfo([FromBody] ApiRequest<UpdataShopInfo> request)
        {
            await TestService.GetShopInfo();
            return Result.Success(true);
        }




    }
}
