using Ae.C.Login.Api.Dal.Repositorys.User;
using Ae.C.Login.Api.Dal.Repositorys.UserThird;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApolloErp.Data.DapperExtensions;
using Ae.C.Login.Api.Core.Model;
using AutoMapper;
using System.Threading.Tasks;
using Ae.C.Login.Api.Core.Request;
using Ae.C.Login.Api.Core.Interfaces;
using ApolloErp.Log;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Ae.C.Login.Api.Core.Enums;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Ae.C.Login.Api.Core.Response;
using Ae.C.Login.Api.Client.Clients.WeChatService;
using Ae.C.Login.Api.Client.Request;
using Ae.C.Login.Api.Common.Exceptions;
using Ae.C.Login.Api.Dal.Model;
using System.Transactions;
using Newtonsoft.Json;
using System.Security.Cryptography;
using Ae.C.Login.Api.Common.Cache;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Http;
using ApolloErp.Redis;
using ApolloErp.Common.ValidateCode;
using Rg.LoginAuthentication.Api.Dal.Repositorys.IDAL;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ApolloErp.Common.GuidHelper;
using Ae.C.Login.Api.Client.Clients.CouponServer;
using Ae.C.Login.Api.Client.Clients.UserServer;
using Ae.C.Login.Api.Imp.Helpers;
using ApolloErp.Message.Sms;
using Ae.C.Login.Api.Common;
using Ae.C.Login.Api.Common.Constant;
using Ae.C.Login.Api.Common.Extension;

namespace Ae.C.Login.Api.Imp.Services
{
    public class LoginService : ILoginService
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly ApolloErpLogger<LoginService> logger;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly IWeChatClient weChatClient;
        private readonly ICouponClient copClient;
        private readonly IUserClient userClient;
        private readonly ILoginAuthService iLoginAuthService;
        private readonly IUserRespository iUserRespository;
        private readonly IUserThirdRespository iUserThirdRespository;
        private readonly ILoginLogRepository iLoginLogRepository;
        private readonly RedisClient redisClient;
        private readonly string redisKey = "Rg:C:LoginAuthentication";
        private readonly IHttpContextAccessor _context;
        private readonly SmsClient smsClient;

        public LoginService(ApolloErpLogger<LoginService> logger, IMapper mapper, IConfiguration configuration,
            IUserThirdRespository iUserThirdRespository,
            IUserRespository iUserRespository,
            ILoginLogRepository iLoginLogRepository,
            IWeChatClient weChatClient,
            ICouponClient copClient,
            IUserClient userClient,
            ILoginAuthService iLoginAuthService,
            RedisClient redisClient,
            IHttpContextAccessor context,
            SmsClient smsClient)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.configuration = configuration;
            this.iUserRespository = iUserRespository;
            this.iLoginAuthService = iLoginAuthService;
            this.weChatClient = weChatClient;
            this.copClient = copClient;
            this.userClient = userClient;
            this.iUserThirdRespository = iUserThirdRespository;
            this.redisClient = redisClient;
            this.iLoginLogRepository = iLoginLogRepository;
            this.smsClient = smsClient;
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // ---------------------------------- 接口实现 --------------------------------------
        public async Task<GetWxOpenIdResponse> GetWxOpenId(GetWxOpenIdRequest request, string refreshUri)
        {
            GetWxOpenIdResponse result = new GetWxOpenIdResponse();

            //调用微信接口获取openid
            GetJsCodeResponse wxOpenId = await weChatClient.GetJscode2session((LoginPlatform)request.PlatForm, request.WxCode);
            if (wxOpenId == null || string.IsNullOrEmpty(wxOpenId.openid) || wxOpenId.errcode != 0)
            {
                string ermg = wxOpenId == null ? "未获取到用户openid" : wxOpenId.errmsg;
                logger.Warn($"微信GetJscode2session:Request:{request.WxCode}调用失败{ermg}");
                throw new CustomException("登录失败,请手动登录");
            }
            //logger.Info($"微信GetJscode2session:Request:{JsonConvert.SerializeObject(request)},wxOpenId:{JsonConvert.SerializeObject(wxOpenId)}");

            //缓存微信的session_key
            await redisClient.Redis.StringSetAsync(redisKey + ":session_key:" + wxOpenId.openid, wxOpenId.session_key,
                TimeSpan.FromDays(7));

            if (string.IsNullOrEmpty(wxOpenId.unionid))
            {
                //解密unionid
                string UnionidStr = Decrypt(request.EncryptedData ?? "", request.Iv ?? "", wxOpenId.session_key);
                GetWxUnionid unionid = JsonConvert.DeserializeObject<GetWxUnionid>(UnionidStr);
                if (unionid == null || string.IsNullOrEmpty(unionid.UnionId))
                {
                    logger.Error($"微信unionid解密失败:Request:{JsonConvert.SerializeObject(request)}，session_key：{wxOpenId.session_key}" +
                        $",UnionidStr = {UnionidStr}");
                    throw new CustomException("登录失败");
                }
                else
                {
                    result.UnionId = unionid.UnionId;
                }
            }
            else 
            {
                result.UnionId = wxOpenId.unionid;
            }

            result.OpenId = wxOpenId.openid;
            //根据openid获取用户信息
            UserInfoDO user = await iUserRespository.GetUserInfoByOpenId(result.UnionId, (int)ThirtyLoginType.WeChat);
            //获取不到用户，说明openid没有注册过
            if (user == null)
            {
                result.IsExistUser = false;
                return result;
            }

            //获取到用户，说明openid已经注册过了,颁发登录token
            var userInfo = mapper.Map<UserInfoVO>(user);
            var tokenInfo = iLoginAuthService.GetTokenInfo(userInfo, refreshUri);
            result.IsExistUser = true;
            if (string.IsNullOrEmpty(userInfo.NickName))
            {
                userInfo.NickName = userInfo.MobileNumber ?? string.Empty;
            }

            result.TokenInfo = tokenInfo;
            result.UserInfo = userInfo;
            logger.Info($"GetWxOpenId登录成功:user:{JsonConvert.SerializeObject(result)}");
            //记录登录日志
            SysLoginLogDO log = new SysLoginLogDO();
            log.LoginName = userInfo.UserId;
            log.IpAddress = GetIp();
            log.LoginTime = DateTime.Now;
            log.OpenId = result.UnionId;
            log.State = LoginState.Success;
            log.LoginType = LoginType.WeChat;
            log.Comment = request.PlatForm.ToString()+"GetWxOpenId登录成功";
            await iLoginLogRepository.CreateLoginLogAsync(log);

            return result;

        }

        public async Task<LoginResponse> ThirtyLoginRegister(ThirtyLoginRequest request, string refreshUri) 
        {
            //增加测试登录
            if(request.LoginType == ThirtyLoginType.Test)
            {
                LoginResponse resultTest = new LoginResponse();
                var userTest = await iUserRespository.GetUserInfoByMobile("13811112222", (int)request.LoginType);
                
                if (userTest == null)
                {
                    logger.Error($"用户信息获取失败:Request:{JsonConvert.SerializeObject(request)}");
                    throw new CustomException("登录失败");
                }

                var userInfoTest = mapper.Map<UserInfoVO>(userTest);
                var tokenInfoTest = iLoginAuthService.GetTokenInfo(userInfoTest, refreshUri);
                if (string.IsNullOrEmpty(userInfoTest.NickName))
                {
                    userInfoTest.NickName = userInfoTest.MobileNumber ?? string.Empty;
                }

                resultTest.TokenInfo = tokenInfoTest;
                resultTest.UserInfo = userInfoTest;
               
                return resultTest;

            }

            SysLoginLogDO log = new SysLoginLogDO();
            log.IpAddress = GetIp();
            log.LoginTime = DateTime.Now;
            log.LoginType = (LoginType)(int)request.LoginType;
            UserInfoDO useri = null;
            //解析加密数据
            string sessionKey = await redisClient.Redis.StringGetAsync(redisKey + ":session_key:" + request.OpenId);
            //string sessionKey = "ErHwN3K8vMfiVo7FtcCj+Q==";
            if (string.IsNullOrEmpty(sessionKey))
            {
                logger.Error($"sessionKey缓存获取失败:Request:{JsonConvert.SerializeObject(request)}");
                throw new CustomException("登录失败");
            }

            //解密unionid
            //string unionidStr = Decrypt(request.UnionIdEncryptedData, request.UnionIdIv, sessionKey);
            //GetWxUnionid union = JsonConvert.DeserializeObject<GetWxUnionid>(unionidStr);
            //if (union == null || string.IsNullOrEmpty(union.UnionId))
            //{
            //    logger.Error($"三方用户唯一标识解密失败:Request:{JsonConvert.SerializeObject(request)}");
            //    throw new CustomException("登录失败");
            //}

            //解密手机号
            string MobileNumberStr = Decrypt(request.EncryptedData, request.Iv, sessionKey);
            GetPhoneNumer phone = JsonConvert.DeserializeObject<GetPhoneNumer>(MobileNumberStr);
            if (phone == null || string.IsNullOrEmpty(phone.PurePhoneNumber))
            {
                logger.Error($"手机号码解密失败:Request:{JsonConvert.SerializeObject(request)}");
                throw new CustomException("登录失败");
            }

            //根据openid获取用户信息
            UserInfoDO user = await iUserRespository.GetUserInfoByOpenId(request.UnionId, (int)request.LoginType);
            //openid不存在的情况
            if (user == null || string.IsNullOrEmpty(user.OpenId))
            {
                //判断手机号是否存在
                UserInfoDO userPhone = await iUserRespository.GetUserInfoByMobile(phone.PurePhoneNumber, (int)request.LoginType);
                if (userPhone == null || string.IsNullOrEmpty(userPhone.MobileNumber))
                {
                    //手机号也不存在，注册新用户
                    using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        bool isSucess = true;
                        Guid userId = Guid.NewGuid();
                        UserDO userInsert = new UserDO();
                        DateTime date = DateTime.Now;
                        userInsert.Id = userId;
                        userInsert.NickName = request.NickName ?? "";
                        userInsert.MobileNumber = phone.PurePhoneNumber;
                        userInsert.HeadUrl = request.HeadUrl ?? "";
                        userInsert.Gender = request.Gender;
                        userInsert.CreateBy = "system";
                        userInsert.CreateTime = date;
                        userInsert.UpdateTime = date;

                        GenReferrerInfo(request.SceneId,request.ShareUserId, userInsert);

                        isSucess = await iUserRespository.CreateUser(userInsert);
                        if (isSucess)
                        {
                            UserThirdDO userth = new UserThirdDO();
                            userth.UserId = userId.ToString();
                            userth.OpenId = request.UnionId;
                            userth.LoginType = (int)request.LoginType;
                            userth.CreateBy = "system";
                            userth.CreateTime = date;
                            userth.UpdateTime = date;
                            isSucess = await iUserThirdRespository.InsertUserThird(userth);
                        }

                        if (isSucess)
                        {
                            ts.Complete();

                            NewRegisterUserFunc(new UserInfoVO
                            {
                                UserId = userId.ToString(),
                                MobileNumber = userInsert.MobileNumber,
                                Channel = userInsert.Channel,
                                ReferrerUserId = userInsert.ReferrerUserId
                            });
                        }
                        else
                        {
                            logger.Error($"注册失败:Request:{JsonConvert.SerializeObject(request)}");
                            throw new CustomException("登录失败");
                        }

                        //tode发
                    }
                }
                else
                {
                    //判断是否锁定用户
                    if (!userPhone.State.Equals(0))
                    {
                        logger.Error($"账号被锁定无法登陆:Request:{JsonConvert.SerializeObject(request)}");
                        log.State = LoginState.Failure;
                        log.Comment = "账号被锁定";
                        log.LoginName = userPhone.UserId.ToString();
                        await iLoginLogRepository.CreateLoginLogAsync(log);
                        throw new CustomException("账号被锁定");
                    }

                    if (string.IsNullOrEmpty(userPhone.OpenId))
                    {
                        //手机号存在了且openid为空时，更新上去openid
                        UserThirdDO userth = new UserThirdDO();
                        userth.UserId = userPhone.UserId.ToString();
                        userth.OpenId = request.UnionId;
                        userth.LoginType = (int)request.LoginType;
                        userth.CreateBy = "system";
                        DateTime date = DateTime.Now;
                        userth.CreateTime = date;
                        userth.UpdateTime = date;
                        bool isSucess = await iUserThirdRespository.InsertUserThird(userth);
                        if (!isSucess)
                        {
                            logger.Error($"添加三方登录信息失败:Request:{JsonConvert.SerializeObject(request)}");
                            throw new CustomException("登录失败");
                        }
                    }
                }
            }
            //openid存在的情况
            else
            {
                //判断是否锁定用户
                if (!user.State.Equals(0))
                {
                    logger.Error($"账号被锁定无法登陆:Request:{JsonConvert.SerializeObject(request)}");
                    log.State = LoginState.Failure;
                    log.Comment = "账号被锁定";
                    log.LoginName = user.UserId.ToString();
                    await iLoginLogRepository.CreateLoginLogAsync(log);
                    throw new CustomException("账号被锁定");
                }

                useri = user;
            }

            //返回用户信息
            LoginResponse result = new LoginResponse();
            if (user == null)
            {
                useri = await iUserRespository.GetUserInfoByMobile(phone.PurePhoneNumber, (int)request.LoginType);
            }

            if (useri == null)
            {
                logger.Error($"用户信息获取失败:Request:{JsonConvert.SerializeObject(request)}");
                throw new CustomException("登录失败");
            }

            var userInfo = mapper.Map<UserInfoVO>(useri);
            var tokenInfo = iLoginAuthService.GetTokenInfo(userInfo, refreshUri);
            if (string.IsNullOrEmpty(userInfo.NickName))
            {
                userInfo.NickName = userInfo.MobileNumber ?? string.Empty;
            }

            result.TokenInfo = tokenInfo;
            result.UserInfo = userInfo;
            logger.Info($"ThirtyLoginRegister登录成功:user:{JsonConvert.SerializeObject(result)}");
            log.State = LoginState.Success;
            log.Comment = "三方登录成功";
            log.LoginName = userInfo.UserId.ToString();
            await iLoginLogRepository.CreateLoginLogAsync(log);
            return result;
        }

        public async Task<LoginResponse> RgLogin(RgLoginRequest request, string refreshUri)
        {
            SysLoginLogDO log = new SysLoginLogDO();
            log.IpAddress = GetIp();
            log.LoginTime = DateTime.Now;
            log.LoginType = LoginType.SMSCode;

            string specialCode = "RgJFKjkxMjBKU0lKc34lIyJ9"; //特殊验证码，和java登录对接用

            //验证验证码
            string code = await redisClient.Redis.StringGetAsync(redisKey + ":VerCodeValite:" + request.MobileNumber);
            if (specialCode.Equals(request.VerificationCode))
            {
                //不用检查
            }    
            else if (string.IsNullOrEmpty(code) || !code.Equals(request.VerificationCode) )
            {
                logger.Error($"验证码不正确:Request:{JsonConvert.SerializeObject(request)}");
                throw new CustomException("验证码不正确");
            }

            //判断手机号是否存在
            UserInfoVO userInfo = new UserInfoVO();
            UserInfoDO user = await iUserRespository.GetUserInfoByMobile(request.MobileNumber);
            if (user == null)
            {
                //手机号也不存在，注册新用户
                bool isSucess = true;
                Guid userId = Guid.NewGuid();
                UserDO userInsert = new UserDO();
                userInsert.Id = userId;
                userInsert.MobileNumber = request.MobileNumber;
                userInsert.NickName = $"{request.MobileNumber.Substring(7)}";
                userInsert.CreateBy = "system";
                userInsert.CreateTime = DateTime.Now;

                GenReferrerInfo(request.SceneId,request.ShareUserId, userInsert);

                isSucess = await iUserRespository.CreateUser(userInsert);
                if (!isSucess)
                {
                    logger.Error($"RG注册失败:Request:{JsonConvert.SerializeObject(request)}");
                    throw new CustomException("登录失败");
                }

                userInfo.UserId = userId.ToString();
                userInfo.MobileNumber = userInsert.MobileNumber;
                userInfo.Channel = userInsert.Channel;
                userInfo.ReferrerUserId = userInsert.ReferrerUserId;

                NewRegisterUserFunc(userInfo);
            }
            else
            {
                userInfo = mapper.Map<UserInfoVO>(user);
            }

            //判断是否锁定用户
            if (!userInfo.State.Equals(0))
            {
                logger.Error($"账号被锁定无法登陆:Request:{JsonConvert.SerializeObject(request)}");
                log.State = LoginState.Failure;
                log.Comment = "账号被锁定";
                log.LoginName = userInfo.UserId.ToString();
                await iLoginLogRepository.CreateLoginLogAsync(log);
                throw new CustomException("账号被锁定");
            }

            //返回用户信息
            LoginResponse result = new LoginResponse();
            var tokenInfo = iLoginAuthService.GetTokenInfo(userInfo, refreshUri);
            if (string.IsNullOrEmpty(userInfo.NickName))
            {
                userInfo.NickName = userInfo.MobileNumber ?? string.Empty;
            }

            result.TokenInfo = tokenInfo;
            result.UserInfo = userInfo;
            logger.Info($"RgLogin登录成功:user:{JsonConvert.SerializeObject(result)}");
            log.State = LoginState.Success;
            log.Comment = "手机号登录成功";
            log.LoginName = userInfo.UserId.ToString();
            await iLoginLogRepository.CreateLoginLogAsync(log);

            return result;
        }

        public async Task SendLoginVerificationCodeSms(VerificationCodeSmsRequest request)
        {
            string signName = configuration["AliSms:SignName"];
            //一分钟内只能发送一次请求
            if (await redisClient.Redis.KeyExistsAsync(redisKey + ":VerCodeSend:" + request.MobilePhone))
            {
                throw new CustomException("操作太频繁");
            }

            //生成验证码
            string code = ValidateCodeHelper.CreateValidateCode(4);
            //发短信
            //SendSms(request.MobilePhone, code);
            var sms = new SmsParameter
            {
                PhoneNumbers = request.MobilePhone,
                SignName = configuration["AliSms:SignName"],
                TemplateCode = configuration["AliSms:LoginCode"],
                TemplateParam = JsonConvert.SerializeObject(new { code = code })
            };

            //logger.Info($"短信:signName:{signName},sms:{JsonConvert.SerializeObject(sms)}");
            var sendSmsResult = smsClient.SendSms(sms);
            var isSendSuccess = sendSmsResult.Code.Equals("10000");

            //验证码放到缓存中
            await redisClient.Redis.StringSetAsync(redisKey + ":VerCodeSend:" + request.MobilePhone, code, TimeSpan.FromSeconds(55));
            await redisClient.Redis.StringSetAsync(redisKey + ":VerCodeValite:" + request.MobilePhone, code, TimeSpan.FromSeconds(295));

        }

        public void SendSms(string phone, string code)
        {
            IClientProfile profile = DefaultProfile.GetProfile("", "", "");
            DefaultAcsClient client = new DefaultAcsClient(profile);
            CommonRequest request = new CommonRequest();
            request.Method = MethodType.POST;
            request.Domain = "dysmsapi.aliyuncs.com";
            request.Version = "2017-05-25";
            request.Action = "SendSms";
            request.AddQueryParameters("PhoneNumbers", phone);
            request.AddQueryParameters("SignName", "AERP");
            request.AddQueryParameters("TemplateCode", "SMS_213970043");
            request.AddQueryParameters("TemplateParam", "{\"code\":\"" + code + "\"}");
            try
            {
                logger.Info($"发送短信开始:phone:{phone},signname:{"总部"},小程序登录验证码{code}");
                CommonResponse response = client.GetCommonResponse(request);
                logger.Info($"发送短信结束:phone:{phone},signname:{"总部"},小程序登录验证码{code},result:{System.Text.Encoding.Default.GetString(response.HttpResponse.Content)}");
            }
            catch (ServerException e)
            {
                logger.Error($"发送短信:phone:{phone},验证码{code}异常", e);
            }
            catch (ClientException e)
            {
                logger.Error($"发送短信:phone:{phone},验证码{code}异常", e);
            }
        }

        public string GetIp()
        {
            return _context.HttpContext.GetClientUserIp();
        }

        // ---------------------------------- 私有方法 --------------------------------------
        private void GenReferrerInfo(string sceneId,string shareUserId, UserDO user)
        {
            if(!shareUserId.IsNullOrWhiteSpace())
            {
                user.Channel = Convert.ToInt32(ChannelType.Consumer);
                user.ReferrerType = Convert.ToInt32(ReferrerType.UserShare);
                user.ReferrerUserId = shareUserId;
                return;
            }

            if (sceneId.IsNullOrWhiteSpace())
            {
                user.Channel = Convert.ToInt32(ChannelType.Consumer);
                user.ReferrerType = Convert.ToInt32(ReferrerType.SelfSearch);
                return;
            }

            var objTask = weChatClient.GetWxacodeScene(new GetWxacodeSceneRequest { SceneId = sceneId });
            Task.WaitAll(objTask);
            object obj = objTask.Result;
            var entity = JsonConvert.DeserializeObject<GetWxacodeSceneResDTO>(JsonConvert.SerializeObject(obj));

            user.Channel = (entity?.ShopId ?? 0) > 0 ? Convert.ToInt32(ChannelType.Employee) : Convert.ToInt32(ChannelType.Consumer);
            user.ReferrerType = entity?.PromoteContentType ?? 0;
            user.ReferrerShopId = entity?.ShopId ?? 0;
            user.ReferrerUserId = entity?.EmployeeId ?? "";
        }

        private void NewRegisterUserFunc(UserInfoVO userInfo)
        {
            Task.Run(() =>
            {
                if (!int.TryParse(configuration["BusinessLogical:NewRegisterUserPointValue"], out var pointValue))
                {
                    pointValue = 100;
                }

                userClient.OperateUserPoint(new OperateUserPointRequest
                {
                    UserId = userInfo.UserId,
                    OperateType = UserPointOperateTypeEnum.Register,
                    PointValue = pointValue,
                    ReferrerNo = userInfo.MobileNumber,
                    SubmitBy = userInfo.UserId,
                    Remark = $"新用户注册：{userInfo.MobileNumber}, {DateTime.Now:yyyy-MM-dd HH:mm:ss}"
                });

                copClient.AddUserCouponForNewRegisterUser(new AddUserCouponReqForNewUserDTO
                {
                    UserId = userInfo.UserId,
                    UserIp = "MiniAPP"
                });

                if (userInfo.Channel == 2 && !userInfo.ReferrerUserId.IsNullOrWhiteSpace())  //C端
                {
                    if (!int.TryParse(configuration["BusinessLogical:ReferrerUserPointValue"], out var referrerPointValue))
                    {
                        referrerPointValue = 10;
                    }

                    userClient.OperateUserPoint(new OperateUserPointRequest
                    {
                        UserId = userInfo.ReferrerUserId,
                        OperateType = UserPointOperateTypeEnum.ReferrerNewUser,
                        PointValue = referrerPointValue,
                        ReferrerNo = userInfo.MobileNumber,
                        SubmitBy = userInfo.UserId,
                        Remark = $"推荐新用户：{userInfo.MobileNumber}, {DateTime.Now:yyyy-MM-dd HH:mm:ss}"
                    });

                }
            });
        }


        /// <summary>
        /// 根据微信小程序平台提供的解密算法解密数据
        /// </summary>
        /// <param name="encryptedData">加密数据</param>
        /// <param name="iv">初始向量</param>
        /// <param name="sessionKey">从服务端获取的SessionKey</param>
        /// <returns></returns>
        private string Decrypt(string encryptedData, string iv, string sessionKey)
        {
            try
            {
                //创建解密器生成工具实例
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                //设置解密器参数
                aes.Mode = CipherMode.CBC;
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.PKCS7;
                //格式化待处理字符串
                byte[] byte_encryptedData = Convert.FromBase64String(encryptedData);
                byte[] byte_iv = Convert.FromBase64String(iv);
                byte[] byte_sessionKey = Convert.FromBase64String(sessionKey);
                aes.IV = byte_iv;
                aes.Key = byte_sessionKey;
                //根据设置好的数据生成解密器实例
                ICryptoTransform transform = aes.CreateDecryptor();
                //解密
                byte[] final = transform.TransformFinalBlock(byte_encryptedData, 0, byte_encryptedData.Length);
                //生成结果
                string result = Encoding.UTF8.GetString(final);
                return result;
            }
            catch (Exception ex)
            {
                logger.Error($"解密手机号失败:encryptedData:{encryptedData},iv:{iv},sessionKey:{sessionKey}", ex);
                return "";
            }
        }

    }
}
