using Microsoft.AspNetCore.Mvc;
using Rg.B.App.Api.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Controllers
{
    [Route("[controller]/[action]")]
    [FilterLog(nameof(TestController))]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public async Task<object> Test()
        {
            await Task.CompletedTask;
            throw new Exception();
            return new
            {
                Status = "Test"
            };

        }
    }
}
