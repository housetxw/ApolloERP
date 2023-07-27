using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Microsoft.AspNetCore.Mvc;
using Ae.BasicData.Service.Core.Interfaces;
using Ae.BasicData.Service.Core.Request;
using Ae.BasicData.Service.Core.Response;
using Ae.BasicData.Service.Filters;
using Ae.BasicData.Service.Core.Model;
using System.Reflection;
using System.Text;
using Ae.BasicData.Service.Imp.Helpers;
using Microsoft.AspNetCore.Cors;
using ApolloErp.Log;

namespace Ae.BasicData.Service.Controllers
{
    /// <summary>
    /// 示例API
    /// </summary>
    //[Route("[controller]/[action]")]
    //[Filter(nameof(ZDemoController))]
    public class ZDemoController : ControllerBase
    {
        private readonly ApolloErpLogger<ZDemoController> logger;

        private readonly ITestService TestService;

        public ZDemoController(ApolloErpLogger<ZDemoController> logger, ITestService iTestService)
        {
            this.logger = logger;
            this.TestService = iTestService;
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
            //var a = 1;
            //var b = 0;
            //var c = a / b;

            return Result.Success(new GetShopInfoResponse
            {
                ShopID = 123,
                Tel = "1213444324",
                PKID = 1,
                Supplier = "供应商"
            });
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
