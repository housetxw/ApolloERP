using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model.Promotion;
using Ae.Shop.Api.Core.Request.Promotion;

namespace Ae.Shop.Api.Controllers
{
    /// <summary>
    /// 美容团购
    /// </summary>
    [Route("[controller]/[action]")]
    public class GrouponController : ControllerBase
    {
        private readonly IPromotionService _promotionService;

        /// <summary>
        /// /构造
        /// </summary>
        /// <param name="promotionService"></param>
        public GrouponController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        /// <summary>
        /// 查询门店美容团购产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<GrouponProductPageVo>> GetGrouponProductPageList(
            [FromQuery] GrouponProductPageListRequest request)
        {
            var result = await _promotionService.GetGrouponProductPageList(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 美容团购产品详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GrouponProductPageVo>> GetGrouponProductDetail(
            [FromQuery] GrouponProductDetailRequest request)
        {
            var result = await _promotionService.GetGrouponProductDetail(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 保存门店美容团购产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> SaveShopGrouponProduct(
            [FromBody] ApiRequest<SaveShopGrouponProductRequest> request)
        {
            var result = await _promotionService.SaveShopGrouponProduct(request.Data);

            return Result.Success(result);
        }
    }
}