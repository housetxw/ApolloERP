using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Castle.DynamicProxy;
using Newtonsoft.Json;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.AccountAuthority.Service.Common.Constant;
using Ae.AccountAuthority.Service.Common.Extension;
using Ae.AccountAuthority.Service.Common.Security;
using Ae.AccountAuthority.Service.Core.Interfaces;
using Ae.AccountAuthority.Service.Core.Model;
using Ae.AccountAuthority.Service.Core.Request;
using Ae.AccountAuthority.Service.Core.Response;
using Ae.AccountAuthority.Service.Dal.Model;
using Ae.AccountAuthority.Service.Dal.Repositorys.IDAL;
using Ae.AccountAuthority.Service.Imp.Helpers;
using Ae.AccountAuthority.Service.Imp.OperationLog;

namespace Ae.AccountAuthority.Service.Imp.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApolloErpLogger<AccountService> logger;
        private readonly IMapper mapper;

        private readonly IAccountRepository acctRepo;

        private readonly ProxyGenerator proxyGenerator;

        public AccountService(ApolloErpLogger<AccountService> logger, IMapper mapper,
            IAccountRepository acctRepo)
        {
            this.logger = logger;
            this.mapper = mapper;

            this.acctRepo = acctRepo;

            proxyGenerator = new ProxyGenerator();
        }


        #region 创建账号，更新密码和登录操作相关方法

        public async Task<string> CreateAccountAsync(AccountCreateEntityDTO req)
        {
            var reqDto = mapper.Map<AccountDO>(req);

            GetEncryptionPwd(req, ref reqDto);

            reqDto.Id = Guid.NewGuid().ToString();
            reqDto.UpdateBy = "";

            var res = await acctRepo.CreateAccountAsync(reqDto);
            return res;
        }

        public async Task<CreateAccountResDTO> CreateAccountWithIdPwd(AccountCreateEntityDTO req)
        {
            var res = new CreateAccountResDTO();

            var reqDto = mapper.Map<AccountDO>(req);

            if (req.Password.IsNullOrWhiteSpace())
            {
                GetEncryptionPwd(req, ref reqDto);
            }
            else
            {
                res.InitPassword = GenInitPassword(req, ref reqDto);
            }

            reqDto.Id = Guid.NewGuid().ToString();
            reqDto.CreateBy = string.Join(CommonConstant.SeparatorVertical, req.CreateBy, req.CreateById) ?? "CrtByAccountSvc";
            reqDto.UpdateBy = "";

            var acctRepoInterceptor = proxyGenerator.CreateInterfaceProxyWithTarget(acctRepo,
                new AccountLogInterceptor(logger, new LogOperationReqDTO
                {
                    LogId = reqDto.Id,
                    LogType = LogType.C.GetEnumDescription(),
                    BizType = BizType.Account.GetEnumDescription(),
                    ReqParam = JsonConvert.SerializeObject(reqDto),
                    Operator = reqDto.Id
                }));

            var resDo = await acctRepoInterceptor.CreateAccountAsync(reqDto);
            if (resDo.IsNullOrWhiteSpace())
            {
                return null;
            }

            //[res] = req.Name.IsTelephone() ? req.Name.GetDefaultPassword() : req.Name
            res.Id = resDo;
            res.Message = CommonConstant.CreateAccountPwdMsg;
            return res;
        }

        private void GetEncryptionPwd(AccountCreateEntityDTO req, ref AccountDO reqDto)
        {
            var password = req.Name;
            if (password.IsTelephone())
            {
                password = password.GetDefaultPassword();
            }
            var genPwd = EncryptPassword.CreatePasswordHash(password);
            reqDto.Password = genPwd["Password"].ToString();
            reqDto.PasswordIteration = Convert.ToInt32(genPwd["PasswordIteration"]);
            reqDto.PasswordSalt = genPwd["PasswordSalt"].ToString();
        }

        private string GenInitPassword(AccountCreateEntityDTO req, ref AccountDO reqDto)
        {
            var (defPwdStr, pwd, pwdSalt, pwdIteration) = GenResetPassword();
            reqDto.Password = pwd;
            reqDto.PasswordSalt = pwdSalt;
            reqDto.PasswordIteration = pwdIteration;
            return defPwdStr;
        }

        public async Task<AccountKeyInfoWithPwdEntityResDTO> GetAccountByNameAsync(AccountEntityReqByNameDTO req)
        {
            var res = await acctRepo.GetAccountByNameAsync(req);
            return res;
        }

        public async Task<bool> UpdateAccountPasswordAsync(AccountUpdatePasswordEntityDTO req, AccountKeyInfoWithPwdEntityResDTO acct)
        {
            var reqDto = mapper.Map<AccountDO>(req);
            reqDto.Id = acct.Id;

            GenEncryptionPwd(req, ref reqDto);

            var acctRepoInterceptor = proxyGenerator.CreateInterfaceProxyWithTarget(acctRepo,
                new AccountLogInterceptor(logger, new LogOperationReqDTO
                {
                    LogId = acct.Id,
                    LogType = LogType.U.GetEnumDescription(),
                    BizType = BizType.Account.GetEnumDescription(),
                    ReqParam = string.Join(" || ", JsonConvert.SerializeObject(reqDto), JsonConvert.SerializeObject(acct)),
                    Operator = acct.Id
                }));

            reqDto.UpdateBy = string.Join("|", req.UpdateBy, req.UpdateById);

            var res = await acctRepoInterceptor.UpdateAccountPasswordByNameAsync(reqDto);
            return res;
        }
        private void GenEncryptionPwd(AccountUpdatePasswordEntityDTO req, ref AccountDO reqDto)
        {
            var password = req.NewPassword;
            if (password.IsTelephone())
            {
                password = password.GetDefaultPassword();
            }
            var genPwd = EncryptPassword.CreatePasswordHash(password);
            reqDto.Password = genPwd["Password"].ToString();
            reqDto.PasswordIteration = Convert.ToInt32(genPwd["PasswordIteration"]);
            reqDto.PasswordSalt = genPwd["PasswordSalt"].ToString();
        }

        #endregion 创建账号，更新密码和登录操作相关方法

        #region Service相关的方法


        #endregion Service相关的方法

        #region API相关的方法

        public async Task<ApiPagedResultData<AccountPageDTO>> GetAuthorityPage(AccountPageReqDTO req)
        {
            var resDo = await acctRepo.GetAuthorityPage(req);
            var res = mapper.Map<ApiPagedResultData<AccountPageDTO>>(resDo);
            return res;
        }

        public async Task<List<AccountKeyInfoListResDTO>> GetAccountKeyInfoListById(AccountListReqDTO req)
        {
            var resDo = await acctRepo.GetAccountKeyInfoListById(req);
            var res = mapper.Map<List<AccountKeyInfoListResDTO>>(resDo);
            return res;
        }

        public async Task<List<AccountKeyInfoListResDTO>> GetAllAccountKeyInfoListAsync()
        {
            var resDo = await acctRepo.GetAllAccountListAsync();
            var res = mapper.Map<List<AccountKeyInfoListResDTO>>(resDo);
            return res;
        }

        public async Task<bool> UnlockAccountById(AccountUnlockReqByIdDTO req)
        {
            var acctRepoInterceptor = proxyGenerator.CreateInterfaceProxyWithTarget(acctRepo,
                new AccountLogInterceptor(logger, new LogOperationReqDTO
                {
                    LogId = req.Id,
                    LogType = LogType.U.GetEnumDescription(),
                    BizType = BizType.Account.GetEnumDescription(),
                    ReqParam = JsonConvert.SerializeObject(req),
                    Operator = req.UpdateBy
                }));

            var res = await acctRepoInterceptor.UnlockAccountById(req);
            return res;
        }

        public async Task<bool> LockAccountById(AccountLockReqByIdDTO req)
        {
            var acctRepoInterceptor = proxyGenerator.CreateInterfaceProxyWithTarget(acctRepo,
                new AccountLogInterceptor(logger, new LogOperationReqDTO
                {
                    LogId = $"'{string.Join("','", req.Id)}'",
                    LogType = LogType.U.GetEnumDescription(),
                    BizType = BizType.Account.GetEnumDescription(),
                    ReqParam = JsonConvert.SerializeObject(req),
                    Operator = req.UpdateBy
                }));

            var res = await acctRepoInterceptor.LockAccountById(req);
            return res;
        }

        public async Task<bool> DeleteAccountById(AccountDeleteReqByIdDTO req)
        {
            var acctRepoInterceptor = proxyGenerator.CreateInterfaceProxyWithTarget(acctRepo,
                new AccountLogInterceptor(logger, new LogOperationReqDTO
                {
                    LogId = req.Id,
                    LogType = LogType.D.GetEnumDescription(),
                    BizType = BizType.Account.GetEnumDescription(),
                    ReqParam = JsonConvert.SerializeObject(req),
                    Operator = req.UpdateBy
                }));

            var res = await acctRepoInterceptor.DeleteAccountById(req);
            return res;
        }

        public async Task<bool> DeleteBatchAccountById(AccountBatchDeleteReqByIdDTO req)
        {
            var acctRepoInterceptor = proxyGenerator.CreateInterfaceProxyWithTarget(acctRepo,
                new AccountLogInterceptor(logger, new LogOperationReqDTO
                {
                    LogId = $"'{string.Join("','", req.Id)}'",
                    LogType = LogType.D.GetEnumDescription(),
                    BizType = BizType.Account.GetEnumDescription(),
                    ReqParam = JsonConvert.SerializeObject(req),
                    Operator = req.UpdateBy
                }));

            var res = await acctRepoInterceptor.DeleteBatchAccountById(req);
            return res;
        }

        public async Task<(bool flag, string message)> ResetAccountPasswordById(AccountResetPasswordReqByIdDTO req)
        {
            var acctDo = await acctRepo.GetAccountEntityById(new AccountEntityReqDTO { Id = req.Id, IsDeleted = CommonConstant.Zero });
            if (acctDo == null)
            {
                return (false, "无效账号或是账号已被禁用");
            }

            var acctRepoInterceptor = proxyGenerator.CreateInterfaceProxyWithTarget(acctRepo,
                new AccountLogInterceptor(logger, new LogOperationReqDTO
                {
                    LogId = req.Id,
                    LogType = LogType.U.GetEnumDescription(),
                    BizType = BizType.Account.GetEnumDescription(),
                    ReqParam = JsonConvert.SerializeObject(req),
                    Operator = req.UpdateBy
                }));

            var reqDto = mapper.Map<AccountResetPasswordReqByIdIntlDTO>(req);
            var (defPwdStr, pwd, pwdSalt, pwdIteration) = GenResetPassword();
            reqDto.Password = pwd;
            reqDto.PasswordSalt = pwdSalt;
            reqDto.PasswordIteration = pwdIteration;

            var res = await acctRepoInterceptor.UpdateAccountPasswordById(reqDto);
            return (res, res ? defPwdStr : "密码重置失败");
        }
        private (string defPwdStr, string pwd, string pwdSalt, int pwdIteration) GenResetPassword()
        {
            var defPwdStr = RandomString.GenResetPasswordRandomString();
            var hashPwd = EncryptPassword.CreatePasswordHash(defPwdStr);
            return (defPwdStr, hashPwd["Password"].ToString(), hashPwd["PasswordSalt"].ToString(), Convert.ToInt32(hashPwd["PasswordIteration"]));
        }

        #endregion API相关的方法
    }
}
