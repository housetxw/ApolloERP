using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.B.FMS.Api.Core.Interfaces;
using Ae.B.FMS.Api.Core.Request;
using Ae.B.FMS.Api.Core.Response;
using Ae.B.FMS.Api.Filters;

namespace Ae.B.FMS.Api.Controllers
{

    [Route("[controller]/[action]")]
    //[Filter(nameof(ShopManageController))]

    public class ShopManageController : ControllerBase
    {
        private readonly IShopManageService shopManageService;

        public ShopManageController(IShopManageService shopManageService)
        {
            this.shopManageService = shopManageService;
        }

        [HttpGet]
        public async Task<ApiPagedResult<ShopSimpleInfoResponse>> GetShopListAsync([FromQuery]GetShopListRequest request)
        {
            var result = await shopManageService.GetShopListAsync(request);
            return await Task.FromResult(result);
        }
    }
}