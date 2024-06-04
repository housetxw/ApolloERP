using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ApolloErp.Web.WebApi;
using System;
using System.Threading.Tasks;
using ApolloErp.Log;
using Ae.BasicData.Service.Filters;

namespace Ae.BasicData.Service.Controllers
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
        public ActionResult Index()
        {
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
