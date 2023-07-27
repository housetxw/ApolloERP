using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Microsoft.AspNetCore.Mvc;
using Ae.Account.Api.Core.Interfaces;
using Ae.Account.Api.Core.Request;
using Ae.Account.Api.Core.Response;
using Ae.Account.Api.Filters;
using Ae.Account.Api.Core.Model;

namespace Ae.Account.Api.Controllers
{
    /// <summary>
    /// 示例API
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(ZDemoController))]
    public class ZDemoController : ControllerBase
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
