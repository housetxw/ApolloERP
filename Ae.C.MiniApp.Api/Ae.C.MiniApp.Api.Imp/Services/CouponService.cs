using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Client.Request.Coupon;
using Ae.C.MiniApp.Api.Client.Request.Pay;
using Ae.C.MiniApp.Api.Client.Response.Coupon;
using Ae.C.MiniApp.Api.Common.Constant;
using Ae.C.MiniApp.Api.Common.Exceptions;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Model.Coupon;
using Ae.C.MiniApp.Api.Core.Request.Coupon;
using Ae.C.MiniApp.Api.Core.Response.Coupon;
using Rg.Pay.Service.Core.Enums.Pay;

namespace Ae.C.MiniApp.Api.Imp.Services
{
    public class CouponService : ICouponService
    {
        #region 变量和构造器

        private readonly ApolloErpLogger<CouponService> logger;
        private readonly IMapper mapper;
        private readonly string className;

        private readonly IIdentityService idenSvc;

        private readonly ICouponClient couponClient;
        private readonly IIdentityService identityService;
        private readonly IPayClient _payClient;

        public CouponService(ApolloErpLogger<CouponService> logger, IMapper mapper,
            ICouponClient couponClient,
            IIdentityService idenSvc, IIdentityService identityService, IPayClient payClient)
        {
            this.logger = logger;
            this.mapper = mapper;
            className = this.GetType().Name;

            this.idenSvc = idenSvc;

            this.couponClient = couponClient;
            this.identityService = identityService;

            _payClient = payClient;
        }


        #endregion 变量和构造器

        #region UserCoupon

        public async Task<ApiPagedResult<UserCouponPageResByUserIdVO>> GetUserCouponPageByUserId(
            UserCouponPageReqByUserIdVO req)
        {
            var reqDto = mapper.Map<UserCouponPageReqByUserIdDTO>(req);
            var resVo = await couponClient.GetUserCouponPageByUserId(reqDto);
            var res = new ApiPagedResult<UserCouponPageResByUserIdVO>
            {
                Code = resVo.Code,
                Message = resVo.Message,
                Data = new ApiPagedResultData<UserCouponPageResByUserIdVO>
                {
                    Items = mapper.Map<List<UserCouponPageResByUserIdVO>>(
                        resVo.Data?.Items ?? new List<UserCouponPageResByUserIdDTO>()),
                    TotalItems = resVo.Data?.TotalItems ?? 0
                }
            };
            return res;
        }

        public async Task<ApiResult<bool>> ExchangeCouponByPromotionCode(ApiRequest<ExchangeCouponReqByCodeVO> req)
        {
            var reqDto = mapper.Map<ApiRequest<ExchangeCouponReqByCodeDTO>>(req);

            reqDto.Data.ActivityChannel = CouponActivityChannel.MiniApp;
            reqDto.Data.UserIp = idenSvc?.GetIp() ?? CommonConstant.UnenabledAuthectication;

            var res = await couponClient.ExchangeCouponByPromotionCode(reqDto);
            return res;
        }

        public async Task<ApiResult<bool>> IntegralExchangeCoupon(ApiRequest<IntegralExchangeCouponReqVO> req)
        {
            var reqDto = mapper.Map<ApiRequest<IntegralExchangeCouponReqDTO>>(req);

            reqDto.Data.ActivityChannel = CouponActivityChannel.MiniApp;
            reqDto.Data.UserIp = idenSvc?.GetIp() ?? CommonConstant.UnenabledAuthectication;

            var res = await couponClient.IntegralExchangeCoupon(reqDto);
            return res;
        }

        /// <summary>
        /// 通过活动领取优惠券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> ExchangeCouponByActId(ExchangeCouponByActIdRequest request)
        {
            long.TryParse(request.CouponActivityId, out long actId);
            var result = await couponClient.ExchangeCouponByActId(new ExchangeCouponByActIdClientRequest()
            {
                UserId = identityService.GetUserId(),
                CouponActivityId = actId,
                ActivityChannel = CouponActivityChannel.MiniApp,
                UserIp = idenSvc?.GetIp() ?? CommonConstant.UnenabledAuthectication
            });

            return result;
        }


        #endregion

        #region CouponActivity

        /// <summary>
        ///  根据优惠券活动渠道，获取积分可兑换优惠券列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<CouponActivityPageResVO>> GetIntegralCouponActivityPage(
            CouponActivityPageReqByChannelVOForDTO req)
        {
            var reqDto = mapper.Map<CouponActivityPageReqByChannelDTO>(req);
            var resVo = await couponClient.GetIntegralCouponActivityPage(reqDto);
            var res = new ApiPagedResult<CouponActivityPageResVO>
            {
                Code = resVo.Code,
                Message = resVo.Message,
                Data = new ApiPagedResultData<CouponActivityPageResVO>
                {
                    Items = mapper.Map<List<CouponActivityPageResVO>>(
                        resVo.Data?.Items ?? new List<CouponActivityPageResDTO>()),
                    TotalItems = resVo.Data?.TotalItems ?? 0
                }
            };
            return res;
        }

        /// <summary>
        /// 兑换码查询优惠券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CouponActivityByCodeResponse> GetCouponActivityByCode(CouponActivityByCodeRequest request)
        {
            var result = await couponClient.GetCouponActivityByCode(new CouponActivityByCodeClientRequest()
            {
                RedeemCode = request.RedeemCode,
                UserId = identityService.GetUserId()
            });
            if (result != null)
            {
                return new CouponActivityByCodeResponse()
                {
                    CanDraw = result.CanDraw,
                    Code = result.Code,
                    Name = result.Name,
                    Message = result.Message
                };
            }
            else
            {
                throw new CustomException("系统异常");
            }
        }

        /// <summary>
        /// 根据活动获取优惠券详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CouponDetailByActIdResponse> GetCouponDetailByActId(CouponDetailByActIdRequest request)
        {
            long.TryParse(request.CouponActivityId, out long actId);
            var result = await couponClient.GetCouponDetailByActId(new CouponDetailByActIdClientRequest()
            {
                CouponActivityId = actId,
                ActivityChannel = CouponActivityChannel.MiniApp,
                UserId = identityService.GetUserId()
            });
            if (result == null)
            {
                throw new CustomException("活动id无效");
            }

            return mapper.Map<CouponDetailByActIdResponse>(result);
        }

        /// <summary>
        /// 根据产品获取可领取的优惠券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetCouponListByProductResponse> GetCouponListByProduct(CouponListByProductRequest request)
        {
            var result = await couponClient.GetCouponListByProduct(new CouponListByProductClientRequest()
            {
                Pid = request.Pid,
                ActivityChannel = CouponActivityChannel.MiniApp,
                UserId = identityService.GetUserId()
            });
            return mapper.Map<GetCouponListByProductResponse>(result);
        }

        #endregion

        #region CouponRule

        #endregion

        #region MyRegion

        /// <summary>
        /// 扫码查询开瓶有礼
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<DecapAwardByCodeResponse> GetDecapAwardByCode(DecapAwardByCodeRequest request)
        {
            DecapAwardByCodeResponse result = new DecapAwardByCodeResponse();
            var decapAward = await couponClient.GetDecapAwardByCode(new DecapAwardByCodeClientRequest()
            {
                Code = request.Code,
                ReadOnly = false
            });

            if (decapAward == null)
            {
                throw new CustomException("兑换码无效");
            }

            result.Amount = decapAward.Amount;

            if (decapAward.PayStatus == 0)
            {
                // await couponClient.DrawDecapAward(new DrawDecapAwardClientRequest()
                // {
                //     AwardId = decapAward.Id,
                //     Code = decapAward.Code,
                //     UserId = identityService.GetUserId(),
                //     OpenId = request.OpenId,
                //     ClientChannel = PayChannel.AliPay.ToString()
                // });
                result.CanDraw = true;
            }
            else
            {
                result.CanDraw = false;
                result.Message = "该红包已被领取";
            }

            return result;
        }

        /// <summary>
        /// 开瓶有奖数据查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<DecapAwardByCodeResponse> GetDecapAwardDetailByCode(DecapAwardDetailByCodeRequest request)
        {
            DecapAwardByCodeResponse result = new DecapAwardByCodeResponse();
            var decapAward = await couponClient.GetDecapAwardByCode(new DecapAwardByCodeClientRequest()
            {
                Code = request.Code,
                ReadOnly = false
            });

            if (decapAward == null)
            {
                throw new CustomException("兑换码无效");
            }

            result.Amount = decapAward.Amount;
            result.AwardId = decapAward.Id;

            if (decapAward.PayStatus == 0)
            {
                result.CanDraw = true;
            }
            else
            {
                result.CanDraw = false;
                result.Message = "该红包已被领取";
            }

            return result;
        }

        /// <summary>
        /// 领取红包
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DrawDecapAward(DrawDecapAwardRequest request)
        {
            var decapAward = await couponClient.GetDecapAwardByCode(new DecapAwardByCodeClientRequest()
            {
                Code = request.Code,
                ReadOnly = false
            });

            if (decapAward == null)
            {
                throw new CustomException("兑换码无效");
            }

            var response = await couponClient.DrawDecapAward(new DrawDecapAwardClientRequest()
            {
                AwardId = request.AwardId,
                Code = request.Code,
                UserId = identityService.GetUserId(),
                OpenId = $"{request.Identity}_{request.UserName}",
                ClientChannel = request.PayChannel.ToString()
            });

            if (response)
            {
                PayChannelEnum channel = PayChannelEnum.None;
                switch (request.PayChannel)
                {
                    case PayChannel.AliPay:
                        channel = PayChannelEnum.Alipay;
                        break;
                    case PayChannel.WeChat:
                        channel = PayChannelEnum.WxPay;
                        break;
                }

                await _payClient.EnterprisePay(new EnterprisePayClientRequest()
                {
                    InnerNoType = PayInnerNoTypeEnum.RedeemCode,
                    OrderNo = decapAward.Code,
                    Channel = channel,
                    Amount = decapAward.Amount,
                    CreateBy = $"{request.Identity}_{request.UserName}",
                    Identity = request.Identity,
                    UserName = request.UserName
                });
            }

            return response;
        }

        /// <summary>
        /// 领取红包V2
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<DrawDecapAwardV2Response> DrawDecapAwardV2(DrawDecapAwardRequest request)
        {
            DrawDecapAwardV2Response result = new DrawDecapAwardV2Response()
            {
                Success = false,
                Message = "领取失败，请重试"
            };
            var decapAward = await couponClient.GetDecapAwardByCode(new DecapAwardByCodeClientRequest()
            {
                Code = request.Code,
                ReadOnly = false
            });

            if (decapAward == null)
            {
                throw new CustomException("兑换码无效");
            }

            var response = await couponClient.DrawDecapAward(new DrawDecapAwardClientRequest()
            {
                AwardId = request.AwardId,
                Code = request.Code,
                UserId = identityService.GetUserId(),
                OpenId = $"{request.Identity}_{request.UserName}",
                ClientChannel = request.PayChannel.ToString()
            });

            if (response)
            {
                PayChannelEnum channel = PayChannelEnum.None;
                switch (request.PayChannel)
                {
                    case PayChannel.AliPay:
                        channel = PayChannelEnum.Alipay;
                        break;
                    case PayChannel.WeChat:
                        channel = PayChannelEnum.WxPay;
                        break;
                }

                var payResult = await _payClient.EnterprisePay(new EnterprisePayClientRequest()
                {
                    InnerNoType = PayInnerNoTypeEnum.RedeemCode,
                    OrderNo = decapAward.Code,
                    Channel = channel,
                    Amount = decapAward.Amount,
                    CreateBy = $"{request.Identity}_{request.UserName}",
                    Identity = request.Identity,
                    UserName = request.UserName,
                    IsHb = true
                });

                string clent = "支付宝";
                if (request.PayChannel == PayChannel.WeChat)
                {
                    clent = "微信";
                }

                if (payResult != null && payResult.Success)
                {
                    result.Success = true;
                    result.Message = $"领取成功，请往{clent}查看账单";
                    if (payResult.WxMiniAppHbCallData != null)
                    {
                        result.WxMiniAppHbCallData = new WxMiniAppHbCallData()
                        {
                            TimeStamp = payResult.WxMiniAppHbCallData.TimeStamp,
                            NonceStr = payResult.WxMiniAppHbCallData.NonceStr,
                            Package = payResult.WxMiniAppHbCallData.Package,
                            SignType = payResult.WxMiniAppHbCallData.SignType,
                            PaySign = payResult.WxMiniAppHbCallData.PaySign
                        };
                    }
                }
            }

            return result;
        }

        #endregion
    }
}
