using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Microsoft.AspNetCore.Mvc;
using Ae.AccountAuthority.Service.Core.Interfaces;
using Ae.AccountAuthority.Service.Core.Request;
using Ae.AccountAuthority.Service.Core.Response;
using Ae.AccountAuthority.Service.Filters;
using Ae.AccountAuthority.Service.Core.Model;
using System.Reflection;
using System.Text;
using Ae.AccountAuthority.Service.Imp.Helpers;
using Microsoft.AspNetCore.Cors;

namespace Ae.AccountAuthority.Service.Controllers
{
    /// <summary>
    /// 示例API
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(ZDemoController))]
    public class ZDemoController : Controller
    {
        private readonly ITestService TestService;

        public ZDemoController(ITestService iTestService)
        {
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
