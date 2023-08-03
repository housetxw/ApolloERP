using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Response;
using Ae.Shop.Api.Filters;

namespace Ae.Shop.Api.Controllers
{
    /// <summary>
    /// 门店运营
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(ShopOperationController))]
    public class ShopOperationController : ControllerBase
    {
        private readonly IShopOperationService shopOperationService;

        public ShopOperationController(IShopOperationService shopOperationService)
        {
            this.shopOperationService = shopOperationService;
        }

        /// <summary>
        /// 获取门店评价列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<GetOrderCommentForShopVO>> GetOrderCommentForShopList([FromBody]ApiRequest<GetOrderCommentBaseRequest> request)
        {
            return await shopOperationService.GetOrderCommentForShopList(request);
        }

        /// <summary>
        /// 获取评价列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetCommentListResponse>> GetCommentList([FromQuery]GetCommentListRequest request)
        {
            return await shopOperationService.GetCommentList(request);
        }

        /// <summary>
        /// 回评详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ReplyDetailResponse>> ReplyDetail([FromQuery]ReplyDetailRequest request)
        {
            return await shopOperationService.ReplyDetail(request);
        }

        /// <summary>
        /// 回复评价
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> ReplyComment([FromBody]ApiRequest<ReplyCommentRequest> request)
        {
            return await shopOperationService.ReplyComment(request.Data);
        }
    }
}
