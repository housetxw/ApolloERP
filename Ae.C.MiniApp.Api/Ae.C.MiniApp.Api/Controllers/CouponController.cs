using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Request.Coupon;
using Ae.C.MiniApp.Api.Common.Constant;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Model.Coupon;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Request.Coupon;
using Ae.C.MiniApp.Api.Core.Response;
using Ae.C.MiniApp.Api.Core.Response.Coupon;
using Ae.C.MiniApp.Api.Filters;

namespace Ae.C.MiniApp.Api.Controllers
{
    /// <summary>
    /// 优惠券
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(CouponController))]
    public class CouponController : ControllerBase
    {
        #region 变量和构造器

        private readonly ApolloErpLogger<CouponController> logger;
        private readonly IMapper mapper;
        private readonly string className;

        private readonly ICouponService copSvc;

        public CouponController(ApolloErpLogger<CouponController> logger, IMapper mapper,
            ICouponService copSvc)
        {
            this.logger = logger;
            this.mapper = mapper;
            className = this.GetType().Name;

            this.copSvc = copSvc;
        }

        /// <summary>
        /// 健康检查
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        [HttpGet]
        public string Health(string msg)
        {
            return $"{msg}:{DateTime.Now.ToString(CultureInfo.InvariantCulture)}";
        }

        #endregion 变量和构造器

        #region UserCoupon

        /// <summary>
        /// 根据userId获取用户优惠券列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<UserCouponPageResByUserIdVO>> GetUserCouponPageByUserId([FromBody] ApiRequest<UserCouponPageReqByUserIdVO> req)
        {
            var res = new ApiPagedResult<UserCouponPageResByUserIdVO>();
            try
            {
                res = await copSvc.GetUserCouponPageByUserId(req.Data);
            }
            catch (Exception e)
            {
                var msg = $"{CommonConstant.ExceptionOccured}\n" +
                          $"{CommonConstant.ParameterReqDetail}:\n{JsonConvert.SerializeObject(req)}";
                logger.Error(msg, e);

                res.Code = ResultCode.Exception;
                res.Message = CommonConstant.ExceptionOccured;
            }
            return res;
        }

        /// <summary>
        /// 根据userId和优惠码(couponCode)兑换优惠券
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> ExchangeCouponByPromotionCode([FromBody] ApiRequest<ExchangeCouponReqByCodeVO> req)
        {
            var res = new ApiResult<bool>();
            try
            {
                res = await copSvc.ExchangeCouponByPromotionCode(req);
            }
            catch (Exception e)
            {
                var msg = $"{CommonConstant.ExceptionOccured}\n" +
                          $"{CommonConstant.ParameterReqDetail}:\n{JsonConvert.SerializeObject(req)}";
                logger.Error(msg, e);

                res.Code = ResultCode.Exception;
                res.Message = CommonConstant.ExceptionOccured;
            }
            return res;
        }

        /// <summary>
        /// 通过活动领取优惠券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> ExchangeCouponByActId(
            [FromBody] ApiRequest<ExchangeCouponByActIdRequest> request)
        {
            var result = await copSvc.ExchangeCouponByActId(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 根据userId和couponActivityId，积分兑换优惠券
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> IntegralExchangeCoupon([FromBody] ApiRequest<IntegralExchangeCouponReqVO> req)
        {
            var res = new ApiResult<bool>();
            try
            {
                res = await copSvc.IntegralExchangeCoupon(req);
            }
            catch (Exception e)
            {
                var msg = $"{CommonConstant.ExceptionOccured}\n" +
                          $"{CommonConstant.ParameterReqDetail}:\n{JsonConvert.SerializeObject(req)}";
                logger.Error(msg, e);

                res.Code = ResultCode.Exception;
                res.Message = CommonConstant.ExceptionOccured;
            }
            return res;
        }


        #endregion

        #region CouponActivity

        /// <summary>
        /// 获取积分可兑换优惠券列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<CouponActivityPageResVO>> GetIntegralCouponActivityPage([FromQuery] CouponActivityPageReqByChannelVO req)
        {
            var res = new ApiPagedResult<CouponActivityPageResVO>();
            try
            {
                //if (req.ActivityChannel != CouponActivityChannel.NotSet && req.ActivityChannel != CouponActivityChannel.MiniApp)
                //{
                //    return Result.Failed<CouponActivityPageResVO>(ResultCode.ArgumentError, "请输入正确的渠道信息", null, 0);
                //}

                res = await copSvc.GetIntegralCouponActivityPage(new CouponActivityPageReqByChannelVOForDTO
                {
                    ActivityChannel = CouponActivityChannel.MiniApp
                });
            }
            catch (Exception e)
            {
                var msg = $"{CommonConstant.ExceptionOccured}\n" +
                          $"{CommonConstant.ParameterReqDetail}:\n{JsonConvert.SerializeObject(req)}";
                logger.Error(msg, e);

                res.Code = ResultCode.Exception;
                res.Message = CommonConstant.ExceptionOccured;
            }
            return res;
        }

        /// <summary>
        /// 兑换码查询优惠券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<CouponActivityByCodeResponse>> GetCouponActivityByCode(
            [FromQuery] CouponActivityByCodeRequest request)
        {
            var result = await copSvc.GetCouponActivityByCode(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 根据活动获取优惠券详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResult<CouponDetailByActIdResponse>> GetCouponDetailByActId(
            [FromQuery] CouponDetailByActIdRequest request)
        {
            var result = await copSvc.GetCouponDetailByActId(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 根据产品获取可领取的优惠券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResult<GetCouponListByProductResponse>> GetCouponListByProduct(
            [FromQuery] CouponListByProductRequest request)
        {
            var result = await copSvc.GetCouponListByProduct(request);

            return Result.Success(result);
        }

        #endregion

        #region CouponRule

        #endregion


        #region OBSOLETED

        /// <summary>
        /// TODO：查询订单确认页可用优惠券列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Obsolete("已废弃")]
        [HttpPost]
        public async Task<ApiPagedResult<GetOrderApplicableCouponListResponse>> GetOrderApplicableCouponList([FromBody]ApiRequest<GetOrderApplicableCouponListRequest> request)
        {
            return null;
        }

        /// <summary>
        /// TODO：获取用户优惠券列表; DONE
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Obsolete("已废弃")]
        [HttpGet]
        public async Task<ApiPagedResult<GetUserCouponListResponse>> GetUserCouponList([FromQuery]GetUserCouponListRequest request)
        {
            return null;
        }

        /// <summary>
        /// TODO：优惠码兑换优惠券 DONE
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Obsolete("已废弃")]
        [HttpPost]
        public async Task<ApiResult> ExchangeCouponByCode([FromQuery]ExchangeCouponByCodeRequest request)
        {
            return null;
        }

        /// <summary>
        /// TODO：获取积分可兑换优惠券列表 DONE
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Obsolete("已废弃")]
        [HttpGet]
        public async Task<ApiPagedResult<GetIntegralExchangeCouponListResponse>> GetIntegralExchangeCouponList([FromQuery]GetIntegralExchangeCouponListRequest request)
        {
            return null;
        }

        /// <summary>
        /// TODO：积分兑换优惠券 DONE
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Obsolete("已废弃")]
        [HttpPost]
        public async Task<ApiResult> IntegralExchangeCouponBefore([FromBody]ApiRequest<IntegralExchangeCouponRequest> request)
        {
            return null;
        }

        #endregion OBSOLETED


        #region 开瓶有奖

        /// <summary>
        /// 扫码查询开瓶有礼
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<DecapAwardByCodeResponse>> GetDecapAwardByCode(
            [FromQuery] DecapAwardByCodeRequest request)
        {
            var result = await copSvc.GetDecapAwardByCode(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 开瓶有奖数据查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiResult<DecapAwardByCodeResponse>> GetDecapAwardDetailByCode(
            [FromQuery] DecapAwardDetailByCodeRequest request)
        {
            var result = await copSvc.GetDecapAwardDetailByCode(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 领取红包
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ApiResult<bool>> DrawDecapAward([FromBody] ApiRequest<DrawDecapAwardRequest> request)
        {
            string clent = "支付宝";
            if (request.Data.PayChannel == PayChannel.WeChat)
            {
                clent = "微信";
            }

            var result = await copSvc.DrawDecapAward(request.Data);

            return Result.Success(result, result ? $"领取成功，请往{clent}查看账单" : "领取失败");
        }

        /// <summary>
        /// 领取红包V2
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ApiResult<DrawDecapAwardV2Response>> DrawDecapAwardV2(
            [FromBody] ApiRequest<DrawDecapAwardRequest> request)
        {
            var result = await copSvc.DrawDecapAwardV2(request.Data);

            return Result.Success(result);
        }

        #endregion
    }
}
