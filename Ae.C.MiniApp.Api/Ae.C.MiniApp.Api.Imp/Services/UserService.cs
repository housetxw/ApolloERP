using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Profile;
using AutoMapper;
using ApolloErp.Common.ValidateCode;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Redis;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Model.UserAddress;
using Ae.C.MiniApp.Api.Client.Clients.Interface;
using Ae.C.MiniApp.Api.Client.Inteface;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Client.Model.OrderComment;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Client.Request.Coupon;
using Ae.C.MiniApp.Api.Client.Request.OrderComment;
using Ae.C.MiniApp.Api.Client.Request.Product;
using Ae.C.MiniApp.Api.Client.Request.User;
using Ae.C.MiniApp.Api.Client.Request.UserAddress;
using Ae.C.MiniApp.Api.Client.Response.Product;
using Ae.C.MiniApp.Api.Common.Exceptions;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Model.User;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Request.User;
using Ae.C.MiniApp.Api.Core.Response.User;
using Ae.C.MiniApp.Api.Core.Response.UserAddress;
using Ae.C.MiniApp.Api.Core.Request.SharingPromotion;
using Ae.C.MiniApp.Api.Client.Response.User;

namespace Ae.C.MiniApp.Api.Imp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserClient _userClient;
        private readonly IVehicleClient _vehicleClient;
        private readonly IMapper _mapper;
        private readonly RedisClient _redisClient;
        private readonly string redisKey = "Rg:C:MiniApp:ChangeMobile";
        private readonly string _source = "MiniApp";
        private readonly ApolloErpLogger<UserService> _logger;
        private readonly IProductClient _productClient;
        private readonly IUserAddressClient _userAddressClient;
        private readonly IOrderCommentClient _orderCommentClient;
        private readonly IIdentityService _identityService;
        private readonly ICouponClient _couponClient;
        private readonly IOrderQueryClient _orderQueryClient;

        public UserService(IUserClient userClient, IVehicleClient vehicleClient, IMapper mapper,
            RedisClient redisClient, ApolloErpLogger<UserService> logger, IProductClient productClient,
            IUserAddressClient userAddressClient, IOrderCommentClient orderCommentClient,
            IIdentityService identityService, ICouponClient couponClient, IOrderQueryClient orderQueryClient)
        {
            _userClient = userClient;
            _vehicleClient = vehicleClient;
            _mapper = mapper;
            _redisClient = redisClient;
            _logger = logger;
            _productClient = productClient;
            _userAddressClient = userAddressClient;
            _orderCommentClient = orderCommentClient;
            _identityService = identityService;
            _couponClient = couponClient;
            _orderQueryClient = orderQueryClient;
        }

        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserInfoResponse> GetUserInfo(string userId)
        {
            var userData = await _userClient.GetUserInfo(new GetUserInfoClientRequest()
            {
                UserId = userId
            });
            var defaultVehicle = await _vehicleClient.GetUserDefaultVehicleAsync(new UserDefaultVehicleClientRequest()
            {
                UserId = userId
            });
            if (userData != null)
            {
                var result = new UserInfoResponse
                {
                    UserName = userData.UserName,
                    NickName = userData.NickName,
                    HeadUrl = userData.HeadUrl,
                    Gender = userData.Gender,
                    BirthDay = userData.BirthDay,
                    UserTel = userData.UserTel,
                    UserTelDes = userData.UserTelDes,
                    MemberLevel = userData.MemberLevel,
                    MemberGrade = userData.MemberGrade,
                    MemberUrl = userData.MemberUrl,
                    Point = userData.Point,
                    DriverLicenseExpiry = !userData.DriverLicenseExpiry.Contains("1900") ? userData.DriverLicenseExpiry : "",
                    PackageCardNum=0,
                };

                if (userData.DefaultAddress != null)
                {
                    result.DefaultAddress = _mapper.Map<UserAddressVO>(userData.DefaultAddress);
                }

                if (defaultVehicle != null)
                {
                    result.DefaultVehicle = _mapper.Map<UserVehicleVO>(defaultVehicle);
                }

                try
                {
                    var availCouponCount = await _couponClient.GetUserCouponUnusedAmountByUserId(
                        new UserCouponUnusedAmountByUserIdRequest()
                        {
                            UserId = userId
                        });
                    result.AvailCouponCount = availCouponCount;

                    var getPackageCard = await _orderQueryClient.GetPackageCardMainInfo(new Core.Request.OrderQuery.GetPackageCardMainInfoRequest()
                    {
                        UserId = userId
                    });

                    result.PackageCardNum = getPackageCard?.Data?.Count()??0;
                }
                catch (Exception ex)
                {
                    _logger.Error($"GetUserCouponUnusedAmountByUserId_Error {ex}");
                }

                return result;
            }

            return null;
        }

        /// <summary>
        /// 个人积分
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserPointResponse> GetUserPoint(string userId)
        {
            var result = await _userClient.GetUserPoint(new UserPointClientRequest()
            {
                UserId = userId
            });
            if (result != null)
            {
                return _mapper.Map<UserPointResponse>(result);
            }

            return null;
        }

        /// <summary>
        /// 成长等级
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<MemberLevelResponse> GetMemberLevel(string userId)
        {
            var result = await _userClient.GetMemberLevel(new MemberLevelClientRequest()
            {
                UserId = userId
            });
            if (result != null)
            {
                return _mapper.Map<MemberLevelResponse>(result);
            }

            return null;
        }

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditUserInfo(EditUserInfoRequest request)
        {
            EditUserInfoClientRequest clientRequest = new EditUserInfoClientRequest()
            {
                UserId = request.UserId,
                UserName = request.UserName,
                NickName = request.NickName,
                HeadUrl = request.HeadUrl,
                Gender = request.Gender,
                BirthDay = request.BirthDay,
                PersonalSign = request.PersonalSign,
                SubmitBy = request.UserId,
                DriverLicenseExpiry = request.DriverLicenseExpiry
            };
            var result = await _userClient.EditUserInfo(clientRequest);

            return result;
        }

        /// <summary>
        /// 发送修改手机号验证码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> SendChangeMobileVerificationCodeSms(SendVerifyCodeSmsRequest request)
        {
            //一分钟内只能发送一次请求
            if (_redisClient.Redis.KeyExists(redisKey + ":Expire:VerCode:" + request.MobilePhone))
            {
                throw new CustomException("操作太频繁");
            }

            //生成验证码
            string code = ValidateCodeHelper.CreateValidateCode(4);
            SendSms(request.MobilePhone, code);
            //验证码放到缓存中
            _redisClient.Redis.StringSet(redisKey + ":Expire:VerCode:" + request.MobilePhone, code,
                TimeSpan.FromSeconds(60));
            _redisClient.Redis.StringSet(redisKey + ":VerCode:" + request.MobilePhone, code, TimeSpan.FromSeconds(300));

            return await Task.FromResult(true);
        }

        private void SendSms(string phone, string code)
        {
            IClientProfile profile =
                DefaultProfile.GetProfile("", "", "");
            DefaultAcsClient client = new DefaultAcsClient(profile);
            CommonRequest request = new CommonRequest();
            request.Method = MethodType.POST;
            request.Domain = "dysmsapi.aliyuncs.com";
            request.Version = "2017-05-25";
            request.Action = "SendSms";
            request.AddQueryParameters("PhoneNumbers", phone);
            request.AddQueryParameters("SignName", "总部");
            request.AddQueryParameters("TemplateCode", "SMS_175");
            request.AddQueryParameters("TemplateParam", "{\"code\":\"" + code + "\"}");
            try
            {
                _logger.Info($"发送短信开始:phone:{phone},signName:,修改手机号验证码{code}");
                CommonResponse response = client.GetCommonResponse(request);
                _logger.Info(
                    $"发送短信结束:phone:{phone},signName:,修改手机号验证码{code},result:{System.Text.Encoding.Default.GetString(response.HttpResponse.Content)}");
            }
            catch (ServerException e)
            {
                _logger.Error($"发送短信开始:phone:{phone},验证码{code}异常", e);
            }
            catch (ClientException e)
            {
                _logger.Error($"发送短信开始:phone:{phone},验证码{code}异常", e);
            }
        }

        /// <summary>
        /// 验证当前手机号
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> VerifyCurrentMobile(VerifyCurrentMobileRequest request, string userId)
        {
            var key = redisKey + ":VerCode:" + request.MobilePhone;
            if (!_redisClient.Redis.KeyExists(key))
            {
                throw new CustomException("验证码已过期");
            }

            var sessionCode = _redisClient.Redis.StringGet(key).ToString();
            if (!sessionCode.Equals(request.SecurityCode))
            {
                throw new CustomException("验证码不正确");
            }

            var userInfo = await _userClient.GetUserInfo(new GetUserInfoClientRequest()
            {
                UserId = userId
            });

            if (userInfo == null)
            {
                throw new CustomException("当前用户不存在");
            }

            if (!userInfo.UserTel.Equals(request.MobilePhone))
            {
                throw new CustomException("当前用户手机号不正确");
            }

            return true;
        }

        /// <summary>
        /// 修改用户手机号
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUserMobile(UpdateUserMobileRequest request, string userId)
        {
            var key = redisKey + ":VerCode:" + request.MobilePhone;
            if (!_redisClient.Redis.KeyExists(key))
            {
                throw new CustomException("验证码已过期");
            }

            var sessionCode = _redisClient.Redis.StringGet(key).ToString();
            if (!sessionCode.Equals(request.SecurityCode))
            {
                throw new CustomException("验证码不正确");
            }

            var userData = await _userClient.GetUserInfoByUserTel(new UserInfoByUserTelClientRequest()
            {
                UserTel = request.MobilePhone
            });

            if (userData != null)
            {
                throw new CustomException("该手机已存在客户");
            }

            EditUserInfoClientRequest clientRequest = new EditUserInfoClientRequest()
            {
                UserId = userId,
                UserTel = request.MobilePhone,
                SubmitBy = _identityService.GetUserId()
            };
            var result = await _userClient.EditUserInfo(clientRequest);

            return result;
        }

        /// <summary>
        /// 添加关注商品
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> AddPersonalProduct(AddPersonalProductRequest request, string userId)
        {
            var result = await _userClient.AddPersonalProduct(new AddPersonalProductClientRequest()
            {
                PidList = request.PidList,
                UserId = userId,
                SubmitBy = _identityService.GetUserId(),
                Source = _source
            });


            return result;
        }

        /// <summary>
        /// 取消关注商品
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> CancelPersonalProduct(CancelPersonalProductRequest request, string userId)
        {
            var result = await _userClient.CancelPersonalProduct(new CancelPersonalProductClientRequest()
            {
                PidList = request.PidList,
                SubmitBy = _identityService.GetUserId(),
                UserId = userId
            });
            return result;
        }

        /// <summary>
        /// 我的关注商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<UserProductVo>> GetUserAttentionProducts(
            UserAttentionProductsRequest request)
        {
            ApiPagedResultData<UserProductVo> result = new ApiPagedResultData<UserProductVo>();
            List<string> pidList = new List<string>();
            List<UserProductVo> userProducts = new List<UserProductVo>();
            int totalCount = 0;
            var userAttention = await _userClient.GetUserAttentionProduct(new UserAttentionProductClientRequest()
            {
                UserId = request.UserId
            });
            if (userAttention != null && userAttention.Any())
            {
                pidList = userAttention.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                    .ToList();
                totalCount = userAttention.Count;
            }

            if (pidList.Any())
            {
                var productsTask = _productClient.ProductDetail(new ProductDetailSearchClientRequest()
                { ProductCodes = pidList });
                var commentTask = _orderCommentClient.GetProductCommentTotalAsync(new ProductCommentTotalClientRequest()
                {
                    ProductIds = pidList
                });
                await Task.WhenAll(productsTask, commentTask);
                var products = productsTask.Result ?? new List<ProductDetailClientResponse>();
                var comment = commentTask.Result ?? new List<ProductCommentTotalDto>();
                if (products.Any())
                {
                    products.ForEach(_ =>
                    {
                        userProducts.Add(new UserProductVo()
                        {
                            Pid = _.Product.ProductCode,
                            DisplayName = _.Product.DisplayName,
                            Price = _.Product.SalesPrice,
                            ImageUrl = _.Product.Image1,
                            FeedbackRate = _.Product.FavorableRate,
                            CommentCount = comment.FirstOrDefault(t => t.ProductId == _.Product.ProductCode)
                                               ?.CommentCount ?? 0
                        });
                    });
                }
            }

            result.Items = userProducts;
            result.TotalItems = totalCount;

            return result;
        }

        /// <summary>
        /// 获取用户所有地址信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<UserAddressVO>> GetUserAddress(string userId)
        {
            var result = (await _userAddressClient.GetUserAddressList(new GetUserAddressClientRequest()
            {
                UserId = userId
            }))?.Data?.AddressVOs ?? new List<UserAddressDTO>();
            result = result.OrderByDescending(_ => _.DefaultAddress).ToList();
            return _mapper.Map<List<UserAddressVO>>(result);
        }

        /// <summary>
        /// 获取用户默认地址
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserAddressVO> GetUserDefaultAddress(string userId)
        {
            var result = (await _userAddressClient.GetUserAddressList(new GetUserAddressClientRequest()
            {
                UserId = userId
            }))?.Data?.AddressVOs ?? new List<UserAddressDTO>();

            var response = _mapper.Map<List<UserAddressVO>>(result);

            var defaultAddress = response.FirstOrDefault(_ => _.DefaultAddress);
            if (defaultAddress == null)
            {
                defaultAddress = response.FirstOrDefault();
            }

            return defaultAddress;
        }

        /// <summary>
        /// 新增用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> AddUserAddress(AddUserAddressRequest request)
        {
            var clientRequest = _mapper.Map<CreateUserAddressClientRequest>(request);
            clientRequest.CreateBy = _identityService.GetUserId();
            var result = await _userAddressClient.CreateUserAddress(clientRequest);
            return result.Data;
        }

        /// <summary>
        /// 编辑用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditUserAddress(EditUserAddressRequest request)
        {
            var clientRequest = _mapper.Map<UpdateUserAddressClientRequest>(request);
            clientRequest.UpdateBy = _identityService.GetUserId();
            clientRequest.Id = request.AddressId;
            var result = await _userAddressClient.UpdateUserAddress(clientRequest);
            return result.Data;
        }

        /// <summary>
        /// 删除用户地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUserAddress(DeleteUserAddressRequest request)
        {
            var clientRequest = new DeleteUserAddressClientRequest()
            {
                AddressId = request.AddressId,
                UserId = request.UserId,
                UpdateBy = _identityService.GetUserId()
            };
            var result = await _userAddressClient.DeleteUserAddress(clientRequest);
            return result.Data;
        }

        /// <summary>
        /// 设置默认地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> SetDefaultAddress(SetDefaultAddressRequest request)
        {
            var result = await _userAddressClient.SetDefaultAddress(new SetDefaultAddressClientRequest()
            {
                AddressId = request.AddressId,
                UserId = request.UserId,
                SubmitBy = _identityService.GetUserId()
            });

            return result;
        }

        /// <summary>
        /// 创建用户地址标签
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> CreateUserAddressTag(CreateUserAddressTagRequest request)
        {
            var clientRequest = new CreateUserAddressTagClientRequest()
            {
                UserId = request.UserId,
                TagName = request.TagName,
                CreateBy = _identityService.GetUserId()
            };
            var result = await _userAddressClient.CreateUserAddressTag(clientRequest);
            return result;
        }

        /// <summary>
        /// 获取用户标签列表
        /// </summary>
        /// <returns></returns>
        public async Task<UserAddressTagResponse> GetUserAddressTagList(string userId)
        {
            var result = await _userAddressClient.GetUserAddressTagList(new GetUserAddressTagClientRequest()
            {
                UserId = userId
            });

            var vo = _mapper.Map<UserAddressTagResponse>(result.Data);
            return vo;
        }


        public async Task<ApiPagedResult<UserInfoResponse>> GetReferrerCustomerList(GetSharingOrdersRequest request)
        {
            request.UserId = _identityService.GetUserId();
            //request.UserId = "1";
            var clientResponse = await _userClient.GetReferrerCustomerList(request);
            var result = _mapper.Map<ApiPagedResult<UserInfoResponse>>(clientResponse);
                
            return result;
        }


    }
}
