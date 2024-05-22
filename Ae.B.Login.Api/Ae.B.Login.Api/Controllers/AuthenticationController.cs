using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Microsoft.AspNetCore.Mvc;
using Ae.B.Login.Api.Filters;
using AutoMapper;
using ApolloErp.Log;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ApolloErp.Login.Auth;
using ApolloErp.Redis;
using Ae.B.Login.Api.Client.Request.ShopManage.Employee;
using Ae.B.Login.Api.Client.Response.ShopManage.Employee;
using Ae.B.Login.Api.Common.Constant;
using Ae.B.Login.Api.Common.Exceptions;
using Ae.B.Login.Api.Common.Extension;
using Ae.B.Login.Api.Common.Log;
using Ae.B.Login.Api.Common.Security;
using Ae.B.Login.Api.Controllers.Base;
using Ae.B.Login.Api.Core.Enums;
using Ae.B.Login.Api.Core.Interfaces;
using Ae.B.Login.Api.Core.Interfaces.ShopManage;
using Ae.B.Login.Api.Core.Model;
using Ae.B.Login.Api.Core.Request;
using Ae.B.Login.Api.Core.Response;
using StackExchange.Redis;

namespace Ae.B.Login.Api.Controllers
{
    /// <summary>
    /// 登录
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(AuthenticationController))]
    public class AuthenticationController : BaseController
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly ApolloErpLogger<AuthenticationController> logger;
        private readonly IMapper mapper;
        private readonly string className;

        private readonly IIdentityService identitySvc;

        //private readonly RedisClient redisClient;
        private readonly RedisClient redisClient;

        private readonly ITokenService tokenSvc;
        private readonly IAccountService acctSvc;
        private readonly IEmployeeService empSvc;
        private readonly ILoginLogService loginLogSvc;

        private IHttpContextAccessor accessor;

        private LoginInfoDTO loginLogInfo;

        public AuthenticationController(ApolloErpLogger<AuthenticationController> logger, IMapper mapper,
            IIdentityService identitySvc,
            RedisClient redisClient,
            ITokenService tokenSvc,
            IAccountService acctSvc,
            IEmployeeService empSvc,
            ILoginLogService loginLogSvc,
            IHttpContextAccessor accessor)
        {
            this.logger = logger;
            this.mapper = mapper;
            className = this.GetType().Name;

            this.identitySvc = identitySvc;
            this.redisClient = redisClient;

            this.tokenSvc = tokenSvc;
            this.acctSvc = acctSvc;
            this.empSvc = empSvc;

            this.loginLogSvc = loginLogSvc;
            this.accessor = accessor;

            loginLogInfo = new LoginInfoDTO
            {
                IpAddress = GetClientIpAddress(accessor),
                LoginLocation = "",
                Browser = GetClientBrowser(accessor),
                OS = GetClientOSPlatform(accessor)
                //MachineName = GetClientMachineName(accessor)
            };
        }

        // ---------------------------------- 登录操作相关方法 --------------------------------------
        /// <summary>
        /// 账号密码，登录接口
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]  
        [AllowAnonymous]
        public async Task<ApiResult<LoginInfoResVO>> LoginAsync([FromBody] ApiRequest<LoginVO> req)
        {
            var temp = new Uri(Request.GetUri(), Url.Action("RefreshToken")).ToString();
            var chkMsg = BasicCheckLoginPwdReq(req);
            if (chkMsg.IsNotNullOrWhiteSpace()) return Result.Failed<LoginInfoResVO>(ResultCode.INVALID_FORMAT, chkMsg);

            loginLogInfo.Channel = req.Channel.Split(CommonConstant.SeparatorLineThrough).FirstOrDefault();

            var msgAcctName = req.Data.Name.HidePhoneSensitiveInfo();
            var noKey = $"{CommonConstant.RedisLoginFolder}{loginLogInfo.Channel}:{CommonConstant.RedisNotFoundAccount}:" +
                        $"{req.Data.Name}|{loginLogInfo.IpAddress}";
            var notName = redisClient.Redis.StringGet(noKey).ToString();
            if (notName.IsNotNullOrWhiteSpace())
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo($"{CommonConstant.ArgumentValidateFailed}：账号不存在\n"));
                return Result.Failed<LoginInfoResVO>(ResultCode.INVALID_FORMAT, "账号格式不正确，或是账号不存在");
            }

            //Check Account State
            var acctChkObj = await CheckAccountByNameAsync(mapper.Map<AccountEntityReqByNameVO>(req.Data));
            if (acctChkObj.Code != ResultCode.Success)
            {
                CreateLoginLog(new SysLoginLogVO
                {
                    LoginName = msgAcctName,
                    State = LoginState.Failure,
                    LoginNameType = LoginNameType.AccountId,
                    LoginType = LoginType.Password,
                    Comment = acctChkObj.Message
                });

                return Result.Failed<LoginInfoResVO>(acctChkObj.Code, acctChkObj.Message);
            }

            //Validation Password
            if (req.Channel.NotEqualsIgnoreCase("b-pc"))
            {
                var dePwd = AESHelper.DecodeAES(req.Data.Password);
                if (dePwd.IsNullOrWhiteSpace())
                {
                    return Result.Failed<LoginInfoResVO>(ResultCode.USERNAME_OR_PASSWORD_ERROR, "密码不正确，或是密码格式不正确");
                }

                req.Data.Password = dePwd;
            }   

            var pwdChk = ValidationPassword(req.Data.Password.Trim(), acctChkObj.Data);
            if (pwdChk.Code != ResultCode.Success)
            {
                CreateLoginLog(new SysLoginLogVO
                {
                    LoginName = acctChkObj.Data.Id,
                    State = LoginState.Failure,
                    LoginNameType = LoginNameType.AccountId,
                    LoginType = LoginType.Password,
                    Comment = pwdChk.Message
                });

                return Result.Failed<LoginInfoResVO>(pwdChk.Code, pwdChk.Message);
            }

            //Fetch Organization List
            LoginInfoResVO resp;
            var (orgTotal, loginRes) = GetLoginInfoForEmployeeLogin(acctChkObj.Data.Id, req.Data.EmployeeType);
            if (orgTotal > 0)
            {
                resp = loginRes;
            }
            else
            {
                CreateLoginLog(new SysLoginLogVO
                {
                    LoginName = acctChkObj.Data.Id,
                    State = LoginState.Success,
                    LoginNameType = LoginNameType.AccountId,
                    LoginType = LoginType.Password,
                    Comment = $"{msgAcctName}的账号存在，无对应员工或所属单位"
                });

                return Result.Failed<LoginInfoResVO>(ResultCode.INSUFFICIENT_USER_PERMISSIONS, $"账号{msgAcctName}存在，无对应员工或所属单位，请联系管理员");
            }

            var resBool = acctChkObj.Code == ResultCode.Success && pwdChk.Code == ResultCode.Success;
            if (!resBool)
            {
                //TODO: Record Log
                Result.Failed<LoginInfoResVO>(ResultCode.Exception, "请刷新页面，重新登录");
            }

            ResetAccountStateAndErrorCountByIdOrName(acctChkObj.Data.Id ?? req.Data.Name, req.Data.Name, JsonConvert.SerializeObject(req));

            CreateLoginLog(new SysLoginLogVO
            {
                LoginName = acctChkObj.Data.Id,
                State = LoginState.Success,
                LoginNameType = LoginNameType.AccountId,
                LoginType = LoginType.Password,
                Comment = CommonConstant.LoginSuccess
            });

            return Result.Success(resp, CommonConstant.LoginSuccess);
        }

        /// <summary>
        /// 登录时，根据AuthCode，获取所有的所属单位分页列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns>返回accountId</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiPagedResult<OrganizationVO>> GetOrgPageListByAuthCodeAsync([FromBody] ApiRequest<OrganizationPageListReqVO> req)
        {
            if (null == req?.Data || req.Data.AuthCode.IsNullOrWhiteSpace())
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return Result.Failed<OrganizationVO>(ResultCode.ArgumentError, "输入的参数不正确", null, 0);
            }

            if (req.Channel.IsNullOrWhiteSpace())
            {
                return Result.Failed<OrganizationVO>(ResultCode.INVALID_FORMAT, "设备渠道格式不正确", null, 0);
            }

            loginLogInfo.Channel = req.Channel.Split(CommonConstant.SeparatorLineThrough).FirstOrDefault();

            var acctId = new RSAHelper().Decrypt(req.Data.AuthCode?.Trim());
            if (acctId.IsNullOrWhiteSpace() || !acctId.CheckIsGuidFormat())
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo($"{CommonConstant.ArgumentValidateFailed}: AuthCode为空 或是 解析出AuthCode不是GUID格式\n"));

                return Result.Failed<OrganizationVO>(ResultCode.ArgumentError, "认证码格式不正确，请退出重新登录", null, 0);
            }

            var acKey = $"{CommonConstant.RedisLoginFolder}{loginLogInfo.Channel}:{CommonConstant.RedisLoginAuthCode}" +
                        $"{acctId}|{loginLogInfo.IpAddress}";
            var authCode = redisClient.Redis.StringGet(acKey).ToString();
            if (req.Data.AuthCode.Trim().NotEqualsIgnoreCase(authCode))
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo($"{CommonConstant.ArgumentValidateFailed}: AuthCode相等性验证失败\n"));
                return Result.Failed<OrganizationVO>(ResultCode.ArgumentError, "认证码错误，请退出重新登录", null, 0);
            }

            //logger.Info($"API: {className}.GetOrgPageListByAuthCodeAsync 请求参数：{JsonConvert.SerializeObject(req)}");

            var reqDto = mapper.Map<EmployeePageForLoginListReqDTO>(req.Data);
            reqDto.AccountId = acctId;

            var resDto = await empSvc.GetEmpAndOrgPageForLoginByAccountIdAsync(reqDto);

            //logger.Info($"API: {className}.GetOrgPageListByAuthCodeAsync 返回值：{JsonConvert.SerializeObject(resDto)}");

            var total = resDto.Data.TotalItems;
            var items = resDto.Data.Items;

            //if (total > 0 && items.Count <= 0)
            //{
            //    return Result.Failed<OrganizationVO>(ResultCode.OPERATION_FAILED, "请刷新页面，重新获取", null, 0);
            //}

            return Result.Success(items, total);
        }

        /// <summary>
        /// 登录时，当账号对应多个员工时，根据AuthCode等关键信息，获取Token信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ApiResult<TokenInfo> GetTokenInfoByAuthCode([FromBody] ApiRequest<EmployeeInfoByAuthCodeReq> req)
        {
            if (null == req?.Data || !req.Data.EmployeeId.CheckIsGuidFormat() || req.Data.OrganizationId <= 0)
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return Result.Failed<TokenInfo>(ResultCode.ArgumentError, "输入的参数不正确");
            }

            var acctId = new RSAHelper().Decrypt(req.Data.AuthCode?.Trim());
            if (acctId.IsNullOrWhiteSpace() || !acctId.CheckIsGuidFormat())
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo($"{CommonConstant.ArgumentValidateFailed} ：AuthCode校验失败\n"));
                return Result.Failed<TokenInfo>(ResultCode.ArgumentError, "认证码输入错误，请退出重新登录");
            }

            //TODO: AuthCode需要存入Redis，来确保AuthCode一定时间内的安全性。 后期需要考虑验证，empId，orgId和orgType是否存在
            var reqVo = mapper.Map<EmployeeInfoVO>(req.Data);
            TokenInfo tokenInfo = tokenSvc.GenerateToken(reqVo, new Uri(Request.GetUri(), Url.Action("RefreshToken")).ToString());
            return Result.Success(tokenInfo, CommonConstant.OperateSuccess);
        }

        /// <summary>
        /// 登录后，根据Token，获取所有的所属单位分页列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns>返回accountId</returns>
        [HttpPost]
        public async Task<ApiPagedResult<OrganizationVO>> GetOrgPageListByTokenAsync([FromBody] ApiRequest<OrganizationPageListByTokenReqVO> req)
        {
            if (null == req?.Data || req.Data.Token.IsNullOrWhiteSpace() || !req.Data.EmployeeId.CheckIsGuidFormat())
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return Result.Failed<OrganizationVO>(ResultCode.ArgumentError, "输入的参数不正确", null, 0);
            }

            if (!req.Data.EmployeeId.CheckIsGuidFormat())
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return Result.Failed<OrganizationVO>(ResultCode.ArgumentError, "请输入有效的EmployeeId", null, 0);
            }

            //logger.Info($"API: {className}.GetOrgPageListByTokenAsync/EmpIdAsync 请求参数：{JsonConvert.SerializeObject(req)}");

            //check the access token valid or not
            if (!tokenSvc.ValidateToken(req.Data.Token.Trim()))
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo($"{CommonConstant.ArgumentValidateFailed}: AccessToken失效"));
                throw new UnauthorizedException("认证失败，请退出重新登录");
                //return Result.Failed<OrganizationVO>(ResultCode.INVALID_ACCESS_TOKEN, "请退出重新登录", null, 0);
            }

            var resDto = GetEmpAndOrgPageForRangeLogin(req.Data);

            //logger.Info($"API: {className}.GetOrgPageListByTokenAsync/EmpIdAsync 返回值：{JsonConvert.SerializeObject(resDto)}");

            var items = resDto.Data.Items;
            var total = resDto.Data.TotalItems;

            //if (total > 0 && items.Count <= 0)
            //{
            //    return Result.Failed<OrganizationVO>(ResultCode.OPERATION_FAILED, "请刷新页面，重新获取", null, 0);
            //}

            return Result.Success(items, total);
        }

        /// <summary>
        /// 登录后，当账号对应多个员工时，根据Token等关键信息，获取新的Token信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<TokenInfo> GetTokenInfoByToken([FromBody] ApiRequest<EmployeeInfoByTokenReq> req)
        {
            if (null == req?.Data || req.Data.Token.IsNullOrWhiteSpace() || !req.Data.EmployeeId.CheckIsGuidFormat() || req.Data.OrganizationId <= 0)
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return Result.Failed<TokenInfo>(ResultCode.ArgumentError, "输入的参数不正确");
            }

            if (!tokenSvc.ValidateToken(req.Data.Token.Trim()))
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                throw new UnauthorizedException("认证失败，请退出重新登录");
                //return Result.Failed<TokenInfo>(ResultCode.AUTH_FAILD, "认证失败，请退出重新登录");
            }

            var reqVo = mapper.Map<EmployeeInfoVO>(req.Data);
            TokenInfo tokenInfo = tokenSvc.GenerateToken(reqVo, new Uri(Request.GetUri(), Url.Action("RefreshToken")).ToString());
            return Result.Success(tokenInfo, CommonConstant.OperateSuccess);
        }

        /// <summary>
        /// 根据RefreshToken获取Token信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ApiResult<TokenInfo> RefreshToken([FromBody] ApiRequest<RefreshTokenReq> req)
        {
            try
            {
                TokenInfo tokenInfo = tokenSvc.RefreshToken(req.Data.RefreshToken.Trim(), new Uri(Request.GetUri(), Url.Action("RefreshToken")).ToString());
                return null == tokenInfo
                    ? Result.Failed<TokenInfo>(ResultCode.INVALID_ACCESS_TOKEN, "解析RefreshToken失败")
                    : Result.Success(tokenInfo, CommonConstant.OperateSuccess);
            }
            catch (Exception e)
            {
                logger.Error($"RefreshToken失败: \n req: {JsonConvert.SerializeObject(req)}", e);
                throw new CustomException("获取RefreshToken失败");
            }
        }

        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResult<bool>> GetSMSCodeAsync([FromBody] ApiRequest<LoginPhoneVO> req)
        {
            if (req?.Data == null || !req.Data.Name.IsTelephone() || req.Data.SMSType == SMSType.None)
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return Result.Failed<bool>(ResultCode.INVALID_FORMAT, "账号格式不正确");
            }

            if (req.Channel.IsNullOrWhiteSpace())
            {
                return Result.Failed<bool>(ResultCode.INVALID_FORMAT, "设备渠道格式不正确");
            }

            loginLogInfo.Channel = req.Channel.Split(CommonConstant.SeparatorLineThrough).FirstOrDefault();

            var smsExpKey = $"{CommonConstant.RedisLoginFolder}{loginLogInfo.Channel}:{CommonConstant.RedisLoginNormalSMS}{req.Data.SMSType.GetEnumDisplayName()}:{CommonConstant.RedisSMSChk}" +
                         $"{req.Data.Name}|{loginLogInfo.IpAddress}";
            if (redisClient.Redis.KeyExists(smsExpKey))
            {
                return Result.Failed<bool>(ResultCode.ILLEGAL_OPERATION, "验证码已发送，请一分钟后再来获取");
            }

            var smsKey = $"{CommonConstant.RedisLoginFolder}{loginLogInfo.Channel}:{CommonConstant.RedisLoginNormalSMS}{req.Data.SMSType.GetEnumDisplayName()}:" +
                         $"{req.Data.Name}|{loginLogInfo.IpAddress}";

            string redisFlag;
            if (req.Data.SMSType == SMSType.AppQuickRegister || req.Data.SMSType == SMSType.QuickCompanyRegister)
            {
                redisFlag = req.Data.Name;
            }
            else
            {
                //Check account
                var chkAcctRes = await CheckAccountByNameAsync(new AccountEntityReqByNameVO {Name = req.Data.Name});
                if (chkAcctRes.Code != ResultCode.Success)
                {
                    return Result.Failed<bool>(chkAcctRes.Code, chkAcctRes.Message);
                }

                var acctObj = chkAcctRes.Data;
                var name = acctObj.Name;
                redisFlag = acctObj.Id;

                if (name.IsNullOrWhiteSpace() || name.NotEqualsIgnoreCase(req.Data.Name))
                {
                    logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                    return Result.Failed<bool>(ResultCode.ArgumentError, "请刷新页面，重新输入账号");
                }
            }

            var code = acctSvc.GetSMSCode(req);
            redisClient.Redis.StringSet(smsExpKey, $"{code}|{redisFlag}", TimeSpan.FromMinutes(CommonConstant.One));
            redisClient.Redis.StringSet(smsKey, $"{code}|{redisFlag}", TimeSpan.FromMinutes(CommonConstant.Five));
            return Result.Success(true, CommonConstant.OperateSuccess);
        }

        /// <summary>
        /// 通过短信验证码登录
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResult<LoginInfoResVO>> LoginWithSMSAsync([FromBody] ApiRequest<LoginMessageVO> req)
        {
            var chkMsg = BasicCheckLoginOrUpdatePwdSMSReq(req, SMSType.Login);
            if (chkMsg.IsNotNullOrWhiteSpace())
            {
                return Result.Failed<LoginInfoResVO>(ResultCode.INVALID_FORMAT, chkMsg);
            }

            loginLogInfo.Channel = req.Channel.Split(CommonConstant.SeparatorLineThrough).FirstOrDefault();

            var msgAcctName = req.Data.Name.HidePhoneSensitiveInfo();

            //需要DoubleCheck，Redis中是否有此手机号的记录，防止DDoS
            var smsKey = $"{CommonConstant.RedisLoginFolder}{loginLogInfo.Channel}:{CommonConstant.RedisLoginNormalSMS}{req.Data.SMSType.GetEnumDisplayName()}:" +
                         $"{req.Data.Name}|{loginLogInfo.IpAddress}";
            var list = redisClient.Redis.StringGet(smsKey).ToString()?.Split("|");
            var code = list?.FirstOrDefault();
            var acctId = list?.LastOrDefault();
            if (code.IsNullOrWhiteSpace() || acctId.IsNullOrWhiteSpace())
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return Result.Failed<LoginInfoResVO>(ResultCode.ArgumentError, "验证码失效，请重新获取验证码");
            }

            if (req.Data.VerificationCode.NotEqualsIgnoreCase(code))
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return Result.Failed<LoginInfoResVO>(ResultCode.ArgumentError, "验证码输入错误，请输入正确的验证码");
            }

            //Fetch Organization List
            LoginInfoResVO resp;
            var (orgTotal, loginRes) = GetLoginInfoForEmployeeLogin(acctId, req.Data.EmployeeType);
            if (orgTotal > 0)
            {
                resp = loginRes;
            }
            else
            {
                CreateLoginLog(new SysLoginLogVO
                {
                    LoginName = msgAcctName,
                    State = LoginState.Success,
                    LoginNameType = LoginNameType.AccountId,
                    LoginType = LoginType.Password,
                    Comment = $"{msgAcctName}的账号存在，无对应员工或所属单位"
                });

                return Result.Failed<LoginInfoResVO>(ResultCode.INSUFFICIENT_USER_PERMISSIONS, $"账号{msgAcctName}存在，无对应员工或所属单位，请联系管理员");
            }

            var key = $"{CommonConstant.RedisLoginFolder}{loginLogInfo.Channel}:{CommonConstant.RedisLoginPwdErr}" +
                      $"{req.Data.Name}|{loginLogInfo.IpAddress}";
            redisClient.Redis.KeyDelete(key);

            ResetAccountStateAndErrorCountByIdOrName(acctId ?? req.Data.Name, req.Data.Name, JsonConvert.SerializeObject(req));

            CreateLoginLog(new SysLoginLogVO
            {
                LoginName = acctId,
                State = LoginState.Success,
                LoginNameType = LoginNameType.AccountId,
                LoginType = LoginType.SMSCode,
                Comment = CommonConstant.LoginSuccess
            });

            return Result.Success(resp, CommonConstant.LoginSuccess);
        }

        /// <summary>
        /// 通过账号和短信验证码，修改账号密码
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResult<LoginInfoResVO>> UpdatePasswordBySMSAsync([FromBody] ApiRequest<UpdatePasswordVO> req)
        {
            var chkMsg = BasicCheckLoginOrUpdatePwdSMSReq(new ApiRequest<LoginMessageVO> { Data = mapper.Map<UpdatePasswordVO>(req.Data) }, SMSType.ChangePassword);
            if (chkMsg.IsNotNullOrWhiteSpace())
            {
                return Result.Failed<LoginInfoResVO>(ResultCode.INVALID_FORMAT, chkMsg);
            }

            loginLogInfo.Channel = req.Channel.Split(CommonConstant.SeparatorLineThrough).FirstOrDefault();

            //需要DoubleCheck，Redis中是否有此手机号的记录，防止DDoS
            var data = req.Data;
            var smsKey = $"{CommonConstant.RedisLoginFolder}{loginLogInfo.Channel}:{CommonConstant.RedisLoginNormalSMS}{req.Data.SMSType.GetEnumDisplayName()}:" +
                         $"{data.Name}|{loginLogInfo.IpAddress}";
            var list = redisClient.Redis.StringGet(smsKey).ToString()?.Split("|");
            var code = list?.FirstOrDefault();
            var acctId = list?.LastOrDefault();
            if (code.IsNullOrWhiteSpace())
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return Result.Failed<LoginInfoResVO>(ResultCode.ArgumentError, "验证码失效，请重新获取验证码");
            }

            if (data.VerificationCode.NotEqualsIgnoreCase(code))
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return Result.Failed<LoginInfoResVO>(ResultCode.ArgumentError, "验证码输入错误，请输入正确的验证码");
            }

            //Update Password
            var genPwd = EncryptPassword.CreatePasswordHash(data.Password);
            var reqVo = new UpdateAccountPwdVO
            {
                Id = acctId ?? data.Name,
                Name = data.Name,
                Password = genPwd["Password"].ToString(),
                PasswordIteration = Convert.ToInt32(genPwd["PasswordIteration"]),
                PasswordSalt = genPwd["PasswordSalt"].ToString(),
            };
            var ud = await acctSvc.UpdateAccountPasswordByNameAsync(reqVo);
            if (!ud)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBUpdateFailed));
                await acctSvc.UpdateAccountPasswordByNameAsync(reqVo);
            }

            //Fetch Organization List
            var msgAcctName = req.Data.Name.HidePhoneSensitiveInfo();
            LoginInfoResVO resp;
            var (orgTotal, loginRes) = GetLoginInfoForEmployeeLogin(acctId, req.Data.EmployeeType);
            if (orgTotal > 0)
            {
                resp = loginRes;
            }
            else
            {
                CreateLoginLog(new SysLoginLogVO
                {
                    LoginName = acctId,
                    State = LoginState.Success,
                    LoginNameType = LoginNameType.AccountId,
                    LoginType = LoginType.Password,
                    Comment = $"{msgAcctName}的账号存在，无对应员工或所属单位"
                });

                return Result.Failed<LoginInfoResVO>(ResultCode.INSUFFICIENT_USER_PERMISSIONS, $"账号{msgAcctName}存在，无对应员工或所属单位，请联系管理员");
            }

            ResetAccountStateAndErrorCountByIdOrName(acctId ?? req.Data.Name, req.Data.Name, JsonConvert.SerializeObject(req));

            CreateLoginLog(new SysLoginLogVO
            {
                LoginName = acctId,
                State = LoginState.Success,
                LoginNameType = LoginNameType.AccountId,
                LoginType = LoginType.Password,
                Comment = CommonConstant.LoginSuccess
            });

            //TODO:修改密码，记录操作日志
            return Result.Success(resp, CommonConstant.LoginSuccess);
        }

        // ---------------------------------- 登录操作相关私有方法 --------------------------------------
        /// <summary>
        /// Check Account State
        /// </summary>
        /// <param name="req"></param>
        /// <returns>返回accountId</returns>
        private async Task<ApiResult<AccountKeyInfoApiResVO>> CheckAccountByNameAsync(AccountEntityReqByNameVO req)
        {
            var noKey = $"{CommonConstant.RedisLoginFolder}{loginLogInfo.Channel}:{CommonConstant.RedisNotFoundAccount}:" +
                        $"{req.Name}|{loginLogInfo.IpAddress}";
            var notName = redisClient.Redis.StringGet(noKey).ToString();
            if (notName.IsNotNullOrWhiteSpace())
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo($"{CommonConstant.ArgumentValidateFailed}：账号不存在\n"));

                return Result.Failed<AccountKeyInfoApiResVO>(ResultCode.INVALID_FORMAT, "账号格式不正确，或是账号不存在");
            }

            var resDto = await acctSvc.GetAccountByNameAsync(req);
            var msgAcctName = req.Name.HidePhoneSensitiveInfo();
            if (null == resDto)
            {
                //账号不存在时，加入Redis，防止恶意请求
                redisClient.Redis.StringSet(noKey, req.Name, TimeSpan.FromMinutes(IntExtension.RandomIntRange(1, 5)));

                return Result.Failed<AccountKeyInfoApiResVO>(ResultCode.SUCCESS_ACOUNT_NOT_EXIST, $"账号{msgAcctName}不存在，或是格式不正确");
            }

            var res = mapper.Map<AccountKeyInfoApiResVO>(resDto);
            switch (resDto.State)
            {
                case AccountState.Normal:

                    //缓存账号，用于发验证码时的DoubleCheck，防止DDoS
                    //redisClient.Redis.StringSet(keyObj, $"{JsonConvert.SerializeObject(resDto)}", TimeSpan.FromMinutes(CommonConstant.Two));
                    return Result.Success(res, CommonConstant.LoginSuccess);

                case AccountState.Exception:
                    return Result.Failed<AccountKeyInfoApiResVO>(ResultCode.SUCCESS_ACOUNT_NOT_EXIST, $"账号 {msgAcctName} 异常，请联系管理员");

                case AccountState.Lock:
                    return Result.Failed<AccountKeyInfoApiResVO>(ResultCode.SUCCESS_ACOUNT_NOT_EXIST, $"账号 {msgAcctName} 已锁定，请联系管理员");

                default:
                    return Result.Success(res, CommonConstant.LoginSuccess);
            }
        }

        /// <summary>
        /// Validation Password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        private ApiResult<bool> ValidationPassword(string password, AccountKeyInfoApiResVO account)
        {
            var msg = "";
            var waitTime = CommonConstant.Three;
            var msgAcctName = account.Name.HidePhoneSensitiveInfo();
            var key = $"{CommonConstant.RedisLoginFolder}{loginLogInfo.Channel}:{CommonConstant.RedisLoginPwdErr}" +
                      $"{account.Name}|{loginLogInfo.IpAddress}";

            var wait = redisClient.Redis.StringGet(key).ToString();
            if (wait.IsNotNullOrWhiteSpace())
            {
                return Result.Failed<bool>(ResultCode.USERNAME_OR_PASSWORD_ERROR, $"账号{msgAcctName}密码已连续输错{wait}次，请{waitTime}分钟后重新登录");
            }

            if (account.ErrorCount >= CommonConstant.Ten)
            {
                //TODO:更新此账号的ErrorCount为10，并加入Redis缓存
                //var key = $"{CommonConstant.RedisLoginFolder}{LoginLogInfo.Channel}:{CommonConstant.RedisLoginNormal}{msgAcctName}|{LoginLogInfo.IpAddress}";
                //redisClient.Redis.StringSet(key, account.Name, TimeSpan.FromMinutes(CommonConstant.Ten));

                msg = $"账号{msgAcctName}密码已连续输错{CommonConstant.Ten}次，已锁定，请联系管理员";
                return Result.Failed<bool>(ResultCode.USERNAME_OR_PASSWORD_ERROR, msg);
            }

            var hashTable = new Hashtable
            {
                {"Password", account.Password},
                {"PasswordIteration", account.PasswordIteration},
                {"PasswordSalt", account.PasswordSalt}
            };
            var validPwd = EncryptPassword.ValidatePassword(password, hashTable);
            if (validPwd)
            {
                //Update Error Count
                if (account.ErrorCount > CommonConstant.Zero)
                {
                    acctSvc.UpdateAccountStateOrErrorCountByIdAsync(new AccountStateOrErrorCountReqVO
                    {
                        Id = account.Id,
                        ErrorCount = CommonConstant.Zero
                    });
                }

                redisClient.Redis.KeyDelete(key);

                return Result.Success(true, CommonConstant.LoginSuccess);
            }

            var tempCount = account.ErrorCount;
            if (account.ErrorCount < CommonConstant.Three)
            {
                msg = $"账号{msgAcctName}密码错误";
            }

            if (account.ErrorCount >= CommonConstant.Three && account.ErrorCount < CommonConstant.Four)
            {
                msg = $"账号{msgAcctName}密码已连续输错{++tempCount}次，连续输错10次，账号会被锁定！";
            }

            if (account.ErrorCount >= CommonConstant.Four && account.ErrorCount < CommonConstant.Ten)
            {
                msg = ++tempCount == CommonConstant.Ten
                    ? $"账号{msgAcctName}密码已连续输错{CommonConstant.Ten}次，已锁定，请联系管理员"
                    : $"账号{msgAcctName}密码已连续输错{tempCount}次，请{waitTime}分钟后重新登录";

                //需要存到Redis缓存中；
                redisClient.Redis.StringSet(key, $"{tempCount}", TimeSpan.FromMinutes(waitTime));
            }

            acctSvc.UpdateAccountStateOrErrorCountByIdAsync(new AccountStateOrErrorCountReqVO
            {
                Id = account.Id,
                ErrorCount = account.ErrorCount >= CommonConstant.Ten ? CommonConstant.Ten : ++account.ErrorCount
            });
            return Result.Failed<bool>(ResultCode.USERNAME_OR_PASSWORD_ERROR, msg);
        }

        /// <summary>
        /// 根据AcctId和EmployeeType获取其所属单位列表
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private (int orgTotal, LoginInfoResVO resp) GetLoginInfoForEmployeeLogin(string accountId, EmployeeType type)
        {
            var resp = new LoginInfoResVO();

            var orgPageObj = empSvc.GetEmpAndOrgPageForLoginByAccountIdAsync(
                new EmployeePageForLoginListReqDTO
                {
                    AccountId = accountId,
                    EmployeeType = type
                });

            //Get organization list
            Task.WaitAll(orgPageObj);
            List<OrganizationVO> orgList = orgPageObj.Result.Data.Items.ToList();
            var orgTotal = orgPageObj.Result.Data.TotalItems;

            if (orgList.Any())
            {
                //Gen Token, Login is Over
                if (orgTotal == 1)
                {
                    var objEmp = orgList.FirstOrDefault();
                    resp.Token = tokenSvc.GenerateToken(new EmployeeInfoVO
                    {
                        EmployeeId = objEmp?.EmployeeId,
                        EmployeeName = objEmp?.EmployeeName,
                        OrganizationId = objEmp?.OrganizationId,
                        EmployeeType = objEmp?.EmployeeType ?? EmployeeType.None
                    }, new Uri(Request.GetUri(), Url.Action("RefreshToken")).ToString());
                    resp.OrgTotal = orgTotal;
                    resp.OrgItems = orgList;
                }
                else if (orgTotal > 1)
                {
                    var authCode = new RSAHelper().Encrypt(accountId.Trim());
                    resp.AuthCode = authCode;
                    resp.OrgTotal = orgTotal;
                    resp.OrgItems = orgList;

                    //后期可以考虑把AccountId存入Redis，当再来获取OrgList或是EmpAuthority数据时，用来验证下请求的合法性；
                    var acKey = $"{CommonConstant.RedisLoginFolder}{loginLogInfo.Channel}:{CommonConstant.RedisLoginAuthCode}" +
                                $"{accountId}|{loginLogInfo.IpAddress}";
                    redisClient.Redis.StringSet(acKey, authCode, TimeSpan.FromMinutes(CommonConstant.Five));
                }
            }
            else
            {
                //TODO: 若账号无所属属单位列表，则再去判断其是否有所属单位管辖范围数据

            }

            return (orgTotal, resp);
        }
        /// <summary>
        /// GetEmpAndOrgPageForRangeLogin
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        private ApiPagedResult<OrganizationVO> GetEmpAndOrgPageForRangeLogin(OrganizationPageListByTokenReqVO req)
        {
            var reqDto = mapper.Map<EmployeePageForLoginListByTokenReqDTO>(req);
            int.TryParse(identitySvc.GetOrganizationId(), out var orgId);
            reqDto.OrganizationId = orgId;

            if (req.Platform == LoginPlatform.Shop) return GetEmpAndOrgPageForRangeLoginShop(reqDto);

            var empTask = empSvc.GetEmpAndOrgPageForLoginByEmpIdExcCurOrgIdAsync(reqDto);
            Task.WaitAll(empTask);
            return empTask.Result;
        }
        /// <summary>
        /// GetEmpAndOrgPageForRangeLoginShop
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        private ApiPagedResult<OrganizationVO> GetEmpAndOrgPageForRangeLoginShop(EmployeePageForLoginListByTokenReqDTO req)
        {
            var res = new ApiPagedResult<OrganizationVO>
            {
                Data = new ApiPagedResultData<OrganizationVO>
                {
                    Items = new List<OrganizationVO>()
                }
            };

            var trueIndex = req.PageIndex;
            var trueSize = req.PageSize;

            req.PageIndex = CommonConstant.One;
            req.PageSize = CommonConstant.TenThousand;

            var empTask = empSvc.GetEmpAndOrgPageForLoginByEmpIdExcCurOrgIdAsync(req);
            var orgRangeTask = empSvc.GetOrgRangePageForLoginByEmpIdExcCurOrgId(req);

            //Task.WaitAll(empTask, orgRangeTask);
            Task.WaitAll(empTask);
            Task.WaitAll( orgRangeTask);

            res.Data.TotalItems = empTask.Result.Data.TotalItems + orgRangeTask.Result.Data.TotalItems;

            var tmpItems = empTask.Result.Data.Items.ToList();
            tmpItems.AddRange(orgRangeTask.Result.Data.Items);

            var trueItems = tmpItems.Skip((trueIndex - 1) * trueSize).Take(trueSize);
            foreach (var item in trueItems) 
            {
                if (res.Data.Items.Where(o=>o.OrganizationId == item.OrganizationId).Count() == 0) 
                {
                    res.Data.Items.Add(item);
                }
            }

            return res;
        }
        /// <summary>
        /// BasicCheckLoginPwdReq
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        private string BasicCheckLoginPwdReq(ApiRequest<LoginVO> req)
        {
            var tipMsg = "";
            if (req?.Data == null || !req.Data.Name.IsTelephone() || req.Data.EmployeeType == EmployeeType.None)
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());

                if (req?.Data == null)
                {
                    tipMsg = "请输入正确的参数";
                }
                else if (!req.Data.Name.IsTelephone())
                {
                    tipMsg = "账号格式不正确";
                }
                //else if (req.Data.EmployeeType == EmployeeType.None && req.Data.Platform != LoginPlatform.Shop)
                else if (CheckEmployeeType(req.Data.EmployeeType, req.Data.Platform))
                {
                    tipMsg = "请输入正确的员工类型或是所属单位类型";
                }
                else if (req.Channel.IsNullOrWhiteSpace())
                {
                    tipMsg = "设备渠道格式不正确";
                }
            }

            return tipMsg;
        }
        /// <summary>
        /// BasicCheckLoginOrUpdatePwdSMSReq
        /// </summary>
        /// <param name="req"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private string BasicCheckLoginOrUpdatePwdSMSReq(ApiRequest<LoginMessageVO> req, SMSType type)
        {
            var tipMsg = "";
            if (req?.Data == null || !req.Data.Name.IsTelephone() || req.Data.SMSType == SMSType.None || req.Data.SMSType != type || req.Data.EmployeeType == EmployeeType.None)
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());

                if (req?.Data == null)
                {
                    tipMsg = "请输入正确的参数";
                }
                else if (!req.Data.Name.IsTelephone())
                {
                    tipMsg = "账号格式不正确";
                }
                else if (req.Data.SMSType == SMSType.None || req.Data.SMSType != type)
                {
                    tipMsg = "请输入正确短信验证码类型";
                }
                else if (CheckEmployeeType(req.Data.EmployeeType, req.Data.Platform))
                {
                    tipMsg = "请输入正确的员工类型或是所属单位类型";
                }
                else if (req.Channel.IsNullOrWhiteSpace())
                {
                    tipMsg = "设备渠道格式不正确";
                }
            }

            return tipMsg;
        }
        /// <summary>
        /// CheckEmployeeType
        /// </summary>
        /// <param name="empType"></param>
        /// <param name="platform"></param>
        /// <returns></returns>
        private bool CheckEmployeeType(EmployeeType empType, LoginPlatform platform)
        {
            // TODO0:增加门店APP可以公司员工登录
            if (platform == LoginPlatform.Shop || platform == LoginPlatform.App) return false;

            return empType == EmployeeType.None;
        }
        /// <summary>
        /// ResetAccountStateAndErrorCountByIdOrName
        /// </summary>
        /// <param name="acctId"></param>
        /// <param name="acctName"></param>
        /// <param name="serialReq"></param>
        private void ResetAccountStateAndErrorCountByIdOrName(string acctId, string acctName, string serialReq)
        {
            Task.Run(async () =>
            {
                var resetBool = await acctSvc.ResetAccountStateAndErrorCountByIdOrNameAsync(new AccountStateOrErrorCountReqVO
                {
                    Id = acctId ?? acctName,
                    Name = acctName,
                    ErrorCount = CommonConstant.Zero
                });
                if (!resetBool)
                {
                    logger.Error($"accountId: {acctId}, accountName: {acctName}, serialReq: {serialReq}".GenDBExceptionInfo(CommonConstant.DBUpdateFailed));

                    await acctSvc.ResetAccountStateAndErrorCountByIdOrNameAsync(new AccountStateOrErrorCountReqVO
                    {
                        Id = acctId ?? acctName,
                        Name = acctName,
                        ErrorCount = CommonConstant.Zero
                    });
                }
            });
        }
        /// <summary>
        /// CreateLoginLog
        /// </summary>
        /// <param name="req"></param>
        private void CreateLoginLog(SysLoginLogVO req)
        {
            Task.Run(() =>
            {
                req.IpAddress = loginLogInfo.IpAddress;
                req.LoginLocation = loginLogInfo.LoginLocation;
                req.Browser = loginLogInfo.Browser;
                req.OS = loginLogInfo.OS;
                loginLogSvc.CreateLoginLogAsync(req);
            });
        }

    }
}