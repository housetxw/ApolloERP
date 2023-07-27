using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ApolloErp.Web.WebApi;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace Ae.B.Login.Api.Controllers
{
    /// <summary>
    /// Home
    /// </summary>
    public class ZHomeController : Controller
    {
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
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
