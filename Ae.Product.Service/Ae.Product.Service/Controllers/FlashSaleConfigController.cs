using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Product.Service.Core.Interfaces;
using Ae.Product.Service.Core.Model;
using Ae.Product.Service.Core.Request;

namespace Ae.Product.Service.Controllers
{

    [Route("[controller]/[action]")]
    public class FlashSaleConfigController : ControllerBase
    {
        private readonly IFlashSaleConfigService _flashSaleConfigService;

        public FlashSaleConfigController(IFlashSaleConfigService flashSaleConfigService)
        {
            this._flashSaleConfigService = flashSaleConfigService;
        }

        [HttpPost]
        public async Task<ApiResult<string>> CreatFlashSaleConfig([FromBody]FlashSaleConfigDTO request)
        {
            return await _flashSaleConfigService.CreatFlashSaleConfig(request);
        }

        [HttpGet]
        public async Task<ApiPagedResult<FlashSaleConfigDTO>> GetFlashSaleConfigs([FromQuery]GetFlashSaleConfigRequest request)
        {
            return await _flashSaleConfigService.GetFlashSaleConfigs(request);
        }

        [HttpPost]
        public async Task<ApiResult<string>> UpdateFlashSaleConfig([FromBody]FlashSaleConfigDTO request)
        {
            return await _flashSaleConfigService.UpdateFlashSaleConfig(request);
        }

        [HttpGet]
        public async Task<ApiResult<List<FlashSaleConfigDTO>>> GetActiveFlashSaleConfigs()
        {
            return await _flashSaleConfigService.GetActiveFlashSaleConfigs();
        }

        [HttpGet]
        public async Task<ApiResult<FlashSaleConfigDTO>> GetFlashSaleProduct([FromQuery]FlashSaleConfigDTO request)
        {
            return await _flashSaleConfigService.GetFlashSaleProduct(request);
        }

        [HttpPost]
        public async Task<ApiResult<string>> UpdateFlashSaleConfigNum([FromBody]FlashSaleConfigDTO request)
        {
            return await _flashSaleConfigService.UpdateFlashSaleConfigNum(request);
        }

        /// <summary>
        /// 自动下架限时活动
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> AutoOffFlashSaleConfig()
        {
            return await _flashSaleConfigService.AutoOffFlashSaleConfig();
        }
    }
}