using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Core.Interfaces;
using Ae.B.Product.Api.Core.Model.Coupon;
using Ae.B.Product.Api.Core.Model.User;
using Ae.B.Product.Api.Core.Request.Coupon;
using Ae.B.Product.Api.Core.Request.User;

namespace Ae.B.Product.Api.Controllers
{
    /// <summary>
    /// 优惠券相关接口
    /// </summary>
    [Route("[controller]/[action]")]
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _couponService;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="couponService"></param>
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        /// <summary>
        /// 优惠券列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<UserCouponPageResByUserIdVo>> GetUserCouponPageByUserId(
            [FromBody] ApiRequest<UserCouponPageByUserIdRequest> request)
        {
            var result = await _couponService.GetUserCouponPageByUserId(request.Data);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 优惠券配置查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<CouponRuleVo>> GetCouponRuleList([FromQuery] GetCouponRuleListRequest request)
        {
            var result = await _couponService.GetCouponRuleList(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 获取优惠券类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<CouponTypeVo>>> GetCouponTypeEnum([FromQuery] CouponTypeEnumRequest request)
        {
            var result = await _couponService.GetCouponTypeEnum(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 获取券分类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<CouponTypeVo>>> GetCouponCategory()
        {
            var result = await _couponService.GetCouponCategory();

            return Result.Success(result);
        }

        /// <summary>
        /// 获取券类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<CouponTypeVo>>> GetCouponType()
        {
            var result = await _couponService.GetCouponType();

            return Result.Success(result);
        }

        /// <summary>
        /// 获取优惠券规则
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<CouponTypeVo>>> GetCouponRangeType()
        {
            var result = await _couponService.GetCouponRangeType();

            return Result.Success(result);
        }

        /// <summary>
        /// 获取支付方式
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<CouponTypeVo>>> GetPayMethod()
        {
            var result = await _couponService.GetPayMethod();

            return Result.Success(result);
        }

        /// <summary>
        /// 有效期开始类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<CouponTypeVo>>> GetValidStartType()
        {
            var result = await _couponService.GetValidStartType();

            return Result.Success(result);
        }

        /// <summary>
        /// 有效期结束类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<CouponTypeVo>>> GetValidEndType()
        {
            var result = await _couponService.GetValidEndType();

            return Result.Success(result);
        }

        /// <summary>
        /// 获取门店类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<CouponTypeVo>>> GetShopTypeEnum()
        {
            var result = new List<CouponTypeVo>
            {
                new CouponTypeVo()
                {
                    Id = 0, DisplayName = "未设置"
                },
                new CouponTypeVo()
                {
                    Id = 1, DisplayName = "合作店"
                },
                new CouponTypeVo()
                {
                    Id = 2, DisplayName = "直营店"
                },
                new CouponTypeVo()
                {
                    Id = 4, DisplayName = "上门养护"
                },
                new CouponTypeVo()
                {
                    Id = 8, DisplayName = "认证店"
                }
            };

            return await Task.FromResult(Result.Success(result));
        }

        /// <summary>
        /// 添加优惠券配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<long>> AddCouponRule([FromBody] ApiRequest<AddCouponRuleRequest> request)
        {
            var result = await _couponService.AddCouponRule(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 获取活动配置列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<CouponActivityVo>> GetCouponActivityList(
            [FromQuery] GetCouponActivityListRequest request)
        {
            var result = await _couponService.GetCouponActivityList(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 添加活动配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<long>> AddCouponActivity([FromBody] ApiRequest<AddCouponActivityRequest> request)
        {
            var result = await _couponService.AddCouponActivity(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 编辑活动配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditCouponActivity([FromBody] ApiRequest<EditCouponActivityRequest> request)
        {
            var result = await _couponService.EditCouponActivity(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 活动渠道
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<CouponTypeVo>>> GetCouponActivityChannel()
        {
            var result = await _couponService.GetCouponActivityChannel();

            return Result.Success(result);
        }

        /// <summary>
        /// 需要积分类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<CouponTypeVo>>> GetCouponActivityIntegralType()
        {
            var result = await _couponService.GetCouponActivityIntegralType();

            return Result.Success(result);
        }

        /// <summary>
        /// 更新活动状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> UpdateCouponActivityStatus(
            [FromBody] ApiRequest<UpdateCouponActivityStatusRequest> request)
        {
            var result = await _couponService.UpdateCouponActivityStatus(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 活动详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<CouponActivityDetailVo>> GetCouponActivityDetail(
            [FromQuery] CouponActivityDetailRequest request)
        {
            var result = await _couponService.GetCouponActivityDetail(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 优惠券发放记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<UserCouponGrantRecordVo>> GetUserCouponGrantRecord(
            [FromQuery] UserCouponGrantRecordRequest request)
        {
            var result = await _couponService.GetUserCouponGrantRecord(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 用户姓名/手机号搜索用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<UserInfoForCouponVo>> SearchUserInfo([FromQuery] SearchUserInfoRequest request)
        {
            var result = await _couponService.SearchUserInfo(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 用户塞券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> GrantUserCoupon([FromBody] ApiRequest<GrantUserCouponRequest> request)
        {
            var result = await _couponService.GrantUserCoupon(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 作废优惠券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> InvalidatedUserCoupon(
            [FromBody] ApiRequest<InvalidatedUserCouponRequest> request)
        {
            var result = await _couponService.InvalidatedUserCoupon(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 延期优惠券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> DelayUserCoupon(
            [FromBody] ApiRequest<DelayUserCouponRequest> request)
        {
            var result = await _couponService.DelayUserCoupon(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 更新发放总数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> UpdateActivityTotalNum(
            [FromBody] ApiRequest<UpdateActivityTotalNumRequest> request)
        {
            var result = await _couponService.UpdateActivityTotalNum(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 开瓶有奖记录查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<DecapAwardDetailVo>> SearchDecapAward(
            [FromQuery] SearchDecapAwardRequest request)
        {
            var result = await _couponService.SearchDecapAward(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 消费赠券活动规则列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<GiftCouponRuleVo>> GetGiftCouponRulePageList(
            [FromQuery] GiftCouponRulePageListRequest request)
        {
            var result = await _couponService.GetGiftCouponRulePageList(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 消费赠券活动规则详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GiftCouponRuleDetailVo>> GetGiftCouponRuleDetail(
            [FromQuery] GiftCouponRuleDetailRequest request)
        {
            var result = await _couponService.GetGiftCouponRuleDetail(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 新增赠券规则
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AddGiftCouponRule([FromBody] ApiRequest<AddGiftCouponRuleRequest> request)
        {
            var result = await _couponService.AddGiftCouponRule(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 编辑赠券规则
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditGiftCouponRule([FromBody] ApiRequest<EditGiftCouponRuleRequest> request)
        {
            var result = await _couponService.EditGiftCouponRule(request.Data);

            return Result.Success(result);
        }
    }
}