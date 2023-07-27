using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Client.Inteface;
using Ae.Shop.Service.Client.Model.Receive;
using Ae.Shop.Service.Client.Model.User;
using Ae.Shop.Service.Client.Request;
using Ae.Shop.Service.Common.Exceptions;
using Ae.Shop.Service.Common.Helper;
using Ae.Shop.Service.Core.Enums;
using Ae.Shop.Service.Core.Interfaces;
using Ae.Shop.Service.Core.Model.ShopCustomer;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Request.ShopCustomer;
using Ae.Shop.Service.Core.Response;
using Ae.Shop.Service.Dal.Model;
using Ae.Shop.Service.Dal.Repositorys.ShopCustomer;

namespace Ae.Shop.Service.Imp.Services
{
    public class ShopCustomerService : IShopCustomerService
    {
        private readonly IShopUserRelationRepository _shopUserRelationRepository;
        private readonly IUserClient _userClient;
        private readonly IReceiveClient _receiveClient;
        private readonly IVehicleClient _vehicleClient;

        public ShopCustomerService(IShopUserRelationRepository shopUserRelationRepository, IUserClient userClient,
            IReceiveClient receiveClient, IVehicleClient vehicleClient)
        {
            _shopUserRelationRepository = shopUserRelationRepository;
            _userClient = userClient;
            _receiveClient = receiveClient;
            _vehicleClient = vehicleClient;
        }

        /// <summary>
        /// 会员标签查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<MemberTagDto> GetMemberTag(MemberTagRequest request)
        {
            var result = await _shopUserRelationRepository.GetShopUserRelation(request.ShopId, request.UserId);
            if (result != null)
            {
                MemberTagEnum memberTagEnum = ConvertMemberTag(result.MemberTag);

                return new MemberTagDto()
                {
                    MemberTag = (int)memberTagEnum,
                    MemberTagDisplay = EnumHelper.GetEnumDescription(memberTagEnum)
                };
            }

            return null;
        }

        private MemberTagEnum ConvertMemberTag(int memberTag)
        {
            MemberTagEnum memberTagEnum = MemberTagEnum.CommonMember;
            var memberValue = memberTag;
            switch (memberValue)
            {
                case 200:
                    memberTagEnum = MemberTagEnum.HighMember;
                    break;
                case 300:
                    memberTagEnum = MemberTagEnum.VipMember;
                    break;
            }
            return memberTagEnum;
        }

        /// <summary>
        /// 获取所有会员标签
        /// </summary>
        /// <returns></returns>
        public async Task<List<MemberTagDto>> GetMemberTagEnum()
        {
            List<MemberTagDto> result = new List<MemberTagDto>()
            {
                new MemberTagDto()
                {
                    MemberTag = (int) MemberTagEnum.CommonMember,
                    MemberTagDisplay = EnumHelper.GetEnumDescription(MemberTagEnum.CommonMember)
                },
                new MemberTagDto()
                {
                    MemberTag = (int) MemberTagEnum.HighMember,
                    MemberTagDisplay = EnumHelper.GetEnumDescription(MemberTagEnum.HighMember)
                },
                new MemberTagDto()
                {
                    MemberTag = (int) MemberTagEnum.VipMember,
                    MemberTagDisplay = EnumHelper.GetEnumDescription(MemberTagEnum.VipMember)
                }
            };

            return await Task.FromResult(result);
        }

        /// <summary>
        /// 用户关联门店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddShopUserRelation(AddShopUserRelationRequest request)
        {
            var result = await _shopUserRelationRepository.GetShopUserRelation(request.ShopId, request.UserId, false);
            if (result != null)
            {
                return true;
            }

            Guid.TryParse(request.UserId, out var userId);
            ShopUserRelationDO shopUserRelationDo = new ShopUserRelationDO()
            {
                ShopId = request.ShopId,
                UserId = userId,
                CreateBy = request.SubmitBy,
                MemberTag = (int)MemberTagEnum.CommonMember,
                CreateTime = DateTime.Now
            };

            GenReferrerInfo(request, shopUserRelationDo);

            var record = await _shopUserRelationRepository.InsertAsync(shopUserRelationDo);
            return record > 0;
        }

        /// <summary>
        /// 编辑会员标签
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditMemberTag(EditMemberTagRequest request)
        {
            var result = await _shopUserRelationRepository.GetShopUserRelation(request.ShopId, request.UserId, false);
            if (result == null)
            {
                throw new CustomException("用户不存在");
            }

            if (Enum.GetName(typeof(MemberTagEnum), request.MemberTag) == null)
            {
                throw new CustomException("会员标签值有误");
            }

            ShopUserRelationDO shopUserRelationDo = new ShopUserRelationDO()
            {
                Id = result.Id,
                MemberTag = request.MemberTag,
                UpdateBy = request.SubmitBy,
                UpdateTime = DateTime.Now
            };
            var record = await _shopUserRelationRepository.UpdateAsync(shopUserRelationDo,
                new List<string>() { "MemberTag", "UpdateBy", "UpdateTime" });

            return record > 0;
        }

        /// <summary>
        /// 更新用户最近一次到店时间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUserLastReceiveTime(UpdateUserLastReceiveTimeRequest request)
        {
            var result = await _shopUserRelationRepository.GetShopUserRelation(request.ShopId, request.UserId, false);
            if (result == null)
            {
                throw new CustomException("用户不存在");
            }

            ShopUserRelationDO shopUserRelationDo = new ShopUserRelationDO()
            {
                Id = result.Id,
                LastArriveId = request.ArriveId,
                LastArriveTime = request.ArriveTime,
                UpdateBy = request.SubmitBy,
                UpdateTime = DateTime.Now
            };
            var record = await _shopUserRelationRepository.UpdateAsync(shopUserRelationDo,
                new List<string>() { "LastArriveId", "LastArriveTime", "UpdateBy", "UpdateTime" });

            return record > 0;
        }

        /// <summary>
        /// 客户列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ShopCustomerVo>> GetCustomerList(CustomerListRequest request)
        {
            List<ShopCustomerVo> items = new List<ShopCustomerVo>();
            int pageIndex = request.PageIndex;
            int pageSize = request.PageSize;
            int totalCount = 0;

            var shopUser =
                await _shopUserRelationRepository.GetShopUserRelationByCondition(request.StartInTime,
                    request.MemberLevel, request.ShopId, request.EmployeeId) ?? new List<ShopUserRelationDO>();
            var userList = new List<UserBaseInfoDto>();
            CommonUserInfoRequest userRequest = new CommonUserInfoRequest();
            if (string.IsNullOrEmpty(request.SearchContent))
            {
                totalCount = shopUser.Count;
                shopUser = shopUser.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                if (shopUser.Any())
                {
                    userRequest.UserIdList = shopUser.Select(x => x.UserId.ToString()).ToList();
                    userList = await _userClient.GetCommonUserInfo(userRequest) ?? new List<UserBaseInfoDto>();
                }
            }
            else
            {
                userRequest.SearchContent = request.SearchContent;
                userList = await _userClient.GetCommonUserInfo(userRequest) ?? new List<UserBaseInfoDto>();
                var userIdList = userList.Select(_ => _.UserId).ToList();
                shopUser = shopUser.Where(_ => userIdList.Contains(_.UserId.ToString())).ToList();
                totalCount = shopUser.Count;
                shopUser = shopUser.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }

            if (shopUser.Any())
            {
                var recIdList = shopUser.Where(_ => _.LastArriveId > 0).Select(_ => _.LastArriveId).ToList();
                var receiveResult = new List<ShopReceiveDto>();
                if (recIdList.Any())
                {
                    receiveResult = await _receiveClient.GetReceiveByIds(new ReceiveByIdsClientRequest()
                    {
                        ArriveIdList = recIdList
                    }) ?? new List<ShopReceiveDto>();
                }

                var userIds = shopUser.Select(_ => _.UserId.ToString()).ToList();
                var vehicleList = (await _vehicleClient.GetUserDefaultVehicleBatch(new UserDefaultVehicleBatchRequest()
                {
                    UserIdList = userIds
                })) ?? new List<UserVehicleDto>();

                shopUser.ForEach(_ =>
                {
                    var userId = _.UserId.ToString();
                    var defaultUser = userList.FirstOrDefault(t => t.UserId == userId);
                    var defaultReceive = receiveResult.FirstOrDefault(x => x.RecId == _.LastArriveId);
                    var defaultCar = vehicleList.FirstOrDefault(t => t.UserId == userId);
                    if (defaultUser != null)
                    {
                        ShopCustomerVo cusItem = new ShopCustomerVo()
                        {
                            UserId = userId,
                            UserName = defaultUser.UserName,
                            UserTel = defaultUser.UserTel
                        };
                        if (defaultReceive != null)
                        {
                            cusItem.CarPlate = defaultReceive.CarPlate;
                            cusItem.LastInTime = defaultReceive.ArriveTime.ToString("yyyy-MM-dd");
                        }

                        if (defaultCar != null)
                        {
                            cusItem.CommonVehicle = new List<ReceiveVehicleVo>()
                            {
                                new ReceiveVehicleVo()
                                {
                                    CarPlate = defaultCar.CarNumber,
                                    CarType = defaultCar.Vehicle  //CommonHelper.GetCarVehicle(defaultCar.Vehicle, defaultCar.PaiLiang, defaultCar.Nian, defaultCar.SalesName)
                                }
                            };
                        }

                        MemberTagEnum memberTagEnum = ConvertMemberTag(_.MemberTag);
                        cusItem.MemberTag = (int)memberTagEnum;
                        cusItem.MemberTagDisplay = EnumHelper.GetEnumDescription(memberTagEnum);
                        items.Add(cusItem);
                    }
                });

            }

            return new ApiPagedResultData<ShopCustomerVo>()
            {
                Items = items,
                TotalItems = totalCount
            };
        }

        /// <summary>
        /// 门店客户列表（Shop站点）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ShopCustomerListVo>> GetShopCustomerList(ShopCustomerListRequest request)
        {
            List<ShopCustomerListVo> items = new List<ShopCustomerListVo>();
            int pageIndex = request.PageIndex;
            int pageSize = request.PageSize;
            int totalCount = 0;

            var shopUser =
                await _shopUserRelationRepository.GetShopUserRelationByCondition(request.StartInTime, 0, request.ShopId, null) ?? new List<ShopUserRelationDO>();
            var userList = new List<UserBaseInfoDto>();
            CommonUserInfoRequest userRequest = new CommonUserInfoRequest();
            if (string.IsNullOrEmpty(request.UserName) && string.IsNullOrEmpty(request.UserTel))
            {
                totalCount = shopUser.Count;
                shopUser = shopUser.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                if (shopUser.Any())
                {
                    userRequest.UserIdList = shopUser.Select(x => x.UserId.ToString()).ToList();
                    userList = await _userClient.GetCommonUserInfo(userRequest) ?? new List<UserBaseInfoDto>();
                }
            }
            else
            {
                userRequest.UserName = request.UserName;
                userRequest.UserTel = request.UserTel;
                userList = await _userClient.GetCommonUserInfo(userRequest) ?? new List<UserBaseInfoDto>();
                var userIdList = userList.Select(_ => _.UserId).ToList();
                shopUser = shopUser.Where(_ => userIdList.Contains(_.UserId.ToString())).ToList();
                totalCount = shopUser.Count;
                shopUser = shopUser.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }

            if (shopUser.Any())
            {
                var userIds = shopUser.Select(_ => _.UserId.ToString()).ToList();
                var vehicleList = (await _vehicleClient.GetUserDefaultVehicleBatch(new UserDefaultVehicleBatchRequest()
                {
                    UserIdList = userIds
                })) ?? new List<UserVehicleDto>();

                shopUser.ForEach(_ =>
                {
                    var userId = _.UserId.ToString();
                    var defaultUser = userList.FirstOrDefault(t => t.UserId == userId);
                    var defaultCar = vehicleList.FirstOrDefault(t => t.UserId == userId);
                    if (defaultUser != null)
                    {
                        ShopCustomerListVo cusItem = new ShopCustomerListVo()
                        {
                            UserId = userId,
                            UserName = defaultUser.UserName,
                            NickName = defaultUser.NickName,
                            UserTel = defaultUser.UserTel,
                            LastInTime = _.LastArriveTime?.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty
                        };

                        string genderDisplay = string.Empty;
                        int gender = defaultUser.Gender;
                        if (gender == 1)
                        {
                            genderDisplay = "男";
                        }
                        else if (gender == 2)
                        {
                            genderDisplay = "女";
                        }
                        cusItem.GenderDisplay = genderDisplay;

                        if (defaultCar != null)
                        {
                            cusItem.DefaultVehicle = new ReceiveVehicleVo()
                            {
                                CarPlate = defaultCar.CarNumber,
                                CarType = CommonHelper.GetCarVehicle(defaultCar.Vehicle, defaultCar.PaiLiang, defaultCar.Nian, defaultCar.SalesName)
                            };
                        }

                        items.Add(cusItem);
                    }
                });
            }

            return new ApiPagedResultData<ShopCustomerListVo>()
            {
                Items = items,
                TotalItems = totalCount
            };
        }

        /// <summary>
        /// 门店客户详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ShopCustomerDetailVo> GetShopCustomerDetail(ShopCustomerDetailRequest request)
        {
            var result = await _shopUserRelationRepository.GetShopUserRelationList(request.ShopId, new List<string> { request.UserId });

            if (result != null && result.Any())
            {
                return new ShopCustomerDetailVo { LastArriveTime = result.FirstOrDefault().LastArriveTime?.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty };
            }

            return null;
        }

        public async Task<ShopReferrerCustomerResDTO> GetCurMonthShopNewCustomerStatisticsInfo(ShopReferrerCustomerReqDTO req)
            => await _shopUserRelationRepository.GetCurMonthShopNewCustomerStatisticsInfo(req);

        public async Task<ApiPagedResultData<DrainageStatisticsResDTO>> GetDrainageStatisticsPage(DrainageStatisticsPageReqDTO req)
        {
            var res = await _shopUserRelationRepository.GetDrainageStatisticsPage(req);
            return new ApiPagedResultData<DrainageStatisticsResDTO>
            {
                TotalItems = res.TotalItems,
                Items = res.Items
            };
        }

        // ---------------------------------- 私有方法 --------------------------------------
        private void GenReferrerInfo(AddShopUserRelationRequest req, ShopUserRelationDO shopUserRelationDo)
        {
            shopUserRelationDo.ReferrerShopId = req.ReferrerShopId;
            shopUserRelationDo.ReferrerUserId = req.ReferrerUserId;
            shopUserRelationDo.Channel = Convert.ToInt32(req.Channel);
            shopUserRelationDo.ReferrerType = Convert.ToInt32(req.ReferrerType);
        }


        #region 平台汽配相关

        /// <summary>
        /// 平台汽配下单调用
        /// 绑定客户与店的关系
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddShopUserRelationForQp(AddShopUserRelationForQpRequest request)
        {
            var result = await _shopUserRelationRepository.GetShopUserRelation(request.ShopId, request.UserId, false);
            if (result == null)
            {
                Guid.TryParse(request.UserId, out var userId);
                ShopUserRelationDO shopUserRelationDo = new ShopUserRelationDO()
                {
                    ShopId = request.ShopId,
                    UserId = userId,
                    LastOrderNo = request.OrderNo,
                    LastOrderTime = request.OrderTime,
                    CreateBy = request.SubmitBy,
                    MemberTag = (int) MemberTagEnum.CommonMember,
                    CreateTime = DateTime.Now
                };

                var record = await _shopUserRelationRepository.InsertAsync(shopUserRelationDo);

                return record > 0;
            }
            else
            {
                ShopUserRelationDO shopUserRelationDo = new ShopUserRelationDO()
                {
                    Id = result.Id,
                    LastOrderNo = request.OrderNo,
                    LastOrderTime = request.OrderTime,
                    UpdateBy = request.SubmitBy,
                    UpdateTime = DateTime.Now
                };
                var record = await _shopUserRelationRepository.UpdateAsync(shopUserRelationDo,
                    new List<string>() {"LastOrderNo", "LastOrderTime", "UpdateBy", "UpdateTime"});

                return record > 0;
            }
        }

        /// <summary>
        /// 平台汽配 - 我的顾客
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ShopCustomerQpVo>> GetCustomerListForQp(CustomerListForQpRequest request)
        {
            ApiPagedResultData<ShopCustomerQpVo> result = new ApiPagedResultData<ShopCustomerQpVo>()
            {
                Items = new List<ShopCustomerQpVo>()
            };
            var shopUser =
                await _shopUserRelationRepository.GetShopCustomerListForQp(request.ShopId, request.PageIndex,
                    request.PageSize);
            if (shopUser != null)
            {
                result.TotalItems = shopUser.TotalItems;
                if (shopUser.Items != null && shopUser.Items.Any())
                {
                    var userIds = shopUser.Items.Select(_ => _.UserId.ToString()).ToList();
                    var userResult = await _userClient.GetUsersByUserIds(new UsersByUserIdsClientRequest()
                    {
                        UserIds = userIds
                    });

                    foreach (var itemUser in shopUser.Items)
                    {
                        var userId = itemUser.UserId.ToString();
                        var defaultUser = userResult.FirstOrDefault(_ => _.UserId == userId);
                        if (defaultUser != null)
                        {
                            result.Items.Add(new ShopCustomerQpVo()
                            {
                                UserId = userId,
                                UserName = defaultUser.UserName,
                                NickName = defaultUser.NickName,
                                UserTel = defaultUser.UserTel,
                                LastOrderTime = itemUser.LastOrderTime.HasValue
                                    ? itemUser.LastOrderTime.Value.ToString("yyyy-MM-dd HH:mm:ss")
                                    : string.Empty,
                                HeadUrl = defaultUser.HeadUrl
                            });
                        }
                    }
                }
            }

            return result;
        }

        #endregion
    }
}
