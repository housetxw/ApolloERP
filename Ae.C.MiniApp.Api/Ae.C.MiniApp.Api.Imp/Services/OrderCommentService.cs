using AutoMapper;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Inteface;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Client.Request.OrderComment;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Imp.Services
{
    public class OrderCommentService : IOrderCommentService
    {
        private readonly IMapper mapper;
        private readonly ApolloErpLogger<OrderCommentService> logger;
        private readonly IOrderCommentClient orderCommentClient;
        private readonly IIdentityService identityService;

        public OrderCommentService(IMapper mapper, ApolloErpLogger<OrderCommentService> logger,
            IOrderCommentClient orderCommentClient,
            IIdentityService identityService
            )
        {
            this.mapper = mapper;
            this.logger = logger;
            this.orderCommentClient = orderCommentClient;
            this.identityService = identityService;
        }

        /// <summary>
        /// 加载评价晒单视图
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<OrderCommentResponse>> OrderComment(OrderCommentRequest request)
        {
            var clientRequest = new OrderCommentClientRequest() 
            {
                OrderNo = request.OrderNo,
                UserId = identityService.GetUserId()
            };
            var clientResponse = await orderCommentClient.OrderComment(clientRequest);
            var result = mapper.Map<ApiResult<OrderCommentResponse>>(clientResponse);
            return result;
        }
        /// <summary>
        /// 获取点评标签
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<GetCommentLabelsResponse>>> GetCommentLabels(GetCommentLabelsRequest request)
        {
            var clientRequest = mapper.Map<GetCommentLabelsClientRequest>(request);
            var clientResponse = await orderCommentClient.GetCommentLabels(clientRequest);
            var result = mapper.Map<ApiResult<List<GetCommentLabelsResponse>>>(clientResponse);
            return result;
        }
        /// <summary>
        /// 提交评价晒单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> SubmitOrderComment(SubmitOrderCommentRequest request)
        {
            var clientRequest = mapper.Map<SubmitOrderCommentClientRequest>(request);
            clientRequest.CreateBy = identityService.GetUserName();
            clientRequest.UserId = identityService.GetUserId();
            return await orderCommentClient.SubmitOrderComment(clientRequest);
        }
        /// <summary>
        /// 我的评价列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<GetMyCommentListResponse>> GetMyCommentList(BasePageRequest request) 
        {
            var clientRequest = new GetMyCommentListClientRequest()
            {
                UserId = identityService.GetUserId(),
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            };
            var clientResponse = await orderCommentClient.GetMyCommentList(clientRequest);
            var result = mapper.Map<ApiPagedResultData<GetMyCommentListResponse>>(clientResponse);
            return result;
        }
        /// <summary>
        /// 点赞喜欢点评
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> LikeComment(LikeCommentRequest request) 
        {
            var clientRequest = mapper.Map<LikeCommentClietRequest>(request);
            return await orderCommentClient.LikeComment(clientRequest);
        }
        /// <summary>
        /// 获取商品评价数量和好评率
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<BaseCommentNumAndApplauseRateResponse> GetProductCommentNumAndApplauseRate(BaseGetProductCommentRequest request) 
        {
            var clientRequest = mapper.Map<BaseGetProductCommentClientRequest>(request);
            var clientResponse = await orderCommentClient.GetProductCommentNumAndApplauseRate(clientRequest);
            var result = mapper.Map<BaseCommentNumAndApplauseRateResponse>(clientResponse);
            return result;
        }
        /// <summary>
        /// 获取商品评价标签列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<BaseCommentLabelListResponse>> GetProductCommentLabelList(GetProductCommentListRequest request)
        {
            var clientRequest = mapper.Map<GetProductCommentListClientRequest>(request);
            var clientResponse = await orderCommentClient.GetProductCommentLabelList(clientRequest);
            var result = mapper.Map<ApiPagedResult<BaseCommentLabelListResponse>>(clientResponse);
            return result;
        }

        /// <summary>
        /// 获取商品评价列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<BaseCommentListResponse>> GetProductCommentList(GetProductCommentListRequest request)
        {
            var clientRequest = mapper.Map<GetProductCommentListClientRequest>(request);
            var clientResponse = await orderCommentClient.GetProductCommentList(clientRequest);
            var result = mapper.Map<ApiPagedResult<BaseCommentListResponse>>(clientResponse);
            return result;
        }

        /// <summary>
        /// 获取技师评价数量和好评率
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<BaseCommentNumAndApplauseRateResponse> GetTechCommentNumAndApplauseRate(BaseGetTechCommentRequest request)
        {
            var clientRequest = mapper.Map<BaseGetTechCommentClientRequest>(request);
            var clientResponse = await orderCommentClient.GetTechCommentNumAndApplauseRate(clientRequest);
            var result = mapper.Map<BaseCommentNumAndApplauseRateResponse>(clientResponse);
            return result;
        }
        /// <summary>
        /// 获取技师评价标签列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<BaseCommentLabelListResponse>> GetTechCommentLabelList(GetTechCommentListRequest request)
        {
            var clientRequest = mapper.Map<GetTechCommentListClientRequest>(request);
            var clientResponse = await orderCommentClient.GetTechCommentLabelList(clientRequest);
            var result = mapper.Map<ApiPagedResult<BaseCommentLabelListResponse>>(clientResponse);
            return result;
        }

        /// <summary>
        /// 获取技师评价列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<BaseCommentListResponse>> GetTechCommentList(GetTechCommentListRequest request)
        {
            var clientRequest = mapper.Map<GetTechCommentListClientRequest>(request);
            var clientResponse = await orderCommentClient.GetTechCommentList(clientRequest);
            var result = mapper.Map<ApiPagedResult<BaseCommentListResponse>>(clientResponse);
            return result;
        }

        /// <summary>
        /// 获取门店评价数量和好评率
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<BaseCommentNumAndApplauseRateResponse> GetShopCommentNumAndApplauseRate(BaseGetShopCommentRequest request)
        {
            var clientRequest = mapper.Map<BaseGetShopCommentClientRequest>(request);
            var clientResponse = await orderCommentClient.GetShopCommentNumAndApplauseRate(clientRequest);
            var result = mapper.Map<BaseCommentNumAndApplauseRateResponse>(clientResponse);
            return result;
        }


        /// <summary>
        /// 获取门店评价标签列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<BaseCommentLabelListResponse>> GetShopCommentLabelList(GetShopCommentListRequest request)
        {
            var clientRequest = mapper.Map<GetShopCommentListClientRequest>(request);
            var clientResponse = await orderCommentClient.GetShopCommentLabelList(clientRequest);
            var result = mapper.Map<ApiPagedResult<BaseCommentLabelListResponse>>(clientResponse);
            return result;
        }

        /// <summary>
        /// 获取门店评价列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<BaseCommentListResponse>> GetShopCommentList(GetShopCommentListRequest request)
        {
            var clientRequest = mapper.Map<GetShopCommentListClientRequest>(request);
            var clientResponse = await orderCommentClient.GetShopCommentList(clientRequest);
            var result = mapper.Map<ApiPagedResult<BaseCommentListResponse>>(clientResponse);
            return result;
        }
        /// <summary>
        /// 加载追加点评视图
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<AppendCommentResponse>> AppendComment(AppendCommentRequest request)
        {
            var clientRequest = mapper.Map<AppendCommentClientRequest>(request);
            var clientResponse = await orderCommentClient.AppendComment(clientRequest);
            var result = mapper.Map<ApiResult<AppendCommentResponse>>(clientResponse);
            return result;
        }
        /// <summary>
        /// 提交追评
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> SubmitAppendComment(SubmitAppendCommentRequest request)
        {
            var clientRequest = mapper.Map<SubmitAppendCommentClientRequest>(request);
            clientRequest.UserId = identityService.GetUserId();
            clientRequest.CreateBy = identityService.GetUserName();
            var clientResponse = await orderCommentClient.SubmitAppendComment(clientRequest);
            return clientResponse;
        }
    }
}
