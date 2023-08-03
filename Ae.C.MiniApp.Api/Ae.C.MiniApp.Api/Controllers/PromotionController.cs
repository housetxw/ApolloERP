using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Model.Promotion;
using Ae.C.MiniApp.Api.Core.Request.Promotion;
using Ae.C.MiniApp.Api.Filters;

namespace Ae.C.MiniApp.Api.Controllers
{
    /// <summary>
    /// 商品促销
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(PromotionController))]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _promotionService;

        /// <summary>
        /// 构造
        /// </summary>
        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        /// <summary>
        /// 促销详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<PromotionDetailVo>> GetPromotionDetail([FromQuery]PromotionDetailRequest request)
        {
            var result = await _promotionService.GetPromotionDetail(request);

            return Result.Success(result);
        }
    }
}