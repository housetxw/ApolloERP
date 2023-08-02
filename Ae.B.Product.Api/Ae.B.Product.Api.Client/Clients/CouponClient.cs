using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Client.Interfaces;
using Ae.B.Product.Api.Client.Model.Coupon;
using Ae.B.Product.Api.Client.Request.Coupon;
using Ae.B.Product.Api.Common.Exceptions;
using Ae.B.Product.Api.Core.Model.Coupon;
using Ae.B.Product.Api.Core.Request.Coupon;
using Ae.B.Product.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Product.Api.Client.Clients
{
    public class CouponClient : ICouponClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ApolloErpLogger<CouponClient> _logger;

        public CouponClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ApolloErpLogger<CouponClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<ApiPagedResultData<UserCouponPageResByUserIdDto>> GetUserCouponPageByUserId(
            UserCouponPageByUserIdClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("CouponServer");
            ApiPagedResult<UserCouponPageResByUserIdDto> result =
                await client
                    .PostAsJsonAsync<UserCouponPageByUserIdClientRequest, ApiPagedResult<UserCouponPageResByUserIdDto>>(
                        _configuration["CouponServer:GetUserCouponPageByUserId"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"GetUserCouponPageByUserId_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 优惠券配置查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<CouponRuleDto>> GetCouponRuleList(GetCouponRuleListClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("CouponServer");
            ApiPagedResult<CouponRuleDto> result =
                await client.GetAsJsonAsync<GetCouponRuleListClientRequest, ApiPagedResult<CouponRuleDto>>(
                    _configuration["CouponServer:GetCouponRuleList"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"GetCouponRuleList_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 添加优惠券配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<long> AddCouponRule(AddCouponRuleClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("CouponServer");
            ApiResult<long> result =
                await client.PostAsJsonAsync<AddCouponRuleClientRequest, ApiResult<long>>(
                    _configuration["CouponServer:AddCouponRule"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"AddCouponRule_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 优惠券活动配置查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<CouponActivityDto>> GetCouponActivityList(
            GetCouponActivityListClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("CouponServer");
            ApiPagedResult<CouponActivityDto> result =
                await client.GetAsJsonAsync<GetCouponActivityListClientRequest, ApiPagedResult<CouponActivityDto>>(
                    _configuration["CouponServer:GetCouponActivityList"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"GetCouponActivityList_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 新增活动配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<long> AddCouponActivity(AddCouponActivityClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("CouponServer");
            ApiResult<long> result =
                await client.PostAsJsonAsync<AddCouponActivityClientRequest, ApiResult<long>>(
                    _configuration["CouponServer:AddCouponActivity"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"AddCouponActivity_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 编辑活动配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditCouponActivity(EditCouponActivityClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("CouponServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<EditCouponActivityClientRequest, ApiResult<bool>>(
                    _configuration["CouponServer:EditCouponActivity"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"EditCouponActivity_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 更新活动状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> UpdateCouponActivityStatus(UpdateCouponActivityStatusClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("CouponServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<UpdateCouponActivityStatusClientRequest, ApiResult<bool>>(
                    _configuration["CouponServer:UpdateCouponActivityStatus"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"UpdateCouponActivityStatus_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 活动详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CouponActivityDetailDto> GetCouponActivityDetail(CouponActivityDetailClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("CouponServer");
            ApiResult<CouponActivityDetailDto> result =
                await client.GetAsJsonAsync<CouponActivityDetailClientRequest, ApiResult<CouponActivityDetailDto>>(
                    _configuration["CouponServer:GetCouponActivityDetail"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"GetCouponActivityDetail_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 优惠券发放记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<UserCouponGrantRecordDto>> GetUserCouponGrantRecord(
            UserCouponGrantRecordClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("CouponServer");
            ApiPagedResult<UserCouponGrantRecordDto> result =
                await client
                    .GetAsJsonAsync<UserCouponGrantRecordClientRequest, ApiPagedResult<UserCouponGrantRecordDto>>(
                        _configuration["CouponServer:GetUserCouponGrantRecord"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"GetUserCouponGrantRecord_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 用户塞券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> GrantUserCoupon(GrantUserCouponClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("CouponServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<GrantUserCouponClientRequest, ApiResult<bool>>(
                    _configuration["CouponServer:GrantUserCoupon"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"GrantUserCoupon_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 作废优惠券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> InvalidatedUserCoupon(InvalidatedUserCouponClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("CouponServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<InvalidatedUserCouponClientRequest, ApiResult<bool>>(
                    _configuration["CouponServer:InvalidatedUserCoupon"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"InvalidatedUserCoupon_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 延期优惠券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DelayUserCoupon(DelayUserCouponClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("CouponServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<DelayUserCouponClientRequest, ApiResult<bool>>(
                    _configuration["CouponServer:DelayUserCoupon"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"DelayUserCoupon_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 更新发放总数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> UpdateActivityTotalNum(UpdateActivityTotalNumClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("CouponServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<UpdateActivityTotalNumClientRequest, ApiResult<bool>>(
                    _configuration["CouponServer:UpdateActivityTotalNum"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"UpdateActivityTotalNum_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 开瓶有奖记录查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<DecapAwardDetailDto>> SearchDecapAward(
            SearchDecapAwardClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("CouponServer");
            ApiPagedResult<DecapAwardDetailDto> result =
                await client.GetAsJsonAsync<SearchDecapAwardClientRequest, ApiPagedResult<DecapAwardDetailDto>>(
                    _configuration["CouponServer:SearchDecapAward"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"SearchDecapAward_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 消费赠券活动规则列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<GiftCouponRuleDto>> GetGiftCouponRulePageList(
            GiftCouponRulePageListClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("CouponServer");
            ApiPagedResult<GiftCouponRuleDto> result =
                await client.GetAsJsonAsync<GiftCouponRulePageListClientRequest, ApiPagedResult<GiftCouponRuleDto>>(
                    _configuration["CouponServer:GetGiftCouponRulePageList"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"GetGiftCouponRulePageList_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 消费赠券活动规则详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GiftCouponRuleDetailDto> GetGiftCouponRuleDetail(GiftCouponRuleDetailClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("CouponServer");
            ApiResult<GiftCouponRuleDetailDto> result =
                await client.GetAsJsonAsync<GiftCouponRuleDetailClientRequest, ApiResult<GiftCouponRuleDetailDto>>(
                    _configuration["CouponServer:GetGiftCouponRuleDetail"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"GetGiftCouponRuleDetail_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 新增赠券规则
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddGiftCouponRule(AddGiftCouponRuleClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("CouponServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<AddGiftCouponRuleClientRequest, ApiResult<bool>>(
                    _configuration["CouponServer:AddGiftCouponRule"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"AddGiftCouponRule_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 编辑赠券规则
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditGiftCouponRule(EditGiftCouponRuleClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("CouponServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<EditGiftCouponRuleClientRequest, ApiResult<bool>>(
                    _configuration["CouponServer:EditGiftCouponRule"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"EditGiftCouponRule_Error {msg}");
                throw new CustomException(msg);
            }
        }

        public async Task<ApiPagedResult<GetCouponActivityListForShopResponse>> GetCouponActivityListForShop(ApiRequest<GetCouponActivityListForShopRequest> request)
        {
            var client = _httpClientFactory.CreateClient("CouponServer");
           return
                await client.PostAsJsonAsync<ApiRequest<GetCouponActivityListForShopRequest>, ApiPagedResult<GetCouponActivityListForShopResponse>>(
                    _configuration["CouponServer:GetCouponActivityListForShop"], request);
        }

        public async Task<ApiResult<bool>> SaveShopGrantCoupon(ApiRequest<ShopGrantCouponDTO> request)
        {
            var client = _httpClientFactory.CreateClient("CouponServer");
            return
                 await client.PostAsJsonAsync<ApiRequest<ShopGrantCouponDTO>, ApiResult<bool>>(
                     _configuration["CouponServer:SaveShopGrantCoupon"], request);
        }
    }
}
