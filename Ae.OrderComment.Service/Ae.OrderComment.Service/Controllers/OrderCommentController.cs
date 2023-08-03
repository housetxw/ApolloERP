using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.OrderComment.Service.Core.Interfaces;
using Ae.OrderComment.Service.Core.Model;
using Ae.OrderComment.Service.Core.Request;
using Ae.OrderComment.Service.Core.Response;
using Ae.OrderComment.Service.Filters;

namespace Ae.OrderComment.Service.Controllers
{

    /// <summary>
    /// 订单评论
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(OrderCommentController))]
    public class OrderCommentController : ControllerBase
    {
        public IOrderCommentService orderCommentService;
        private readonly string _constMessage = "无数据";
        private readonly ApolloErpLogger<OrderCommentController> logger;
        public OrderCommentController(IOrderCommentService orderCommentService,
            ApolloErpLogger<OrderCommentController> logger
            )
        {
            this.orderCommentService = orderCommentService;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<ApiPagedResult<GetOrderCommentForProductDTO>> GetOrderCommentForProductList([FromBody]ApiRequest<GetOrderCommentForProductRequest> request)
        {
            var result = await orderCommentService.GetOrderCommentForProductList(request.Data);

            ApiPagedResult<GetOrderCommentForProductDTO> response = new ApiPagedResult<GetOrderCommentForProductDTO>()
            {
                Code = ResultCode.Success,
                Data = result
            };
            response.Message = "查询成功!";
            if (response.Data == null && response.Code == ResultCode.Success)
            {
                response.Message = _constMessage;
                response.Code = ResultCode.SUCCESS_NOT_EXIST;
            }
            return response;
        }

        [HttpPost]
        public async Task<ApiPagedResult<GetOrderCommentForShopDTO>> GetOrderCommentForShopList([FromBody]ApiRequest<GetOrderCommentForShopRequest> request)
        {
            var result = await orderCommentService.GetOrderCommentForShopList(request.Data);

            ApiPagedResult<GetOrderCommentForShopDTO> response = new ApiPagedResult<GetOrderCommentForShopDTO>()
            {
                Code = ResultCode.Success,
                Data = result
            };
            response.Message = "查询成功!";
            if (response.Data == null && response.Code == ResultCode.Success)
            {
                response.Message = _constMessage;
                response.Code = ResultCode.SUCCESS_NOT_EXIST;
            }
            return response;
        }

        [HttpPost]
        public async Task<ApiPagedResult<GetOrderCommentForTechDTO>> GetOrderCommentForTechList([FromBody]ApiRequest<GetOrderCommentForTechRequest> request)
        {
            var result = await orderCommentService.GetOrderCommentForTechList(request.Data);

            ApiPagedResult<GetOrderCommentForTechDTO> response = new ApiPagedResult<GetOrderCommentForTechDTO>()
            {
                Code = ResultCode.Success,
                Data = result
            };
            response.Message = "查询成功!";
            if (response.Data == null && response.Code == ResultCode.Success)
            {
                response.Message = _constMessage;
                response.Code = ResultCode.SUCCESS_NOT_EXIST;
            }
            return response;
        }

        [HttpGet]
        public async Task<ApiResult<List<CommentImageDTO>>> GetCommentImages([FromQuery]GetCommentImageRequest request)
        {
            var result = await orderCommentService.GetCommentImages(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 客户初评审核
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> CheckOrderComment([FromBody]ApiRequest<CheckOrderCommentRequest> request)
        {
            var result = await orderCommentService.CheckOrderComment(request.Data);

            return Result.Success("审核成功!");
        }

        /// <summary>
        /// 门店回评，客户追评，门店追评审核
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> CheckOrderReply([FromBody]ApiRequest<CheckOrderCommentRequest> request)
        {
            var result = await orderCommentService.CheckOrderReply(request.Data);

            return Result.Success("审核成功!");
        }

        [HttpGet]
        public async Task<ApiResult<List<BasicInfoDTO>>> GetBaseInfos([FromQuery]BasicInfoRequest request)
        {
            var result = await orderCommentService.GetBaseInfos(request);
            return Result.Success(result);
        }

        [HttpPost]
        public async Task<ApiPagedResult<GetOrderCommentForReplyDTO>> GetOrderCommentForReplyList([FromBody]ApiRequest<GetOrderCommentForReplyRequest> request)
        {
            var result = await orderCommentService.GetOrderCommentForReplyList(request.Data);

            ApiPagedResult<GetOrderCommentForReplyDTO> response = new ApiPagedResult<GetOrderCommentForReplyDTO>()
            {
                Code = ResultCode.Success,
                Data = result
            };
            response.Message = "查询成功!";

            if (response.Data == null && response.Code == ResultCode.Success)
            {
                response.Message = _constMessage;
                response.Code = ResultCode.SUCCESS_NOT_EXIST;
            }
            return response;
        }

        /// <summary>
        /// 获取商品评论数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<ProductCommentTotalDTO>>> GetProductCommentTotalAsync([FromBody] GetProductCommentTotal request)
        {
            var result = await orderCommentService.GetProductCommentTotal(request);
            return Result.Success(result);
        }
        /// <summary>
        /// 加载评价晒单视图
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<OrderCommentResponse>> OrderComment([FromQuery]OrderCommentRequest request)
        {
            return await orderCommentService.OrderComment(request);
        }

        /// <summary>
        /// 获取点评标签
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<GetCommentLabelsResponse>>> GetCommentLabels([FromQuery]GetCommentLabelsRequest request)
        {
            var result = await orderCommentService.GetCommentLabels(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 提交评价晒单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> SubmitOrderComment([FromBody] SubmitOrderCommentRequest request)
        {
            logger.Info($"提交评价晒单 data: {JsonConvert.SerializeObject(request)}");
            return await orderCommentService.SubmitOrderComment(request);
        }

        /// <summary>
        /// 我的评价列表
        /// </summary>
        /// <param name = "request" ></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<GetMyCommentListResponse>> GetMyCommentList([FromQuery] GetMyCommentListRequest request)
        {
            var result = await orderCommentService.GetMyCommentList(request);
            ApiPagedResult<GetMyCommentListResponse> response = new ApiPagedResult<GetMyCommentListResponse>()
            {
                Code = ResultCode.Success,
                Data = result
            };
            return response;
        }

        /// <summary>
        /// 点赞喜欢点评
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> LikeComment([FromBody]LikeCommentRequest request)
        {
            var result = await orderCommentService.LikeComment(request);
            return Result.Success(result);
        }
        /// <summary>
        /// 获取商品评价数量和好评率
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<BaseCommentNumAndApplauseRateResponse>> GetProductCommentNumAndApplauseRate([FromQuery]BaseGetProductCommentRequest request)
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
        public async Task<ApiPagedResult<BaseCommentLabelListResponse>> GetProductCommentLabelList([FromQuery]GetProductCommentListRequest request)
        {
            var result = await orderCommentService.GetProductCommentLabelList(request);
            ApiPagedResult<BaseCommentLabelListResponse> response = new ApiPagedResult<BaseCommentLabelListResponse>()
            {
                Code = ResultCode.Success,
                Data = result
            };
            return response;
        }
        /// <summary>
        /// 获取商品评价列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<BaseCommentListResponse>> GetProductCommentList([FromQuery]GetProductCommentListRequest request)
        {
            var result = await orderCommentService.GetProductCommentList(request);
            ApiPagedResult<BaseCommentListResponse> response = new ApiPagedResult<BaseCommentListResponse>()
            {
                Code = ResultCode.Success,
                Data = result
            };
            return response;
        }

        /// <summary>
        /// 获取门店评价数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<BaseCommentNumAndApplauseRateResponse>> GetShopCommentNumAndApplauseRate([FromQuery]BaseGetShopCommentRequest request)
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
        public async Task<ApiPagedResult<BaseCommentLabelListResponse>> GetShopCommentLabelList([FromQuery]GetShopCommentListRequest request)
        {
            var result = await orderCommentService.GetShopCommentLabelList(request);
            ApiPagedResult<BaseCommentLabelListResponse> response = new ApiPagedResult<BaseCommentLabelListResponse>()
            {
                Code = ResultCode.Success,
                Data = result
            };
            return response;
        }

        /// <summary>
        /// 获取门店评价列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<BaseCommentListResponse>> GetShopCommentList([FromQuery]GetShopCommentListRequest request)
        {
            var result = await orderCommentService.GetShopCommentList(request);
            ApiPagedResult<BaseCommentListResponse> response = new ApiPagedResult<BaseCommentListResponse>()
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
        public async Task<ApiResult<AppendCommentResponse>> AppendComment([FromQuery]AppendCommentRequest request)
        {
            var result = await orderCommentService.AppendComment(request);
            return Result.Success(result);
        }
        /// <summary>
        /// 提交追评
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> SubmitAppendComment([FromBody]SubmitAppendCommentRequest request)
        {
            var result = await orderCommentService.SubmitAppendComment(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 每日评论统计
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> DailyCommentStatistics()
        {
            return await orderCommentService.DailyCommentStatistics();
        }

        /// <summary>
        /// 获取门店评分
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<ShopScoreVo>>> GetShopScore([FromBody] ShopScoreRequest request)
        {
            var result = await orderCommentService.GetShopScore(request);

            return Result.Success(result);
        }
    }
}