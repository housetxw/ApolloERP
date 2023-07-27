using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ApolloErp.Web.WebApi;
using System;
using System.Threading.Tasks;
using ApolloErp.Log;

namespace Ae.AccountAuthority.Service.Controllers
{
    /// <summary>
    /// Home
    /// </summary>
    public class ZHomeController : Controller
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
        public ActionResult Index()
        {
            return Redirect("~/swagger/index.html");
        }

        /// <summary>
        /// 健康检查
        /// </summary>
        /// <returns></returns>
        [HttpGet]
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
        public async Task<object> Actuator()
        {
            await Task.CompletedTask;

            return new { };
        }
    }
}
