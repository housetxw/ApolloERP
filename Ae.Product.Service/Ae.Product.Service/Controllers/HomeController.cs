using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ApolloErp.Web.WebApi;
using System;
using System.Threading.Tasks;


namespace Ae.Product.Service.Controllers
{
    /// <summary>
    /// Home
    /// </summary>
    public class HomeController : ControllerBase
    {
      
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
        [HttpGet("/health")]
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
