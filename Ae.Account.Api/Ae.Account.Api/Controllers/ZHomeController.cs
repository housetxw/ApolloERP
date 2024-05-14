using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ApolloErp.Log;
using Ae.Account.Api.Filters;

namespace Ae.Account.Api.Controllers
{
    /// <summary>
    /// Home
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(ZHomeController))]
    public class ZHomeController : ControllerBase
    {
        private readonly ApolloErpLogger<ZHomeController> logger;

        public ZHomeController(ApolloErpLogger<ZHomeController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            //logger.Info("Test Info");
            //logger.Error("\nTest Error");

            //var a = 1;
            //var b = 0;
            //var c = a / b;

            return Redirect("~/swagger/index.html");
        }

        /// <summary>
        /// 健康检查
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<object> Health()
        {
            await Task.CompletedTask;

            return new
            {
                Status = "UP"
            };
        }

        /// <summary>
        /// actuator
        /// </summary>
        /// <returns></returns>
        [HttpGet("/actuator")]
        [AllowAnonymous]
        public async Task<object> Actuator()
        {
            await Task.CompletedTask;

            return new { };
        }
    }
}
