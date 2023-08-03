using Microsoft.AspNetCore.Mvc;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Filters;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Controllers
{
    /// <summary>
    /// 示例API
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(DemoController))]
    public class DemoController : ControllerBase
    {
        private readonly ITestService testService;
        private readonly IIdentityService identityService;
        private readonly ApolloErpLogger<DemoController> logger;

        public DemoController(ITestService testService, IIdentityService identityService, ApolloErpLogger<DemoController> logger)
        {
            this.testService = testService;
            this.identityService = identityService;
            this.logger = logger;
        }

        /// <summary>
        /// 获取门店主表信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ShopVO>> GetShopById([FromQuery]GetShopRequest request)
        {
            return await testService.GetShopById(request);
        }
    }
}
