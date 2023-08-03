using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Inteface;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Client.Request.OrderComment;
using Ae.C.MiniApp.Api.Client.Response;
using Ae.C.MiniApp.Api.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Ae.C.MiniApp.Api.Client.Model.OrderComment;
using ApolloErp.Log;
using Ae.C.MiniApp.Api.Common.Exceptions;

namespace Ae.C.MiniApp.Api.Client.Clients.OrderComment
{
    public class OrderCommentClient : IOrderCommentClient
    {
        private readonly ApolloErpLogger<OrderCommentClient> _logger;
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }


        public OrderCommentClient(IHttpClientFactory clientFactory, 
            IConfiguration configuration,
            ApolloErpLogger<OrderCommentClient> logger
            )
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// 加载评价晒单视图
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<OrderCommentClientResponse>> OrderComment(OrderCommentClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderCommentServer");
            ApiResult<OrderCommentClientResponse> result = 
                await client.GetAsJsonAsync<OrderCommentClientRequest, ApiResult<OrderCommentClientResponse>>(configuration["OrderCommentServer:OrderComment"], request);
            return result;
        }

        /// <summary>
        /// 获取点评标签
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<GetCommentLabelsClientResponse>>> GetCommentLabels(GetCommentLabelsClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderCommentServer");
            ApiResult<List<GetCommentLabelsClientResponse>> result = 
                await client.GetAsJsonAsync<GetCommentLabelsClientRequest, ApiResult<List<GetCommentLabelsClientResponse>>>(configuration["OrderCommentServer:GetCommentLabels"], request);
            return result;
        }

        /// <summary>
        /// 提交评价晒单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> SubmitOrderComment(SubmitOrderCommentClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderCommentServer");
            ApiResult result =
                await client.PostAsJsonAsync<SubmitOrderCommentClientRequest, ApiResult>(configuration["OrderCommentServer:SubmitOrderComment"], request);
            return result;
        }

        /// <summary>
        /// 商品评论数查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ProductCommentTotalDto>> GetProductCommentTotalAsync(
            ProductCommentTotalClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderCommentServer");
            ApiResult<List<ProductCommentTotalDto>> result =
                await client.PostAsJsonAsync<ProductCommentTotalClientRequest, ApiResult<List<ProductCommentTotalDto>>>(
                    configuration["OrderCommentServer:GetProductCommentTotalAsync"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<ProductCommentTotalDto>();
            }
            else
            {
                _logger.Warn($"GetProductCommentTotalAsync_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }

        /// <summary>
        /// 我的评价列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<GetMyCommentListClientResponse>> GetMyCommentList(GetMyCommentListClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderCommentServer");
            ApiPagedResult<GetMyCommentListClientResponse> result = await client.GetAsJsonAsync<GetMyCommentListClientRequest, ApiPagedResult<GetMyCommentListClientResponse>>(configuration["OrderCommentServer:GetMyCommentList"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Warn($"GetMyCommentList_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }

        /// <summary>
        /// 点赞喜欢点评
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> LikeComment(LikeCommentClietRequest request)
        {
            var client = clientFactory.CreateClient("OrderCommentServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<LikeCommentClietRequest, ApiResult<bool>>(configuration["OrderCommentServer:LikeComment"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Warn($"LikeComment_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }
        /// <summary>
        /// 获取商品评价数量和好评率
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<BaseCommentNumAndApplauseRateClientResponse> GetProductCommentNumAndApplauseRate(BaseGetProductCommentClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderCommentServer");
            ApiResult<BaseCommentNumAndApplauseRateClientResponse> result =
                await client.GetAsJsonAsync<BaseGetProductCommentClientRequest, ApiResult<BaseCommentNumAndApplauseRateClientResponse>>(configuration["OrderCommentServer:GetProductCommentNumAndApplauseRate"], request);
            if (result.IsNotNullSuccess()) 
            {
                return result.Data;
            }
            else
            {
                _logger.Warn($"GetProductCommentNumAndApplauseRate_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }

        /// <summary>
        /// 获取商品评价标签列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<BaseCommentLabelListClientResponse>> GetProductCommentLabelList(GetProductCommentListClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderCommentServer");
            ApiPagedResult<BaseCommentLabelListClientResponse> result =
                await client.GetAsJsonAsync<GetProductCommentListClientRequest, ApiPagedResult<BaseCommentLabelListClientResponse>>(configuration["OrderCommentServer:GetProductCommentLabelList"], request);
            if (result.IsNotNullSuccess())
            {
                return result;
            }
            else
            {
                _logger.Warn($"GetProductCommentLabelList_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }

        /// <summary>
        /// 获取商品评价列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<BaseCommentListClientResponse>> GetProductCommentList(GetProductCommentListClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderCommentServer");
            ApiPagedResult<BaseCommentListClientResponse> result =
                await client.GetAsJsonAsync<GetProductCommentListClientRequest, ApiPagedResult<BaseCommentListClientResponse>>(configuration["OrderCommentServer:GetProductCommentList"], request);
            if (result.IsNotNullSuccess())
            {
                return result;
            }
            else
            {
                _logger.Warn($"GetProductCommentList_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }

        /// <summary>
        /// 获取技师评价数量和好评率
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<BaseCommentNumAndApplauseRateClientResponse> GetTechCommentNumAndApplauseRate(BaseGetTechCommentClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderCommentServer");
            ApiResult<BaseCommentNumAndApplauseRateClientResponse> result =
                await client.GetAsJsonAsync<BaseGetTechCommentClientRequest, ApiResult<BaseCommentNumAndApplauseRateClientResponse>>(configuration["OrderCommentServer:GetTechCommentNumAndApplauseRate"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Warn($"GetTechCommentNumAndApplauseRate {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }

        /// <summary>
        /// 获取技师评价标签列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<BaseCommentLabelListClientResponse>> GetTechCommentLabelList(GetTechCommentListClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderCommentServer");
            ApiPagedResult<BaseCommentLabelListClientResponse> result =
                await client.GetAsJsonAsync<GetTechCommentListClientRequest, ApiPagedResult<BaseCommentLabelListClientResponse>>(configuration["OrderCommentServer:GetTechCommentLabelList"], request);
            if (result.IsNotNullSuccess())
            {
                return result;
            }
            else
            {
                _logger.Warn($"GetTechCommentLabelList {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }

        /// <summary>
        /// 获取技师评价列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<BaseCommentListClientResponse>> GetTechCommentList(GetTechCommentListClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderCommentServer");
            ApiPagedResult<BaseCommentListClientResponse> result =
                await client.GetAsJsonAsync<GetTechCommentListClientRequest, ApiPagedResult<BaseCommentListClientResponse>>(configuration["OrderCommentServer:GetTechCommentList"], request);
            if (result.IsNotNullSuccess())
            {
                return result;
            }
            else
            {
                _logger.Warn($"GetTechCommentList {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }

        /// <summary>
        /// 获取门店评价数量和好评率
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<BaseCommentNumAndApplauseRateClientResponse> GetShopCommentNumAndApplauseRate(BaseGetShopCommentClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderCommentServer");
            ApiResult<BaseCommentNumAndApplauseRateClientResponse> result =
                await client.GetAsJsonAsync<BaseGetShopCommentClientRequest, ApiResult<BaseCommentNumAndApplauseRateClientResponse>>(configuration["OrderCommentServer:GetShopCommentNumAndApplauseRate"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Warn($"GetShopCommentNumAndApplauseRate_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }

        /// <summary>
        /// 获取门店评价标签列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<BaseCommentLabelListClientResponse>> GetShopCommentLabelList(GetShopCommentListClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderCommentServer");
            ApiPagedResult<BaseCommentLabelListClientResponse> result =
                await client.GetAsJsonAsync<GetShopCommentListClientRequest, ApiPagedResult<BaseCommentLabelListClientResponse>>(configuration["OrderCommentServer:GetShopCommentLabelList"], request);
            if (result.IsNotNullSuccess())
            {
                return result;
            }
            else
            {
                _logger.Warn($"GetShopCommentLabelList_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }


        /// <summary>
        /// 获取门店评价列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<BaseCommentListClientResponse>> GetShopCommentList(GetShopCommentListClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderCommentServer");
            ApiPagedResult<BaseCommentListClientResponse> result =
                await client.GetAsJsonAsync<GetShopCommentListClientRequest, ApiPagedResult<BaseCommentListClientResponse>>(configuration["OrderCommentServer:GetShopCommentList"], request);
            if (result.IsNotNullSuccess())
            {
                return result;
            }
            else
            {
                _logger.Warn($"GetShopCommentList_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }

        /// <summary>
        /// 加载追加点评视图
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<AppendCommentClientResponse>> AppendComment(AppendCommentClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderCommentServer");
            ApiResult<AppendCommentClientResponse> result =
                await client.GetAsJsonAsync<AppendCommentClientRequest, ApiResult<AppendCommentClientResponse>>(configuration["OrderCommentServer:AppendComment"], request);
            return result;
        }

        /// <summary>
        /// 提交追评
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> SubmitAppendComment(SubmitAppendCommentClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderCommentServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<SubmitAppendCommentClientRequest, ApiResult<bool>>(configuration["OrderCommentServer:SubmitAppendComment"], request);
            if (result.IsNotNullSuccess())
            {
                return result;
            }
            else
            {
                _logger.Warn($"SubmitAppendComment_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }
    }
}
