using AutoMapper;
using Ae.User.Service.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;
using ApolloErp.Log;
using Ae.User.Service.Common.Constant;
using Ae.User.Service.Common.Exceptions;
using Ae.User.Service.Common.Helper;
using Ae.User.Service.Dal.Repository;
using Ae.User.Service.Core.Model;
using Ae.User.Service.Core.Request;
using Ae.User.Service.Dal.Model;
using Ae.User.Service.Dal.Model.Request;
using Ae.User.Service.Core.Response;
using ApolloErp.Web.WebApi;
using Microsoft.Extensions.Configuration;
using Ae.User.Service.Core.Enums;

namespace Ae.User.Service.Imp.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUserPointRepository _userPointRepository;
        private readonly IMemberGradeEnumRepository _memberGradeEnumRepository;
        private readonly IUserAddressRepository _userAddressRepository;
        private readonly IUserProductFocusRepository _userProductFocusRepository;
        private readonly IUserPointRecordRepository _userPointRecordRepository;
        private readonly ApolloErpLogger<UserService> _logger;
        private readonly IUserExpandShopRepository _userExpandShopRepository;
        private readonly IConfiguration _configuration;
        private readonly IUserAuthenticationRepository _userAuthenticationRepository;
        private readonly IUserBankCardRepository _userBankCardRepository;


        public UserService(IMapper mapper, IUserRepository userRepository, IUserPointRepository userPointRepository,
            IMemberGradeEnumRepository memberGradeEnumRepository, IUserAddressRepository userAddressRepository,
            IUserProductFocusRepository userProductFocusRepository,
            IUserPointRecordRepository userPointRecordRepository, ApolloErpLogger<UserService> logger,
            IUserExpandShopRepository userExpandShopRepository, IConfiguration configuration,
            IUserAuthenticationRepository userAuthenticationRepository, IUserBankCardRepository userBankCardRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _userPointRepository = userPointRepository;
            _memberGradeEnumRepository = memberGradeEnumRepository;
            _userAddressRepository = userAddressRepository;
            _userProductFocusRepository = userProductFocusRepository;
            _userPointRecordRepository = userPointRecordRepository;
            _logger = logger;
            _userExpandShopRepository = userExpandShopRepository;
            _configuration = configuration;
            _userAuthenticationRepository = userAuthenticationRepository;
            _userBankCardRepository = userBankCardRepository;
        }

        /// <summary>
        /// 客户列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Tuple<List<UserBaseInfoVo>, int>> GetUserList(UserListRequest request)
        {
            GetUserListRequest repoRequest = _mapper.Map<GetUserListRequest>(request);

            var resultData = await _userRepository.GetUserListAsync(repoRequest);
            var data = resultData.Item1?.Select(_ => new UserBaseInfoVo
            {
                UserId = _.Id.ToString(),
                UserName = _.UserName,
                NickName = _.NickName,
                ContactName = _.ContactName,
                Gender = _.Gender,
                BirthDay = _.BirthDay > new DateTime(1900, 1, 1) ? _.BirthDay.ToString("yyyy-MM-dd") : "",
                UserTel = _.MobileNumber,
                Email = _.Email,
                CreateTime = _.CreateTime,
                UserTelDes = FormatHelper.FormatTel(_.MobileNumber)
            })?.ToList();

            return new Tuple<List<UserBaseInfoVo>, int>(data, resultData.Item2);
        }

        /// <summary>
        /// 客户详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserInfoResponse> GetUserInfo(GetUserInfoRequest request)
        {
            var userResultTask = _userRepository.GetUserInfo(request.UserId);
            var pointResultTask = _userPointRepository.GetUserPointAsync(request.UserId);
            var memberGradeResultTask = _memberGradeEnumRepository.GetMemberGradeEnumAsync();
            var userAddressTask = _userAddressRepository.GetUserAddressAsync(request.UserId);
            await Task.WhenAll(userResultTask, pointResultTask, memberGradeResultTask, userAddressTask);
            var userResult = userResultTask.Result;
            var pointResult = pointResultTask.Result;
            var memberGradeResult = memberGradeResultTask.Result?.ToList() ?? new List<MemberGradeEnumDO>();
            var userAddress = userAddressTask.Result?.ToList() ?? new List<UserAddressDO>();
            if (userResult != null)
            {
                UserInfoResponse result = new UserInfoResponse()
                {
                    UserId = userResult.Id.ToString(),
                    UserName = userResult.UserName,
                    NickName = userResult.NickName,
                    ContactName = userResult.ContactName,
                    HeadUrl = userResult.HeadUrl,
                    Gender = userResult.Gender,
                    BirthDay = userResult.BirthDay > new DateTime(1900, 1, 1)
                        ? userResult.BirthDay.ToString("yyyy-MM-dd")
                        : "",
                    UserTel = userResult.MobileNumber,
                    UserTelDes = FormatHelper.FormatTel(userResult.MobileNumber),
                    PersonalSign = userResult.PersonalSign,
                    Email = userResult.Email,
                    ContactAddress = userResult.ContactAddress,
                    IdNumber = userResult.IdNumber,
                    DriverLicenseExpiry = userResult.DriverLicenseExpiry > new DateTime(1900, 1, 1)
                        ? userResult.DriverLicenseExpiry.ToString("yyyy-MM-dd") : "",
                    Channel = (ChannelType)userResult.Channel,
                    ReferrerUserId = userResult.ReferrerUserId,
                    CreateTime = userResult.CreateTime > new DateTime(1900, 1, 1)
                        ? userResult.CreateTime.ToString("yyyy-MM-dd")
                        : "",
                };
                int currentGradeValue = 0;
                if (pointResult != null)
                {
                    result.Point = pointResult.CurrentPoint;
                    currentGradeValue = pointResult.CurrentGrowthValue;
                }
                var currentGrade = memberGradeResult.FirstOrDefault(_ =>
                    _.StartValue <= currentGradeValue && _.EndValue >= currentGradeValue);
                if (currentGrade != null)
                {
                    result.MemberLevel = currentGrade.Description;
                    result.MemberGrade = currentGrade.MemberGrade;
                    result.MemberUrl = currentGrade.MemberUrl;
                }

                var userDefaultAddress = userAddress.FirstOrDefault(_ => _.DefaultAddress);
                if (userDefaultAddress != null)
                {
                    result.DefaultAddress = new UserAddressVO()
                    {
                        AddressId = userDefaultAddress.Id,
                        UserName = userDefaultAddress.UserName,
                        MobileNumber = userDefaultAddress.MobileNumber,
                        MobileNumberDes = FormatHelper.FormatTel(userDefaultAddress.MobileNumber),
                        AddressTag = userDefaultAddress.AddressTag,
                        Province = userDefaultAddress.Province,
                        City = userDefaultAddress.City,
                        District = userDefaultAddress.District,
                        AddressLine = userDefaultAddress.AddressLine,
                        ProvinceId = userDefaultAddress.ProvinceId,
                        CityId = userDefaultAddress.CityId,
                        DistrictId = userDefaultAddress.DistrictId,
                        DefaultAddress = userDefaultAddress.DefaultAddress
                    };
                }

                return result;
            }

            return null;
        }

        /// <summary>
        /// userId批量查询用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<UserBaseInfoVo>> GetUsersByUserIds(UsersByUserIdsRequest request)
        {
            List<UserBaseInfoVo> result = new List<UserBaseInfoVo>();
            var userIds = request.UserIds;
            if (userIds != null && userIds.Any())
            {
                var resultData = await _userRepository.GetUsersByUserIds(userIds);
                if (resultData != null && resultData.Any())
                {
                    result = resultData.Select(_ => new UserBaseInfoVo
                    {
                        UserId = _.Id.ToString(),
                        UserName = _.UserName,
                        NickName = _.NickName,
                        ContactName = _.ContactName,
                        Gender = _.Gender,
                        BirthDay = _.BirthDay > new DateTime(1900, 1, 1) ? _.BirthDay.ToString("yyyy-MM-dd") : "",
                        UserTel = _.MobileNumber,
                        Email = _.Email,
                        UserTelDes = FormatHelper.FormatTel(_.MobileNumber),
                        HeadUrl = _.HeadUrl
                    }).ToList();
                }
            }

            return result;
        }

        /// <summary>
        /// 查询用户已关注商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserAttentionProductResponse> GetUserAttentionProduct(UserAttentionProductRequest request)
        {
            List<string> pidList = new List<string>();
            var result = (await _userProductFocusRepository.GetUserAttentionProductAsync(request.UserId))?.ToList();
            if (result != null && result.Any())
            {
                pidList = result.Select(_ => _.Pid).Distinct().ToList();
            }

            return new UserAttentionProductResponse()
            {
                PidList = pidList
            };
        }

        /// <summary>
        /// 添加关注商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddPersonalProduct(AddPersonalProductRequest request)
        {
            foreach (var itemPid in request.PidList)
            {
                UserProductFocusDO userProductRef = new UserProductFocusDO()
                {
                    UserId = Guid.Parse(request.UserId),
                    Pid = itemPid,
                    Source = request.Source,
                    CreateBy = request.SubmitBy,
                    CreateTime = DateTime.Now,
                    UpdateBy = request.SubmitBy,
                    UpdateTime = DateTime.Now
                };
                await _userProductFocusRepository.InsertAsync(userProductRef);
            }

            return true;
        }

        /// <summary>
        /// 取消关注商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> CancelPersonalProduct(CancelPersonalProductRequest request)
        {
            return await _userProductFocusRepository.DeleteUserProductAsync(request.UserId, request.PidList,
                request.SubmitBy);
        }

        /// <summary>
        /// 个人积分
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserPointResponse> GetUserPoint(UserPointRequest request)
        {
            UserPointResponse result = new UserPointResponse();
            var pointTask = _userPointRepository.GetUserPointAsync(request.UserId);
            var pointRecordTask = _userPointRecordRepository.GetUserPointRecordAsync(request.UserId);

            // await Task.WhenAll(pointTask, pointRecordTask);
            await pointTask;
            var point = pointTask.Result;
            var pointRecord = new List<UserPointRecordDO>();

            if (!request.OnlyCurrentPoint)
            {
                await pointRecordTask;
                pointRecord = pointRecordTask.Result?.ToList();
            }
           
            if (point != null)
            {
                result.CurrentPoint = point.CurrentPoint;
                result.PointList = pointRecord.Where(_ => _.BusinessType == 0).Select(_ => new UserPointRecordVo
                {
                    Id = _.Id,
                    OperationType = _.OperationType,
                    Description = _.Description,
                    CreateTime = _.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    PointValue = _.PointValue,
                    ReferrerNo = _.ReferrerNo,
                    ReferrerNoDes = FormatHelper.FormatTel(_.ReferrerNo),
                    Remark = _.Remark
                })?.OrderByDescending(_ => _.Id).ToList();
            }

            return result;
        }

        /// <summary>
        /// 内部员工积分查询（无限层级）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserPointResponse> GetTechUserAllPoint(UserPointRequest request)
        {
            UserPointResponse result = new UserPointResponse();
            var pointTask = _userPointRepository.GetUserPointAsync(request.UserId);
            var pointRecordTask = _userPointRecordRepository.GetUserPointRecordAsync(request.UserId);

            // await Task.WhenAll(pointTask, pointRecordTask);
            await pointTask;
            var point = pointTask.Result;
            var pointRecord = new List<UserPointRecordDO>();

            if (!request.OnlyCurrentPoint)
            {
                await pointRecordTask;
                pointRecord = pointRecordTask.Result?.ToList();
            }

            if (point != null)
            {
                result.CurrentPoint = point.CurrentPoint;
                result.PointList = pointRecord.Where(_ => _.BusinessType == 0).Select(_ => new UserPointRecordVo
                {
                    Id = _.Id,
                    OperationType = _.OperationType,
                    Description = _.Description,
                    CreateTime = _.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    PointValue = _.PointValue,
                    ReferrerNo = _.ReferrerNo,
                    Remark = _.Remark
                })?.OrderByDescending(_ => _.Id).ToList();
            }

            return result;
        }

        /// <summary>
        /// 成长等级
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<MemberLevelResponse> GetMemberLevel(MemberLevelRequest request)
        {
            MemberLevelResponse result = new MemberLevelResponse();
            var pointTask = _userPointRepository.GetUserPointAsync(request.UserId);
            var pointRecordTask = _userPointRecordRepository.GetUserPointRecordAsync(request.UserId);
            var memberGradeResultTask = _memberGradeEnumRepository.GetMemberGradeEnumAsync();
            await Task.WhenAll(pointTask, pointRecordTask, memberGradeResultTask);
            var point = pointTask.Result;
            var pointRecord = pointRecordTask.Result?.ToList() ?? new List<UserPointRecordDO>();
            var memberGradeResult = memberGradeResultTask.Result?.ToList() ?? new List<MemberGradeEnumDO>();
            if (point != null)
            {
                result.CurrentGrowthValue = point.CurrentGrowthValue;
            }
            var currentGrade = memberGradeResult.FirstOrDefault(_ =>
                _.StartValue <= result.CurrentGrowthValue && _.EndValue >= result.CurrentGrowthValue);
            if (currentGrade != null)
            {
                result.MemberGrade = currentGrade.MemberGrade;
                result.DisplayName = currentGrade.Description;
            }

            var nextGrade = memberGradeResult.Where(_ => _.StartValue > result.CurrentGrowthValue)
                .OrderBy(_ => _.StartValue).FirstOrDefault();
            if (nextGrade != null)
            {
                result.NextMemberGrade = nextGrade.MemberGrade;
            }

            result.GrowthDetail = pointRecord.Where(_ => _.BusinessType == 1).Select(_ => new UserPointRecordVo
            {
                OperationType = _.OperationType,
                Description = _.Description,
                CreateTime = _.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                PointValue = _.PointValue,
                Remark = _.Remark
            }).ToList();

            result.MemberGrades = memberGradeResult.Select(_ => new MemberGradeVo
            {
                MemberGrade = _.MemberGrade,
                DisplayName = _.Description,
                StartValue = _.StartValue,
                EndValue = _.EndValue,
                Remark = _.Remark
            }).ToList();

            return result;
        }

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="request"></param>
        public async Task<bool> EditUserInfo(EditUserInfoRequest request)
        {
            UserDO userDo = new UserDO()
            {
                Id = Guid.Parse(request.UserId),
                UserName = request.UserName,
                NickName = request.NickName,
                HeadUrl = request.HeadUrl,
                Gender = request.Gender,
                BirthDay = string.IsNullOrEmpty(request.BirthDay)
                    ? new DateTime(1900, 1, 1)
                    : Convert.ToDateTime(request.BirthDay),
                PersonalSign = request.PersonalSign,
                MobileNumber = request.UserTel,
                UpdateBy = request.SubmitBy,
                DriverLicenseExpiry = string.IsNullOrEmpty(request.DriverLicenseExpiry)
                    ? new DateTime(1900, 1, 1)
                    : Convert.ToDateTime(request.DriverLicenseExpiry),
                IdNumber = request.IdNumber,
                ContactAddress = request.Address,
            };
            return await _userRepository.EditUserInfo(userDo);
        }

        /// <summary>
        /// 手机号查用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserBaseInfoVo> GetUserInfoByUserTel(UserInfoByUserTelRequest request)
        {
            var result = await _userRepository.GetUserInfoByUserTel(request.UserTel);
            if (result != null)
            {
                return new UserBaseInfoVo()
                {
                    UserId = result.Id.ToString(),
                    UserName = result.UserName,
                    NickName = result.NickName,
                    Gender = result.Gender,
                    BirthDay = result.BirthDay > new DateTime(1900, 1, 1) ? result.BirthDay.ToString("yyyy-MM-dd") : "",
                    DriverLicenseExpiry = result.DriverLicenseExpiry > new DateTime(1900, 1, 1)
                        ? result.DriverLicenseExpiry.ToString("yyyy-MM-dd")
                        : "",
                    IdNumber = result.IdNumber,
                    Address = result.ContactAddress,
                    UserTel = result.MobileNumber,
                    Email = result.Email,
                    UserTelDes = FormatHelper.FormatTel(result.MobileNumber),
                    CreateTime = result.CreateTime
                };
            }

            return null;
        }

        /// <summary>
        /// 操作用户积分
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> OperateUserPoint(OperateUserPointRequest request)
        {
            var userPoint = await _userPointRepository.GetUserPointAsync(request.UserId, false);
            if (request.PointValue < 0)
            {
                if (userPoint == null)
                {
                    throw new CustomException("当前用户积分不够");
                }

                if (userPoint.CurrentPoint + request.PointValue < 0)
                {
                    throw new CustomException("当前用户积分不够");
                }
            }

            await _userPointRecordRepository.InsertAsync(new UserPointRecordDO()
            {
                UserId = Guid.Parse(request.UserId),
                BusinessType = 0,
                OperationType = request.OperateType.ToString(),
                Description = EnumHelper.GetEnumDescription(request.OperateType),
                PointValue = request.PointValue,
                ReferrerNo = request.ReferrerNo,
                Remark = request.Remark,
                CreateBy = request.SubmitBy,
                CreateTime = DateTime.Now,
                UpdateBy = "",
                UpdateTime = new DateTime(1900, 1, 1)
            });

            UserPointDO userPointDo = new UserPointDO()
            {
                UserId = Guid.Parse(request.UserId),
                CurrentPoint = request.PointValue
            };
            if (userPoint == null)
            {
                userPointDo.CreateBy = request.SubmitBy;
                userPointDo.CreateTime = DateTime.Now;
                userPointDo.UpdateBy = "";
                userPointDo.UpdateTime = new DateTime(1900, 1, 1);
            }
            else
            {
                userPointDo.Id = userPoint.Id;
                userPointDo.UpdateBy = request.SubmitBy;
                userPointDo.UpdateTime = DateTime.Now;
            }

            await _userPointRepository.OperatePointPoint(userPointDo);

            return true;
        }

        /// <summary>
        /// 操作用户成长值
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> OperateUserGrowthValue(OperateUserGrowthValueRequest request)
        {
            var userPoint = await _userPointRepository.GetUserPointAsync(request.UserId, false);
            if (request.GrowthValue < 0)
            {
                if (userPoint == null)
                {
                    throw new CustomException("当前用户暂无成长值");
                }
            }
            await _userPointRecordRepository.InsertAsync(new UserPointRecordDO()
            {
                UserId = Guid.Parse(request.UserId),
                BusinessType = 1,
                OperationType = request.OperateType.ToString(),
                Description = EnumHelper.GetEnumDescription(request.OperateType),
                PointValue = request.GrowthValue,
                Remark = request.Remark,
                CreateBy = request.SubmitBy,
                CreateTime = DateTime.Now,
                UpdateBy = "",
                UpdateTime = new DateTime(1900, 1, 1)
            });

            UserPointDO userPointDo = new UserPointDO()
            {
                UserId = Guid.Parse(request.UserId),
                CurrentGrowthValue = request.GrowthValue
            };
            if (userPoint == null)
            {
                userPointDo.CreateBy = request.SubmitBy;
                userPointDo.CreateTime = DateTime.Now;
                userPointDo.UpdateBy = "";
                userPointDo.UpdateTime = new DateTime(1900, 1, 1);
            }
            else
            {
                userPointDo.Id = userPoint.Id;
                userPointDo.UpdateBy = request.SubmitBy;
                userPointDo.UpdateTime = DateTime.Now;
            }

            await _userPointRepository.OperateUserGrowth(userPointDo);

            return true;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="request"></param>
        public async Task<string> CreateUser(CreateUserRequest request)
        {
            var user = await _userRepository.GetUserInfoByUserTel(request.UserTel, false);
            if (user != null)
            {
                return user.Id.ToString();
            }

            Guid userId = Guid.NewGuid();
            UserDO userDo = new UserDO()
            {
                Id = userId,
                UserName = request.UserName,
                NickName = request.NickName,
                HeadUrl = request.HeadUrl,
                Gender = request.Gender,
                PersonalSign = request.PersonalSign,
                MobileNumber = request.UserTel,
                IdNumber = request.IdNumber,
                ContactAddress = request.Address,
                Channel = (int)request.Channel,
                ReferrerType = (int)request.ReferrerType,
                ReferrerShopId = request.ReferrerShopId,
                ReferrerUserId = request.ReferrerUserId,
                CreateBy = request.SubmitBy,
                CreateTime = DateTime.Now
            };
            if (!string.IsNullOrEmpty(request.BirthDay))
            {
                userDo.BirthDay = DateTime.Parse(request.BirthDay);
            }
            if (!string.IsNullOrEmpty(request.DriverLicenseExpiry))
            {
                userDo.DriverLicenseExpiry = DateTime.Parse(request.DriverLicenseExpiry);
            }

            var result = await _userRepository.InsertAsync<Guid>(userDo);
            return userId.ToString();
        }

        /// <summary>
        /// 搜索用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<UserBaseInfoVo>> GetCommonUserInfo(CommonUserInfoRequest request)
        {
            _logger.Info($"GetCommonUserInfo_Start Para={JsonConvert.SerializeObject(request)}");
            if ((request.UserIdList == null || !request.UserIdList.Any()) && string.IsNullOrEmpty(request.EmployeeId) &&
                string.IsNullOrEmpty(request.SearchContent) && string.IsNullOrEmpty(request.UserName) && string.IsNullOrEmpty(request.UserTel))
            {
                throw new CustomException("查询条件不能为空");
            }

            var result =
                await _userRepository.GetCommonUserInfo(request.UserIdList, request.EmployeeId, request.SearchContent, request.UserName, request.UserTel);

            return result?.Select(_ => new UserBaseInfoVo
            {

                UserId = _.Id.ToString(),
                UserName = _.UserName,
                NickName = _.NickName,
                Gender = _.Gender,
                BirthDay = _.BirthDay > new DateTime(1900, 1, 1) ? _.BirthDay.ToString("yyyy-MM-dd") : "",
                UserTel = _.MobileNumber,
                Email = _.Email,
                UserTelDes = FormatHelper.FormatTel(_.MobileNumber)

            })?.ToList() ?? new List<UserBaseInfoVo>();
        }


        /// <summary>
        /// 获取用户分享获得的新客数量
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<int> GetReferrerCustomerNum(ReferrerCustomerRequest req)
        {
            return await _userRepository.GetReferrerCustomerNum(req);
           
        }

        public async Task<ApiPagedResultData<UserInfoVo>> GetReferrerCustomerList(ReferrerCustomerListRequest req)
        {
            List<UserInfoVo> data = new List<UserInfoVo>();
            var result = await _userRepository.GetReferrerCustomerList(req);
            var userList = result?.Items?.ToList() ?? new List<UserDO>();
            if (userList.Any())
            {
                userList.ForEach(_ =>
                {
                    UserInfoVo itemUser = new UserInfoVo()
                    {
                        UserId = _.Id.ToString(),
                        UserName = _.UserName,
                        NickName = _.NickName,
                        HeadUrl = _.HeadUrl,
                        Gender = _.Gender,
                        Email = _.Email,
                        BirthDay = _.BirthDay > new DateTime(1900, 1, 1) ? _.BirthDay.ToString("yyyy-MM-dd") : "",
                        UserTel = _.MobileNumber,
                        UserTelDes = FormatHelper.FormatTel(_.MobileNumber)
                    };                   

                    data.Add(itemUser);
                });
            }

            return new ApiPagedResultData<UserInfoVo>()
            {
                Items = data,
                TotalItems = result?.TotalItems ?? 0
            };

        }


        public async Task<ShopReferrerCustomerResDTO> GetShopNewCustomerInfo(ShopReferrerCustomerReqDTO req)
        {
            return await _userRepository.GetShopNewCustomerInfo(req);
        }

        public async Task<ApiPagedResultData<EmployeeReferrerCustomerResDTO>> GetEmployeeReferrerNewCustomerPage(EmployeeReferrerCustomerPageReqDTO req)
        {
            var res = await _userRepository.GetEmployeeReferrerNewCustomerPage(req);
            return new ApiPagedResultData<EmployeeReferrerCustomerResDTO>
            {
                TotalItems = res.TotalItems,
                Items = res.Items
            };
        }

        /// <summary>
        /// 刷新用户成长值（job）
        /// </summary>
        /// <returns></returns>
        public async Task<bool> RefreshUserGrowthValue()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 搜索用户 top100
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<UserInfoVo>> SearchUserInfo(SearchUserInfoRequest request)
        {
            List<UserInfoVo> data = new List<UserInfoVo>();
            var memberGradeResultTask = _memberGradeEnumRepository.GetMemberGradeEnumAsync();
            var result = await _userRepository.SearchUserInfo(new SearchUserInfoCondition()
            {
                UserName = request.UserName,
                UserTel = request.UserTel,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            });
            var userList = result?.Items?.ToList() ?? new List<UserDO>();
            if (userList.Any())
            {
                var userIds = userList.Select(_ => _.Id.ToString()).ToList();
                var pointResult = await _userPointRepository.GetUserPointByUserIds(userIds);
                var memberGradeResult = (await memberGradeResultTask)?.ToList() ?? new List<MemberGradeEnumDO>();
                userList.ForEach(_ =>
                {
                    UserInfoVo itemUser = new UserInfoVo()
                    {
                        UserId = _.Id.ToString(),
                        UserName = _.UserName,
                        NickName = _.NickName,
                        UserTel = _.MobileNumber,
                        UserTelDes = FormatHelper.FormatTel(_.MobileNumber)
                    };
                    int currentGradeValue = pointResult.FirstOrDefault(f => f.UserId == _.Id)?.CurrentGrowthValue ?? 0;
                    var currentGrade = memberGradeResult.FirstOrDefault(t =>
                        t.StartValue <= currentGradeValue && t.EndValue >= currentGradeValue);
                    if (currentGrade != null)
                    {
                        itemUser.MemberLevel = currentGrade.Description;
                        itemUser.MemberGrade = currentGrade.MemberGrade;
                    }

                    data.Add(itemUser);
                });
            }

            return new ApiPagedResultData<UserInfoVo>()
            {
                Items = data,
                TotalItems = result?.TotalItems ?? 0
            };
        }

        /// <summary>
        /// 获取用户默认门店信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<DefaultRecommendShopResponse> GetDefaultRecommendShop(DefaultRecommendShopRequest request)
        {
            long defaultShopId = Convert.ToInt64(_configuration["QpConfig:DefaultRecommendShopId"]);
            long shopId = request.ShopId;
            var userExpand = await _userExpandShopRepository.GetDefaultUserExpandShop(request.UserId);
            if (userExpand != null)
            {
                //判断是否店长
                if (userExpand.RelationShopId > 0)
                {
                    var user = await _userRepository.GetAsync(request.UserId);
                    if (user == null)
                    {
                        throw new CustomException("客户信息不存在");
                    }

                    return new DefaultRecommendShopResponse()
                    {
                        IsShopManager = 1,
                        ShopId = userExpand.RelationShopId,
                        ShopManagerName = user.NickName ?? user.UserName ?? user.MobileNumber,
                        HeadUrl = user.HeadUrl
                    };
                }
                else
                {
                    if (shopId == 0 && userExpand.RecommendShopId > 0)
                    {
                        shopId = userExpand.RecommendShopId;
                    }
                }
            }

            if (shopId == 0)
            {
                shopId = defaultShopId;
            }

            var user1 = await _userRepository.GetUserByRelationShopId(shopId);
            if (user1 == null)
            {
                throw new CustomException("店长信息不存在");
            }

            var updateTask = UpdateOrInsertUserExtendShop(request.UserId, request.ShopId);

            return new DefaultRecommendShopResponse()
            {
                IsShopManager = 0,
                ShopId = shopId,
                ShopManagerName = user1.NickName ?? user1.UserName ?? user1.MobileNumber,
                HeadUrl = user1.HeadUrl
            };
        }

        /// <summary>
        /// 获取UserExpandShop
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserExpandShopVo> GetUserExpandShop(GetUserExpandShopRequest request)
        {
            var userExpand = await _userExpandShopRepository.GetDefaultUserExpandShop(request.UserId);
            if (userExpand != null)
            {
                return new UserExpandShopVo()
                {
                    UserId = userExpand.UserId.ToString(),
                    RelationShopId = userExpand.RelationShopId,
                    RecommendShopId = userExpand.RecommendShopId
                };
            }

            return null;
        }

        /// <summary>
        /// 更新默认推荐店
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        private async Task<bool> UpdateOrInsertUserExtendShop(string userId, long shopId)
        {
            if (shopId > 0)
            {
                var userExpandTask = _userExpandShopRepository.GetDefaultUserExpandShop(userId);
                var userExpand1Task = _userExpandShopRepository.GetDefaultUserExpandShop(shopId);
                await Task.WhenAll(userExpandTask, userExpand1Task);
                var userExpand = userExpandTask.Result;
                var userExpand1 = userExpand1Task.Result;
                if (userExpand1 != null)
                {
                    if (userExpand != null && userExpand.RelationShopId == 0 && userExpand.RecommendShopId != shopId)
                    {
                        await _userExpandShopRepository.UpdateAsync(new UserExpandShopDo()
                        {
                            Id = userExpand.Id,
                            RecommendShopId = shopId,
                            RecommendUserId = userExpand1.UserId,
                            UpdateBy = "System",
                            UpdateTime = DateTime.Now
                        }, new List<string>()
                        {
                            "RecommendShopId", "RecommendUserId", "UpdateBy", "UpdateTime"
                        });
                    }
                    else if (userExpand == null)
                    {
                        await _userExpandShopRepository.InsertAsync(new UserExpandShopDo()
                        {
                            UserId = Guid.Parse(userId),
                            RecommendShopId = shopId,
                            RecommendUserId = userExpand1.UserId,
                            CreateBy = "System",
                            CreateTime = DateTime.Now
                        });
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 成为店长 - 关联用户和门店信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> RelationShopAndUser(RelationShopAndUserRequest request)
        {
            var userExpand = await _userExpandShopRepository.GetDefaultUserExpandShop(request.UserId);
            if (userExpand != null)
            {
                if (userExpand.RelationShopId != request.ShopId)
                {
                    if (userExpand.RelationShopId > 0)
                    {
                        throw new CustomException("该用户已是店长");
                    }
                    else
                    {
                        await _userExpandShopRepository.UpdateAsync(new UserExpandShopDo()
                        {
                            Id = userExpand.Id,
                            RelationShopId = request.ShopId,
                            UpdateBy = request.SubmitBy,
                            UpdateTime = DateTime.Now
                        }, new List<string>()
                        {
                            "RelationShopId", "UpdateBy", "UpdateTime"
                        });
                    }
                }
            }
            else
            {
                await _userExpandShopRepository.InsertAsync(new UserExpandShopDo()
                {
                    UserId = Guid.Parse(request.UserId),
                    RelationShopId = request.ShopId,
                    CreateBy = request.SubmitBy,
                    CreateTime = DateTime.Now

                });
            }

            return true;
        }

        /// <summary>
        /// 实名认证
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserAuthResponse> UserAuth(UserAuthRequest request)
        {
            if (!Guid.TryParse(request.UserId, out Guid userId))
            {
                throw new CustomException("userId格式有误");
            }

            UserAuthParaCheck(request);

            var existAuth = await _userAuthenticationRepository.GetUserAuthentication(request.UserId, false);
            if (existAuth != null)
            {
                throw new CustomException("该用户已实名认证");
            }

            await _userAuthenticationRepository.InsertAsync(new UserAuthenticationDO()
            {
                UserId = userId,
                AuthType = request.AuthType,
                AuthName = request.AuthName,
                AuthIdCard = request.AuthIdCard,
                CreateBy = request.UserId,
                CreateTime = DateTime.Now
            });

            return new UserAuthResponse()
            {
                PassAuth = true
            };
        }

        /// <summary>
        /// 参数校验
        /// </summary>
        /// <param name="request"></param>
        private void UserAuthParaCheck(UserAuthRequest request)
        {
            switch (request.AuthType)
            {
                //二代身份证
                case 0:
                    if (!IdentityCardValidation.CheckIdentityName(request.AuthName))
                    {
                        throw new CustomException("用户姓名格式有误");
                    }

                    if (!IdentityCardValidation.CheckIdCard(request.AuthIdCard))
                    {
                        throw new CustomException("请输入正确的身份证号码");
                    }

                    break;
            }
        }

        /// <summary>
        /// 添加银行卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddUserBankCard(AddUserBankCardRequest request)
        {
            if (!Guid.TryParse(request.UserId, out Guid userId))
            {
                throw new CustomException("userId格式有误");
            }

            var userBankCard = await _userBankCardRepository.GetUserBank(request.UserId, request.CardNumber, false);
            if (userBankCard.Any())
            {
                throw new CustomException("该银行卡已添加");
            }

            await _userBankCardRepository.InsertAsync(new UserBankCardDO()
            {
                UserId = userId,
                BankName = request.BankName,
                CardNumber = request.CardNumber,
                PhoneNo = request.PhoneNo,
                CreateBy = request.UserId,
                CreateTime = DateTime.Now
            });

            return true;
        }
    }
}
