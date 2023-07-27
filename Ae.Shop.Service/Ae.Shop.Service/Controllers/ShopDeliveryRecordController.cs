using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Core.Interfaces;
using Ae.Shop.Service.Core.Model;

namespace Ae.Shop.Service.Controllers
{
    [Route("[controller]/[action]")]
    public class ShopDeliveryRecordController : ControllerBase
    {
        private readonly IShopDeliveryRecordService _shopDeliveryRecordService;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="shopService"></param>
        public ShopDeliveryRecordController(IShopDeliveryRecordService shopDeliveryRecordService)
        {
            _shopDeliveryRecordService = shopDeliveryRecordService;
        }

        [HttpGet]
        public async Task<ApiResult<ShopDeliveryRecordDTO>> GetShopDeliveryRecord([FromQuery]ShopDeliveryRecordDTO request)
        {
            return await _shopDeliveryRecordService.GetShopDeliveryRecord(request);
        }

        [HttpPost]
        public async Task<ApiResult<string>> AddShopDeliveryRecord([FromBody]ShopDeliveryRecordDTO request)
        {
            return await _shopDeliveryRecordService.AddShopDeliveryRecord(request);
        }

        [HttpPost]
        public async Task<ApiResult<string>> AddShopDeliveryConfig([FromBody]ShopDeliveryConfigDTO request)
        {
            return await _shopDeliveryRecordService.AddShopDeliveryConfig(request);
        }

        [HttpGet]
        public async Task<ApiResult<List<ShopDeliveryConfigDTO>>> GetShopDeliveryConfigs([FromQuery]ShopDeliveryConfigDTO request)
        {
            return await _shopDeliveryRecordService.GetShopDeliveryConfigs(request);
        }

    }
}