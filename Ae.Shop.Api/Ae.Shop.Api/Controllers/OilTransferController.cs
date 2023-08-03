using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model;

namespace Ae.Shop.Api.Controllers
{
    [Route("[controller]/[action]")]
    //[Filter(nameof(DemoController))]
    public class OilTransferController : ControllerBase
    {
        private readonly IStockManageService stockManageService;
        private readonly IOilTransferService oilTransferService;
        public OilTransferController(IStockManageService _stockManageService,
            IOilTransferService oilTransferService)
        {
            this.stockManageService = _stockManageService;
            this.oilTransferService = oilTransferService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiResult<List<ProductStockDTO>>> GetShopProductStocks([FromQuery]ProductStockDTO request)
        {
            return await this.oilTransferService.GetProductStocks(request);
        }
    }
}