using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Client.Model.Coupon;
using Ae.C.MiniApp.Api.Client.Request.Coupon;
using Ae.C.MiniApp.Api.Client.Response.Coupon;
using Ae.C.MiniApp.Api.Common.Constant;
using Ae.C.MiniApp.Api.Common.Exceptions;
using Ae.C.MiniApp.Api.Core.Model.Coupon;
using Ae.C.MiniApp.Api.Core.Request.Coupon;

namespace Ae.C.MiniApp.Api.Client.Clients.Coupon
{
    public class CouponClient : ICouponClient
    {
        #region 变量和构造器

        private readonly HttpClient client;
        private IConfiguration configuration { get; }

        private readonly ApolloErpLogger<CouponClient> logger;
        private readonly string className;

        public CouponClient(ApolloErpLogger<CouponClient> logger, IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.configuration = configuration;
            client = clientFactory.CreateClient("CouponServer");

            this.logger = logger;
            className = this.GetType().Name;
        }

        #endregion 变量和构造器

        #region UserCoupon

        public async Task<ApiPagedResult<UserCouponPageResByUserIdDTO>> GetUserCouponPageByUserId(UserCouponPageReqByUserIdDTO req)
        {
            ApiPagedResult<UserCouponPageResByUserIdDTO> res = null;
            try
            {
                logger.Info($"API: CouponServer:GetUserCouponPageByUserId 请求参数：{JsonConvert.SerializeObject(req)}");

                res = await client.PostAsJsonAsync<UserCouponPageReqByUserIdDTO, ApiPagedResult<UserCouponPageResByUserIdDTO>>(configuration["CouponServer:GetUserCouponPageByUserId"], req);

                logger.Info($"API: CouponServer:GetUserCouponPageByUserId 返回值：{JsonConvert.SerializeObject(res)}");

                if (res.Code != ResultCode.Success)
                {
                    var msg = $"{CommonConstant.ResultCodeFailed}\n" +
                              $"{CommonConstant.ParameterReqDetail}:\n{JsonConvert.SerializeObject(res)}";
                    logger.Warn(msg);
                }
            }
            catch (Exception e)
            {
                var msg = $"{CommonConstant.ResultCodeException}\n" +
                          $"{CommonConstant.ParameterReqDetail}:\n{JsonConvert.SerializeObject(req)}";
                logger.Error(msg, e);
            }
            return res;
        }

        public async Task<ApiResult<bool>> ExchangeCouponByPromotionCode(ApiRequest<ExchangeCouponReqByCodeDTO> req)
        {
            ApiResult<bool> res = null;
            try
            {
                logger.Info($"API: CouponServer:ExchangeCouponByPromotionCode 请求参数：{JsonConvert.SerializeObject(req)}");

                res = await client.PostAsJsonAsync<ApiRequest<ExchangeCouponReqByCodeDTO>, ApiResult<bool>>(configuration["CouponServer:ExchangeCouponByPromotionCode"], req);

                logger.Info($"API: CouponServer:ExchangeCouponByPromotionCode 返回值：{JsonConvert.SerializeObject(res)}");

                if (res.Code != ResultCode.Success)
                {
                    var msg = $"{CommonConstant.ResultCodeFailed}\n" +
                              $"{CommonConstant.ParameterReqDetail}:\n{JsonConvert.SerializeObject(res)}";
                    logger.Warn(msg);
                }
            }
            catch (Exception e)
            {
                var msg = $"{CommonConstant.ResultCodeException}\n" +
                          $"{CommonConstant.ParameterReqDetail}:\n{JsonConvert.SerializeObject(req)}";
                logger.Error(msg, e);
            }
            return res;
        }

        public async Task<ApiPagedResult<CouponActivityPageResDTO>> GetIntegralCouponActivityPage(CouponActivityPageReqByChannelDTO req)
        {
            ApiPagedResult<CouponActivityPageResDTO> res = null;
            try
            {
                logger.Info($"API: CouponServer:GetIntegralCouponActivityPage 请求参数：{JsonConvert.SerializeObject(req)}");

                res = await client.GetAsJsonAsync<CouponActivityPageReqByChannelDTO, ApiPagedResult<CouponActivityPageResDTO>>(configuration["CouponServer:GetIntegralCouponActivityPage"], req);

                logger.Info($"API: CouponServer:GetIntegralCouponActivityPage 返回值：{JsonConvert.SerializeObject(res)}");

                if (res.Code != ResultCode.Success)
                {
                    var msg = $"{CommonConstant.ResultCodeFailed}\n" +
                              $"{CommonConstant.ParameterReqDetail}:\n{JsonConvert.SerializeObject(res)}";
                    logger.Warn(msg);
                }
            }
            catch (Exception e)
            {
                var msg = $"{CommonConstant.ResultCodeException}\n" +
                          $"{CommonConstant.ParameterReqDetail}:\n{JsonConvert.SerializeObject(req)}";
                logger.Error(msg, e);
            }
            return res;
        }

        public async Task<ApiResult<bool>> IntegralExchangeCoupon(ApiRequest<IntegralExchangeCouponReqDTO> req)
        {
            ApiResult<bool> res = null;
            try
            {
                logger.Info($"API: CouponServer:IntegralExchangeCoupon 请求参数：{JsonConvert.SerializeObject(req)}");

                res = await client.PostAsJsonAsync<ApiRequest<IntegralExchangeCouponReqDTO>, ApiResult<bool>>(configuration["CouponServer:IntegralExchangeCoupon"], req);

                logger.Info($"API: CouponServer:IntegralExchangeCoupon 返回值：{JsonConvert.SerializeObject(res)}");

                if (res.Code != ResultCode.Success)
                {
                    var msg = $"{CommonConstant.ResultCodeFailed}\n" +
                              $"{CommonConstant.ParameterReqDetail}:\n{JsonConvert.SerializeObject(res)}";
                    logger.Warn(msg);
                }
            }
            catch (Exception e)
            {
                var msg = $"{CommonConstant.ResultCodeException}\n" +
                          $"{CommonConstant.ParameterReqDetail}:\n{JsonConvert.SerializeObject(req)}";
                logger.Error(msg, e);
            }
            return res;
        }

        /// <summary>
        ///  通过活动领取优惠券【一般活动：不需要积分，不需要兑换码】
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> ExchangeCouponByActId(ExchangeCouponByActIdClientRequest request)
        {
            ApiResult<bool> result =
                await client.PostAsJsonAsync<ExchangeCouponByActIdClientRequest, ApiResult<bool>>(
                    configuration["CouponServer:ExchangeCouponByActId"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                logger.Warn($"ExchangeCouponByActId_Error {msg}  Para={JsonConvert.SerializeObject(request)}");
                throw new CustomException(msg);
            }
        }


        #endregion

        #region CouponActivity

        /// <summary>
        /// 兑换码查询优惠券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CouponActivityByCodeClientResponse> GetCouponActivityByCode(
            CouponActivityByCodeClientRequest request)
        {
            ApiResult<CouponActivityByCodeClientResponse> result =
                await client
                    .GetAsJsonAsync<CouponActivityByCodeClientRequest, ApiResult<CouponActivityByCodeClientResponse>>(
                        configuration["CouponServer:GetCouponActivityByCode"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                logger.Warn($"GetCouponActivityByCode_Error {msg}");
                throw new CustomException(msg);
            }
        }

        #endregion

        #region CouponRule

        #endregion

        /// <summary>
        /// 用户可用优惠券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> GetUserCouponUnusedAmountByUserId(UserCouponUnusedAmountByUserIdRequest request)
        {
            ApiResult<int> result =
                await client.GetAsJsonAsync<UserCouponUnusedAmountByUserIdRequest, ApiResult<int>>(
                    configuration["CouponServer:GetUserCouponUnusedAmountByUserId"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                logger.Error($"GetUserCouponUnusedAmountByUserId_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async Task<ApiResult<List<CouponProductVo>>> GetRecommendProductsForDiamondMembership(RecommendProductsForDiamondMembershipRequest request)
        {
            var response = await client.GetAsJsonAsync<RecommendProductsForDiamondMembershipRequest, ApiResult<List<CouponProductVo>>>(configuration["CouponServer:GetRecommendProductsForDiamondMembership"], request);
            return response;
        }

        /// <summary>
        /// 查询开瓶有奖信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<DecapAwardDto> GetDecapAwardByCode(DecapAwardByCodeClientRequest request)
        {
            ApiResult<DecapAwardDto> result =
                await client.GetAsJsonAsync<DecapAwardByCodeClientRequest, ApiResult<DecapAwardDto>>(
                    configuration["CouponServer:GetDecapAwardByCode"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                logger.Error($"GetDecapAwardByCode_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 领取奖励
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DrawDecapAward(DrawDecapAwardClientRequest request)
        {
            ApiResult<bool> result =
                await client.PostAsJsonAsync<DrawDecapAwardClientRequest, ApiResult<bool>>(
                    configuration["CouponServer:DrawDecapAward"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                logger.Info($"DrawDecapAward_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 根据活动获取优惠券详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CouponDetailByActIdClientResponse> GetCouponDetailByActId(
            CouponDetailByActIdClientRequest request)
        {
            ApiResult<CouponDetailByActIdClientResponse> result =
                await client
                    .GetAsJsonAsync<CouponDetailByActIdClientRequest, ApiResult<CouponDetailByActIdClientResponse>>(
                        configuration["CouponServer:GetCouponDetailByActId"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                logger.Info($"GetCouponDetailByActId_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 根据产品获取可领取的优惠券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetCouponListByProductClientResponse> GetCouponListByProduct(
            CouponListByProductClientRequest request)
        {
            ApiResult<GetCouponListByProductClientResponse> result =
                await client
                    .GetAsJsonAsync<CouponListByProductClientRequest, ApiResult<GetCouponListByProductClientResponse>>(
                        configuration["CouponServer:GetCouponListByProduct"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                logger.Info($"GetCouponListByProduct_Error {msg}");
                throw new CustomException(msg);
            }
        }
    }
}
