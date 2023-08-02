using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Core.Interfaces;
using Ae.B.Product.Api.Core.Model;
using Ae.B.Product.Api.Core.Request;

namespace Ae.B.Product.Api.Controllers
{
    [Route("[controller]/[action]")]
    public class FlashSaleConfigController : ControllerBase
    {
        private readonly IFlashSaleConfigService _flashSaleConfigService;

        public FlashSaleConfigController(IFlashSaleConfigService flashSaleConfigService)
        {
            _flashSaleConfigService = flashSaleConfigService;
        }

        [HttpPost]
        public async Task<ApiResult<string>> CreatFlashSaleConfig([FromBody]ApiRequest<FlashSaleConfigDTO> request)
        {
            return await _flashSaleConfigService.CreatFlashSaleConfig(request.Data);
        }

        [HttpGet]
        public async Task<ApiPagedResult<FlashSaleConfigDTO>> GetFlashSaleConfigs([FromQuery]GetFlashSaleConfigRequest request)
        {
            return await _flashSaleConfigService.GetFlashSaleConfigs(request);
        }

        [HttpPost]
        public async Task<ApiResult<string>> UpdateFlashSaleConfig([FromBody]ApiRequest<FlashSaleConfigDTO> request)
        {
            return await _flashSaleConfigService.UpdateFlashSaleConfig(request.Data);
        }
    }
}