using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Response;
using Ae.C.MiniApp.Api.Filters;

namespace Ae.C.MiniApp.Api.Controllers
{
    /// <summary>
    /// 点评
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(CommentController))]
    public class CommentController : ControllerBase
    {
        private readonly IOrderCommentService orderCommentService;
        private readonly ApolloErpLogger<CommentController> logger;
        private readonly IConfiguration _configuration;

        public CommentController(ApolloErpLogger<CommentController> logger,
            IOrderCommentService orderCommentService, IConfiguration configuration
        )
        {
            this.logger = logger;
            this.orderCommentService = orderCommentService;
            _configuration = configuration;
        }


        /// <summary>
        /// 加载评价晒单视图
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<OrderCommentResponse>> OrderComment([FromQuery] OrderCommentRequest request)
        {
            return await orderCommentService.OrderComment(request);
        }

        /// <summary>
        /// 获取点评标签
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<GetCommentLabelsResponse>>> GetCommentLabels(
            [FromQuery] GetCommentLabelsRequest request)
        {
            return await orderCommentService.GetCommentLabels(request);
        }

        /// <summary>
        /// 提交评价晒单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> SubmitOrderComment([FromBody] ApiRequest<SubmitOrderCommentRequest> request)
        {
            logger.Info($"提交评价晒单 data:{JsonConvert.SerializeObject(request)}");
            return await orderCommentService.SubmitOrderComment(request.Data);
        }

        /// <summary>
        /// 我的评价列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<GetMyCommentListResponse>> GetMyCommentList(
            [FromQuery] BasePageRequest request)
        {
            logger.Info($"我的评价列表 data:{JsonConvert.SerializeObject(request)}");
            var result = await orderCommentService.GetMyCommentList(request);
            ApiPagedResult<GetMyCommentListResponse> response = new ApiPagedResult<GetMyCommentListResponse>()
            {
                Code = ResultCode.Success,
                Data = result
            };
            return response;


        }

        /// <summary>
        /// 加载追加点评视图
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<AppendCommentResponse>> AppendComment([FromQuery] AppendCommentRequest request)
        {
            return await orderCommentService.AppendComment(request);
        }

        /// <summary>
        /// 提交追评
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> SubmitAppendComment(
            [FromBody] ApiRequest<SubmitAppendCommentRequest> request)
        {
            logger.Info($"提交追评 data:{JsonConvert.SerializeObject(request.Data)}");
            return await orderCommentService.SubmitAppendComment(request.Data);
        }

        /// <summary>
        /// 点赞喜欢点评
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> LikeComment([FromBody] ApiRequest<LikeCommentRequest> request)
        {
            var result = await orderCommentService.LikeComment(request.Data);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取商品评价数量和好评率
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<BaseCommentNumAndApplauseRateResponse>> GetProductCommentNumAndApplauseRate(
            [FromQuery] BaseGetProductCommentRequest request)
        {
            var result = await orderCommentService.GetProductCommentNumAndApplauseRate(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取商品评价标签列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<BaseCommentLabelListResponse>> GetProductCommentLabelList(
            [FromQuery] GetProductCommentListRequest request)
        {
            var result = await orderCommentService.GetProductCommentLabelList(request);
            return result;
        }

        /// <summary>
        /// 获取商品评价列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<BaseCommentListResponse>> GetProductCommentList(
            [FromQuery] GetProductCommentListRequest request)
        {
            var response = await orderCommentService.GetProductCommentList(request);
            return response;
        }

        /// <summary>
        /// 获取门店评价数量和好评率
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<BaseCommentNumAndApplauseRateResponse>> GetShopCommentNumAndApplauseRate(
            [FromQuery] BaseGetShopCommentRequest request)
        {
            var result = await orderCommentService.GetShopCommentNumAndApplauseRate(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取门店评价标签列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<BaseCommentLabelListResponse>> GetShopCommentLabelList(
            [FromQuery] GetShopCommentListRequest request)
        {
            var result = await orderCommentService.GetShopCommentLabelList(request);
            return result;
        }

        /// <summary>
        /// 获取门店评价列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<BaseCommentListResponse>> GetShopCommentList(
            [FromQuery] GetShopCommentListRequest request)
        {
            var result = await orderCommentService.GetShopCommentList(request);
            return result;
        }

        /// <summary>
        /// 获取技师评价数量和好评率
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<BaseCommentNumAndApplauseRateResponse>> GetTechCommentNumAndApplauseRate(
            [FromQuery] BaseGetTechCommentRequest request)
        {
            var result = await orderCommentService.GetTechCommentNumAndApplauseRate(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取技师评价标签列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<BaseCommentLabelListResponse>> GetTechCommentLabelList(
            [FromQuery] GetTechCommentListRequest request)
        {
            var result = await orderCommentService.GetTechCommentLabelList(request);
            return result;
        }

        /// <summary>
        /// 获取技师评价列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<BaseCommentListResponse>> GetTechCommentList(
            [FromQuery] GetTechCommentListRequest request)
        {
            var response = await orderCommentService.GetTechCommentList(request);
            return response;
        }
    }
}