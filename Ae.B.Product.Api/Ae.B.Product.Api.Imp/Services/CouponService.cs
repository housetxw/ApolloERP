using Ae.B.Product.Api.Client.Interfaces;
using Ae.B.Product.Api.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Core.Model.Coupon;
using Ae.B.Product.Api.Core.Request.Coupon;
using AutoMapper;
using Ae.B.Product.Api.Client.Request.Coupon;
using Ae.B.Product.Api.Client.Model.Coupon;
using ApolloErp.Login.Auth;
using Ae.B.Product.Api.Client.Request.Order;
using Ae.B.Product.Api.Client.Request.User;
using Ae.B.Product.Api.Common.Exceptions;
using Ae.B.Product.Api.Common.Extension;
using Ae.B.Product.Api.Core.Request.User;
using Ae.B.Product.Api.Core.Model.User;
using Ae.B.Product.Api.Client.Model.User;

namespace Ae.B.Product.Api.Imp.Services
{
    public class CouponService : ICouponService
    {
        private readonly ICouponClient _couponClient;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;
        private readonly IUserClient _userClient;
        private readonly IOrderClient _orderClient;

        public CouponService(ICouponClient couponClient, IMapper mapper, IIdentityService identityService,
            IUserClient userClient, IOrderClient orderClient)
        {
            _couponClient = couponClient;
            _mapper = mapper;
            _identityService = identityService;
            _userClient = userClient;
            _orderClient = orderClient;
        }

        public async Task<ApiPagedResultData<UserCouponPageResByUserIdVo>> GetUserCouponPageByUserId(
            UserCouponPageByUserIdRequest request)
        {
            ApiPagedResultData<UserCouponPageResByUserIdVo> data = new ApiPagedResultData<UserCouponPageResByUserIdVo>()
            {
                Items = new List<UserCouponPageResByUserIdVo>()
            };
            var clientRequest = _mapper.Map<UserCouponPageByUserIdClientRequest>(request);
            var result = await _couponClient.GetUserCouponPageByUserId(clientRequest);
            if (result != null)
            {
                data.TotalItems = result.TotalItems;
                if (result.Items != null && result.Items.Any())
                {
                    data.Items = _mapper.Map<List<UserCouponPageResByUserIdVo>>(result.Items);
                }
            }

            return data;
        }

        /// <summary>
        /// 优惠券配置查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<CouponRuleVo>> GetCouponRuleList(GetCouponRuleListRequest request)
        {
            ApiPagedResultData<CouponRuleVo> data = new ApiPagedResultData<CouponRuleVo>()
            {
                Items = new List<CouponRuleVo>()
            };
            var result = await _couponClient.GetCouponRuleList(new GetCouponRuleListClientRequest()
            {
                Id = request.Id,
                DisplayName = request.DisplayName,
                Type = request.Type,
                Category = request.Category,
                RangeType = request.RangeType,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ShopId=request.ShopId
            });
            if (result != null)
            {
                data.TotalItems = result.TotalItems;
                if (result.Items != null && result.Items.Any())
                {
                    data.Items = _mapper.Map<List<CouponRuleVo>>(result.Items);
                }
            }

            return data;
        }

        /// <summary>
        /// 获取优惠券类型
        /// </summary>
        /// <returns></returns>
        public async Task<List<CouponTypeVo>> GetCouponTypeEnum(CouponTypeEnumRequest request)
        {
            List<CouponTypeVo> result = new List<CouponTypeVo>();
            if (request.ShowAll)
            {
                result.Add(new CouponTypeVo()
                {
                    Id = 0,
                    DisplayName = "全部"
                });
            }

            Type t = typeof(Core.Enums.CouponType);
            Array arrays = Enum.GetValues(t);
            for (int i = 0; i < arrays.LongLength; i++)
            {
                Core.Enums.CouponType couponType = (Core.Enums.CouponType) arrays.GetValue(i);
                if ((int) couponType > 0)
                {
                    result.Add(new CouponTypeVo()
                    {
                        Id = (int) couponType,
                        DisplayName = EnumExtension.GetEnumDescription(couponType)
                    });
                }
            }

            return await Task.FromResult(result);
        }

        /// <summary>
        /// 获取券分类
        /// </summary>
        /// <returns></returns>
        public async Task<List<CouponTypeVo>> GetCouponCategory()
        {
            List<CouponTypeVo> result = new List<CouponTypeVo>();
            Type t = typeof(Core.Enums.CouponCategory);
            Array arrays = Enum.GetValues(t);
            for (int i = 0; i < arrays.LongLength; i++)
            {
                Core.Enums.CouponCategory couponType = (Core.Enums.CouponCategory) arrays.GetValue(i);
                result.Add(new CouponTypeVo()
                {
                    Id = (int) couponType,
                    DisplayName = EnumExtension.GetEnumDescription(couponType)
                });
            }

            return await Task.FromResult(result);
        }

        /// <summary>
        /// 获取券类型
        /// </summary>
        /// <returns></returns>
        public async Task<List<CouponTypeVo>> GetCouponType()
        {
            List<CouponTypeVo> result = new List<CouponTypeVo>();
            Type t = typeof(Core.Enums.CouponType);
            Array arrays = Enum.GetValues(t);
            for (int i = 0; i < arrays.LongLength; i++)
            {
                Core.Enums.CouponType couponType = (Core.Enums.CouponType) arrays.GetValue(i);
                result.Add(new CouponTypeVo()
                {
                    Id = (int) couponType,
                    DisplayName = EnumExtension.GetEnumDescription(couponType)
                });
            }

            return await Task.FromResult(result);
        }

        /// <summary>
        /// 获取优惠券规则
        /// </summary>
        /// <returns></returns>
        public async Task<List<CouponTypeVo>> GetCouponRangeType()
        {
            List<CouponTypeVo> result = new List<CouponTypeVo>();
            Type t = typeof(Core.Enums.CouponRangeType);
            Array arrays = Enum.GetValues(t);
            for (int i = 0; i < arrays.LongLength; i++)
            {
                Core.Enums.CouponRangeType couponType = (Core.Enums.CouponRangeType) arrays.GetValue(i);
                result.Add(new CouponTypeVo()
                {
                    Id = (int) couponType,
                    DisplayName = EnumExtension.GetEnumDescription(couponType)
                });
            }

            return await Task.FromResult(result);
        }

        /// <summary>
        /// 获取支付方式
        /// </summary>
        /// <returns></returns>
        public async Task<List<CouponTypeVo>> GetPayMethod()
        {
            List<CouponTypeVo> result = new List<CouponTypeVo>();
            Type t = typeof(Core.Enums.PayMethod);
            Array arrays = Enum.GetValues(t);
            for (int i = 0; i < arrays.LongLength; i++)
            {
                Core.Enums.PayMethod couponType = (Core.Enums.PayMethod) arrays.GetValue(i);
                result.Add(new CouponTypeVo()
                {
                    Id = (int) couponType,
                    DisplayName = EnumExtension.GetEnumDescription(couponType)
                });
            }

            return await Task.FromResult(result);
        }

        /// <summary>
        /// 有效期开始类型
        /// </summary>
        /// <returns></returns>
        public async Task<List<CouponTypeVo>> GetValidStartType()
        {
            List<CouponTypeVo> result = new List<CouponTypeVo>();
            Type t = typeof(Core.Enums.ValidStartType);
            Array arrays = Enum.GetValues(t);
            for (int i = 0; i < arrays.LongLength; i++)
            {
                Core.Enums.ValidStartType couponType = (Core.Enums.ValidStartType) arrays.GetValue(i);
                result.Add(new CouponTypeVo()
                {
                    Id = (int) couponType,
                    DisplayName = EnumExtension.GetEnumDescription(couponType)
                });
            }

            return await Task.FromResult(result);
        }

        /// <summary>
        /// 有效期结束类型
        /// </summary>
        /// <returns></returns>
        public async Task<List<CouponTypeVo>> GetValidEndType()
        {
            List<CouponTypeVo> result = new List<CouponTypeVo>();
            Type t = typeof(Core.Enums.ValidEndType);
            Array arrays = Enum.GetValues(t);
            for (int i = 0; i < arrays.LongLength; i++)
            {
                Core.Enums.ValidEndType couponType = (Core.Enums.ValidEndType) arrays.GetValue(i);
                result.Add(new CouponTypeVo()
                {
                    Id = (int) couponType,
                    DisplayName = EnumExtension.GetEnumDescription(couponType)
                });
            }

            return await Task.FromResult(result);
        }

        /// <summary>
        /// 添加优惠券配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<long> AddCouponRule(AddCouponRuleRequest request)
        {
            List<long> shopIdList = new List<long>();
            List<string> pidList = new List<string>();
            try
            {
                shopIdList = request.ShopIdList.Replace("，", ",")
                    .Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(_ => Convert.ToInt64(_)).ToList();
            }
            catch (Exception e)
            {
                throw new CustomException("门店配置输入有误");
            }

            try
            {
                pidList = request.PidList.Replace("，", ",").Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                    .ToList();
            }
            catch (Exception e)
            {
                throw new CustomException("产品Pid配置输入有误");
            }

            var result = await _couponClient.AddCouponRule(new AddCouponRuleClientRequest()
            {
                Name = request.Name,
                DisplayName = request.DisplayName,
                Category = request.Category,
                Type = request.Type,
                RangeType = request.RangeType,
                Value = string.IsNullOrEmpty(request.Value) ? 0 : Convert.ToDecimal(request.Value),
                Threshold = string.IsNullOrEmpty(request.Threshold) ? 0 : Convert.ToDecimal(request.Threshold),
                ImageUrl = request.ImageUrl,
                PayMethod = request.PayMethod,
                ShopType = request.ShopType,
                ShopIdList = shopIdList,
                RegionList = request.RegionList?.Select(_ => new CouponRuleRegionDto
                {
                    ProvinceId = _.ProvinceId,
                    CityId = _.CityId,
                    DistrictId = _.DistrictId,
                    Province = _.Province,
                    City = _.City,
                    District = _.District
                })?.ToList() ?? new List<CouponRuleRegionDto>(),
                CategoryList = request.CategoryList,
                BrandList = request.BrandList,
                PidList = pidList,
                UseRuleDesc = request.UseRuleDesc,
                ValidStartType = request.ValidStartType,
                ValidEndType = request.ValidEndType,
                ValidDays = string.IsNullOrEmpty(request.ValidDays) ? 0 : Convert.ToInt32(request.ValidDays),
                EarliestUseDate = ConvertDateTime(request.EarliestUseDate),
                LatestUseDate = ConvertDateTime(request.LatestUseDate),
                CreateBy = _identityService.GetUserName()
            });

            return result;
        }

        private DateTime? ConvertDateTime(string strTime)
        {
            if (!string.IsNullOrEmpty(strTime))
            {
                return Convert.ToDateTime(strTime);
            }

            return null;
        }

        /// <summary>
        /// 获取活动配置列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<CouponActivityVo>> GetCouponActivityList(
            GetCouponActivityListRequest request)
        {
            ApiPagedResultData<CouponActivityVo> data = new ApiPagedResultData<CouponActivityVo>()
            {
                Items = new List<CouponActivityVo>()
            };
            long couponActivityId = 0;
            long couponRuleId = 0;
            if (!string.IsNullOrEmpty(request.CouponActivityId))
            {
                try
                {
                    couponActivityId = Convert.ToInt64(request.CouponActivityId);
                }
                catch (Exception e)
                {
                    couponActivityId = 0;
                }
            }

            if (!string.IsNullOrEmpty(request.CouponRuleId))
            {
                Int64.TryParse(request.CouponRuleId, out couponRuleId);
            }

            var result = await _couponClient.GetCouponActivityList(new GetCouponActivityListClientRequest()
            {
                Name = request.Name,
                CouponActivityId = couponActivityId,
                Status = request.Status,
                CouponRuleId = couponRuleId,
                Code = request.Code,
                CodeId = request.CodeId,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            });
            if (result != null)
            {
                data.TotalItems = result.TotalItems;
                if (result.Items != null && result.Items.Any())
                {
                    data.Items = _mapper.Map<List<CouponActivityVo>>(result.Items);
                }
            }

            return data;
        }

        /// <summary>
        /// 活动渠道
        /// </summary>
        /// <returns></returns>
        public async Task<List<CouponTypeVo>> GetCouponActivityChannel()
        {
            List<CouponTypeVo> result = new List<CouponTypeVo>();
            Type t = typeof(Core.Enums.CouponActivityChannel);
            Array arrays = Enum.GetValues(t);
            for (int i = 0; i < arrays.LongLength; i++)
            {
                Core.Enums.CouponActivityChannel couponType = (Core.Enums.CouponActivityChannel) arrays.GetValue(i);
                result.Add(new CouponTypeVo()
                {
                    Id = (int) couponType,
                    DisplayName = EnumExtension.GetEnumDescription(couponType)
                });
            }

            return await Task.FromResult(result);
        }

        /// <summary>
        /// 需要积分类型
        /// </summary>
        /// <returns></returns>
        public async Task<List<CouponTypeVo>> GetCouponActivityIntegralType()
        {
            List<CouponTypeVo> result = new List<CouponTypeVo>();
            Type t = typeof(Core.Enums.CouponActivityIntegralType);
            Array arrays = Enum.GetValues(t);
            for (int i = 0; i < arrays.LongLength; i++)
            {
                Core.Enums.CouponActivityIntegralType couponType =
                    (Core.Enums.CouponActivityIntegralType) arrays.GetValue(i);
                result.Add(new CouponTypeVo()
                {
                    Id = (int) couponType,
                    DisplayName = EnumExtension.GetEnumDescription(couponType)
                });
            }

            return await Task.FromResult(result);
        }

        /// <summary>
        /// 添加活动配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<long> AddCouponActivity(AddCouponActivityRequest request)
        {
            var result = await _couponClient.AddCouponActivity(new AddCouponActivityClientRequest()
            {
                Name = request.Name,
                Code = request.Code,
                CouponRuleId = request.CouponRuleId,
                Channel = request.Channel,
                IsNewUserOnly = request.IsNewUserOnly,
                MaxNumPerUser = request.MaxNumPerUser,
                IsIntegralExchange = request.IsIntegralExchange,
                NeedIntegralType = request.NeedIntegralType,
                NeedIntegralNum = request.NeedIntegralNum,
                TotalNum = request.TotalNum,
                Num = request.Num,
                Url = request.Url,
                StartTime = Convert.ToDateTime(request.StartTime),
                EndTime = Convert.ToDateTime(request.EndTime),
                GrantPattern = request.GrantPattern,
                ParentId = request.ParentId,
                SubmitBy = _identityService.GetUserName()
            });

            return result;
        }

        /// <summary>
        /// 编辑活动配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditCouponActivity(EditCouponActivityRequest request)
        {
            var result = await _couponClient.EditCouponActivity(new EditCouponActivityClientRequest()
            {
                CouponActivityId = request.CouponActivityId,
                Name = request.Name,
                Code = request.Code,
                CouponId = request.CouponId,
                Channel = request.Channel,
                IsNewUserOnly = request.IsNewUserOnly,
                MaxNumPerUser = request.MaxNumPerUser,
                IsIntegralExchange = request.IsIntegralExchange,
                NeedIntegralType = request.NeedIntegralType,
                NeedIntegralNum = request.NeedIntegralNum,
                TotalNum = request.TotalNum,
                Num = request.Num,
                Url = request.Url,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                GrantPattern = request.GrantPattern,
                ParentId = request.ParentId,
                SubmitBy = _identityService.GetUserName()
            });

            return result;
        }

        /// <summary>
        /// 更新活动状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> UpdateCouponActivityStatus(UpdateCouponActivityStatusRequest request)
        {
            var result = await _couponClient.UpdateCouponActivityStatus(new UpdateCouponActivityStatusClientRequest()
            {
                ActivityId = request.ActivityId,
                Status = request.Status,
                SubmitBy = _identityService.GetUserName()
            });

            return result;
        }

        /// <summary>
        /// 活动详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CouponActivityDetailVo> GetCouponActivityDetail(CouponActivityDetailRequest request)
        {
            var result = await _couponClient.GetCouponActivityDetail(new CouponActivityDetailClientRequest()
            {
                ActivityId = request.ActivityId
            });

            if (result != null)
            {
                return _mapper.Map<CouponActivityDetailVo>(result);
            }

            return null;
        }

        /// <summary>
        /// 优惠券发放记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<UserCouponGrantRecordVo>> GetUserCouponGrantRecord(
            UserCouponGrantRecordRequest request)
        {
            ApiPagedResultData<UserCouponGrantRecordVo> data = new ApiPagedResultData<UserCouponGrantRecordVo>()
            {
                Items = new List<UserCouponGrantRecordVo>()
            };
            DateTime? startTime = null;
            DateTime? endTime = null;
            string userId = string.Empty;
            if (!string.IsNullOrEmpty(request.UserTel))
            {
                var userResult = await _userClient.GetUserInfoByUserTel(new UserInfoByUserTelClientRequest()
                {
                    UserTel = request.UserTel
                });

                if (userResult == null)
                {
                    return data;
                }

                userId = userResult.UserId;
            }

            if (!string.IsNullOrEmpty(request.StartTime))
            {
                try
                {
                    startTime = Convert.ToDateTime(request.StartTime);
                }
                catch (Exception e)
                {
                    startTime = null;
                }

            }

            if (!string.IsNullOrEmpty(request.EndTime))
            {
                try
                {
                    endTime = Convert.ToDateTime(request.EndTime);
                }
                catch (Exception e)
                {
                    endTime = null;
                }
            }

            var result = await _couponClient.GetUserCouponGrantRecord(new UserCouponGrantRecordClientRequest()
            {
                CouponRuleId = request.CouponRuleId,
                CouponActivityId = request.CouponActivityId,
                UserId = userId,
                StartTime = startTime,
                EndTime = endTime,
                Status = request.Status,
                GrantMethod = request.GrantMethod,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ShopId = request.ShopId
            });

            if (result != null)
            {
                data.TotalItems = result.TotalItems;
                if (result.Items != null && result.Items.Any())
                {
                    data.Items = _mapper.Map<List<UserCouponGrantRecordVo>>(result.Items);
                }

                List<long> userCouponIdList = data.Items.Where(_ => _.Status == UserCouponStatus.Used)
                    .Select(_ => _.UserCouponId).ToList(); //已使用的优惠券Id

                if (userCouponIdList.Any())
                {
                    var orderCoupon = await _orderClient.GetOrderCoupons(new GetOrderCouponsClientRequest()
                    {
                        CouponIds = userCouponIdList
                    });

                    foreach (var couponItem in data.Items)
                    {
                        if (couponItem.Status == UserCouponStatus.Used)
                        {
                            var orderNo =
                                orderCoupon.FirstOrDefault(_ => _.UserCouponId == couponItem.UserCouponId)?.OrderNo ??
                                string.Empty;
                            if (!string.IsNullOrEmpty(orderNo))
                            {
                                couponItem.OrderNo = orderNo;
                                couponItem.StatusName += $" - {orderNo}";
                            }
                        }
                    }
                }
            }

            return data;
        }

        /// <summary>
        /// 用户姓名/手机号搜索用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<UserInfoForCouponVo>> SearchUserInfo(SearchUserInfoRequest request)
        {
            ApiPagedResultData<UserInfoForCouponVo> data = new ApiPagedResultData<UserInfoForCouponVo>()
            {
                Items = new List<UserInfoForCouponVo>()
            };
            var result = await _userClient.SearchUserInfo(new SearchUserInfoClientRequest()
            {
                UserName = request.UserName,
                UserTel = request.UserTel,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            });
            if (result != null)
            {
                data.TotalItems = result.TotalItems;
                if (result.Items != null && result.Items.Any())
                {
                    data.Items = result.Items.Select(_ => new UserInfoForCouponVo
                    {
                        UserId = _.UserId,
                        UserName = _.UserName,
                        NickName = _.NickName,
                        UserTel = _.UserTel,
                        Gender = _.Gender == 1 ? "男" : (_.Gender == 2 ? "女" : string.Empty)
                    }).ToList();
                }
            }

            return data;
        }

        /// <summary>
        /// 用户塞券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> GrantUserCoupon(GrantUserCouponRequest request)
        {
            var result = await _couponClient.GrantUserCoupon(new GrantUserCouponClientRequest()
            {
                ActivityId = request.ActivityId,
                UserIdList = request.UserIdList,
                SubmitBy = _identityService.GetUserName()
            });

            return result;
        }

        /// <summary>
        /// 作废优惠券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> InvalidatedUserCoupon(InvalidatedUserCouponRequest request)
        {
            var result = await _couponClient.InvalidatedUserCoupon(new InvalidatedUserCouponClientRequest()
            {
                UserCouponId = request.UserCouponId,
                SubmitBy = _identityService.GetUserName()
            });

            return result;
        }

        /// <summary>
        /// 延期优惠券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DelayUserCoupon(DelayUserCouponRequest request)
        {
            var delayDay = request.DayNum > 0 ? request.DayNum : 60;
            var result = await _couponClient.DelayUserCoupon(new DelayUserCouponClientRequest()
            {
                UserCouponId = request.UserCouponId,
                SubmitBy = _identityService.GetUserName(),
                EndDay = DateTime.Now.AddDays(delayDay)
            });

            return result;
        }

        /// <summary>
        /// 更新发放总数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> UpdateActivityTotalNum(UpdateActivityTotalNumRequest request)
        {
            var result = await _couponClient.UpdateActivityTotalNum(new UpdateActivityTotalNumClientRequest()
            {
                CouponActivityId = request.CouponActivityId,
                TotalNum = request.TotalNum,
                SubmitBy = _identityService.GetUserName()
            });

            return result;
        }

        /// <summary>
        /// 开瓶有奖记录查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<DecapAwardDetailVo>> SearchDecapAward(SearchDecapAwardRequest request)
        {
            ApiPagedResultData<DecapAwardDetailVo> result = new ApiPagedResultData<DecapAwardDetailVo>()
            {
                Items = new List<DecapAwardDetailVo>()
            };
            var decapAward = await _couponClient.SearchDecapAward(new SearchDecapAwardClientRequest()
            {
                Code = request.Code,
                Status = request.Status,
                PayStatus = request.PayStatus,
                PayChannel = request.PayChannel,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            });

            if (decapAward != null)
            {
                result.TotalItems = decapAward.TotalItems;
                if (decapAward.Items != null && decapAward.Items.Any())
                {
                    List<UserBaseInfoDto> userList = new List<UserBaseInfoDto>();
                    var userId = decapAward.Items.Where(_ => !string.IsNullOrWhiteSpace(_.DrawUserId))
                        .Select(_ => _.DrawUserId).ToList();
                    if (userId.Any())
                    {
                        userList = await _userClient.GetUsersByUserIds(userId);
                    }

                    foreach (var deItem in decapAward.Items)
                    {
                        string drawOpenId = deItem.DrawOpenId;
                        string userName = string.Empty;
                        string userTel = string.Empty;
                        if (deItem.ClientChannel == "AliPay")
                        {
                            userName = drawOpenId.Split(new[] {'_'})[1];
                            userTel = drawOpenId.Split(new[] {'_'})[0];
                            drawOpenId = string.Empty;
                        }
                        else if (deItem.ClientChannel == "WeChat")
                        {
                            var defaultUser = userList.FirstOrDefault(_ => _.UserId == deItem.DrawUserId);
                            drawOpenId = drawOpenId.Split(new[] {'_'})[0];
                            if (defaultUser != null)
                            {
                                userName = !string.IsNullOrWhiteSpace(defaultUser.UserName)
                                    ? defaultUser.UserName
                                    : defaultUser.NickName;
                                userTel = defaultUser.UserTel;
                            }
                        }

                        DecapAwardDetailVo detailItem = new DecapAwardDetailVo()
                        {
                            Id = deItem.Id,
                            Code = TransCode(deItem.Code, deItem.PayStatus),
                            Amount = deItem.Amount,
                            Status = deItem.Status,
                            PayStatus = deItem.PayStatus,
                            DrawTime = deItem.DrawTime.HasValue
                                ? deItem.DrawTime.Value.ToString("yyyy-MM-dd HH:mm:ss")
                                : string.Empty,
                            DrawUserId = deItem.DrawUserId,
                            DrawOpenId = drawOpenId,
                            PayTime = deItem.PayTime.HasValue
                                ? deItem.PayTime.Value.ToString("yyyy-MM-dd HH:mm:ss")
                                : string.Empty,
                            ClientChannel = deItem.ClientChannel,
                            UserName = userName,
                            UserTel = userTel
                        };
                        result.Items.Add(detailItem);
                    }
                }
            }

            return result;
        }

        private string TransCode(string code, int payStatus)
        {
            if (payStatus == 1)
            {
                return code;
            }
            else
            {
                return $"{code.Substring(0, 4)}***********{code.Substring(code.Length - 4, 4)}";
            }
        }

        /// <summary>
        /// 消费赠券活动规则列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<GiftCouponRuleVo>> GetGiftCouponRulePageList(
            GiftCouponRulePageListRequest request)
        {
            ApiPagedResultData<GiftCouponRuleVo> result = new ApiPagedResultData<GiftCouponRuleVo>()
            {
                Items = new List<GiftCouponRuleVo>()
            };

            var data = await _couponClient.GetGiftCouponRulePageList(new GiftCouponRulePageListClientRequest()
            {
                Name = request.Name,
                Actived = request.Actived,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            });

            if (data != null)
            {
                result.TotalItems = data.TotalItems;
                if (data.Items != null && data.Items.Any())
                {
                    result.Items = _mapper.Map<List<GiftCouponRuleVo>>(data.Items);
                }
            }

            return result;
        }

        /// <summary>
        /// 消费赠券活动规则详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GiftCouponRuleDetailVo> GetGiftCouponRuleDetail(GiftCouponRuleDetailRequest request)
        {
            var result = await _couponClient.GetGiftCouponRuleDetail(new GiftCouponRuleDetailClientRequest()
            {
                GiftId = request.GiftId
            });

            if (result != null)
            {
                return _mapper.Map<GiftCouponRuleDetailVo>(result);
            }

            return null;
        }

        /// <summary>
        /// 新增赠券规则
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddGiftCouponRule(AddGiftCouponRuleRequest request)
        {
            var result = await _couponClient.AddGiftCouponRule(new AddGiftCouponRuleClientRequest()
            {
                Name = request.Name,
                Channel = request.Channel,
                MaxNumPerUser = request.MaxNumPerUser,
                Threshold = request.Threshold,
                StartTime = Convert.ToDateTime(request.StartTime),
                EndTime = Convert.ToDateTime(request.EndTime),
                Actived = request.Actived,
                SubmitBy = _identityService.GetUserName(),
                CouponActivityId = request.CouponActivityId,
                PidList = request.PidList
            });

            return result;
        }

        /// <summary>
        /// 编辑赠券规则
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditGiftCouponRule(EditGiftCouponRuleRequest request)
        {
            var result = await _couponClient.EditGiftCouponRule(new EditGiftCouponRuleClientRequest()
            {
                Id = request.Id,
                Name = request.Name,
                Channel = request.Channel,
                MaxNumPerUser = request.MaxNumPerUser,
                Threshold = request.Threshold,
                StartTime = TransDateTime(request.StartTime),
                EndTime = TransDateTime(request.EndTime),
                Actived = request.Actived,
                SubmitBy = _identityService.GetUserName(),
                CouponActivityId = request.CouponActivityId,
                PidList = request.PidList
            });

            return result;
        }

        private DateTime? TransDateTime(string dateTime)
        {
            if (DateTime.TryParse(dateTime, out DateTime date))
            {
                return date;
            }

            return null;
        }
    }
}
