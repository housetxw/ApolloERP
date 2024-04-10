using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Profile;
using AutoMapper;
using Castle.DynamicProxy;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Message.Sms;
using ApolloErp.Web.WebApi;
using Ae.B.Login.Api.Common.Cache.LocalCache;
using Ae.B.Login.Api.Common.Extension;
using Ae.B.Login.Api.Core.Interfaces;
using Ae.B.Login.Api.Core.Model;
using Ae.B.Login.Api.Core.Request;
using Ae.B.Login.Api.Core.Response;
using Ae.B.Login.Api.Dal.Model;
using Ae.B.Login.Api.Dal.Repositorys.IDAL;
using Ae.B.Login.Api.Imp.OperationLog;

namespace Ae.B.Login.Api.Imp.Services
{
    public class AccountService : IAccountService
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly ApolloErpLogger<AccountService> logger;
        private readonly IMapper mapper;
        private readonly SmsClient smsClient;

        private readonly IAccountRepository acctRepo;

        private readonly ProxyGenerator proxyGenerator;

        public AccountService(ApolloErpLogger<AccountService> logger, IMapper mapper, SmsClient smsClient,
            IAccountRepository acctRepo)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.smsClient = smsClient;

            this.acctRepo = acctRepo;

            proxyGenerator = new ProxyGenerator();
        }

        // ---------------------------------- 登录操作相关方法 --------------------------------------
        public string GetSMSCode(ApiRequest<LoginPhoneVO> req)
        {
            //生成验证码
            var code = ValidatationCodeHelper.CreateValidateCode(4);
            var res = SMSAction(req.Data.Name, code, req.Data.SMSType, req.Channel);
            if (!res)
            {
                SMSAction(req.Data.Name, code, req.Data.SMSType, req.Channel);
            }
            return code;
        }

        public async Task<AccountKeyInfoWithPwdEntityResVO> GetAccountByNameAsync(AccountEntityReqByNameVO req)
        {
            return await acctRepo.GetAccountByNameAsync(req);
        }

        public async Task<bool> UpdateAccountStateOrErrorCountByIdAsync(AccountStateOrErrorCountReqVO req)
        {
            return await acctRepo.UpdateAccountStateOrErrorCountByIdAsync(req);
        }

        public async Task<bool> ResetAccountStateAndErrorCountByIdOrNameAsync(AccountStateOrErrorCountReqVO req)
        {
            return await acctRepo.ResetAccountStateAndErrorCountByIdOrNameAsync(req);
        }

        public async Task<bool> UpdateAccountPasswordByNameAsync(UpdateAccountPwdVO req)
        {
            var acctRepoInterceptor = proxyGenerator.CreateInterfaceProxyWithTarget(acctRepo,
                new AccountLogInterceptor(logger, new LogOperationReqDTO
                {
                    LogId = req.Id,
                    LogType = LogType.U.GetEnumDescription(),
                    BizType = BizType.Account.GetEnumDescription(),
                    ReqParam = JsonConvert.SerializeObject(req),
                    Operator = req.Id
                }));

            return await acctRepoInterceptor.UpdateAccountPasswordByNameAsync(req);
        }

        // ---------------------------------- 登录操作相关私有方法 --------------------------------------
        private bool SMSAction(string phone, string code, SMSType smsType, string channel)
        {
            var res = false;
            var tmplCode = "";
            switch (smsType)
            {
                case SMSType.Login:
                    tmplCode = "SMS_213970043";
                    break;
                case SMSType.ChangePassword:
                    tmplCode = "SMS_213970040";
                    break;
                case SMSType.AppQuickRegister:
                    tmplCode = "SMS_213970041";
                    break;
                case SMSType.QuickCompanyRegister:
                    tmplCode = "SMS_213970044";
                    break;
            }

            var chlType = "侬田田";//门店APP签名
            if (channel.Contains("b-pc") || channel.Contains("pc")) chlType = "侬田田";//Web登录短信签名

            var reqSms = new SmsParameter
            {
                PhoneNumbers = phone,
                SignName = chlType,
                TemplateCode = tmplCode,
                TemplateParam = "{\"code\":\"" + code + "\"}"
            };

            SmsResult resSms = new SmsResult();
            var logMsg = $"Account Name: {phone}, signName: {chlType}, Channel {channel},";

            try
            {
                resSms = smsClient.SendSms(reqSms);
                if (resSms.Code.Equals("10000")) res = true;
            }
            catch (Exception e)
            {
                logger.Error($"发送短信异常: {logMsg} 登录验证码{code}异常", e);
            }
            finally
            {
                logger.Info($"发送短信信息: {logMsg} {smsType.GetEnumDescription()}: {code}, resSms: {JsonConvert.SerializeObject(resSms)}");
            }
            return res;
        }


    }
}
