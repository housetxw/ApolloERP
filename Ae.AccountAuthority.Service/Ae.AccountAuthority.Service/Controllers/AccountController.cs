using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.AccountAuthority.Service.Common.Constant;
using Ae.AccountAuthority.Service.Common.Extension;
using Ae.AccountAuthority.Service.Common.Security;
using Ae.AccountAuthority.Service.Core.Interfaces;
using Ae.AccountAuthority.Service.Core.Model;
using Ae.AccountAuthority.Service.Core.Request;
using Ae.AccountAuthority.Service.Core.Response;
using Ae.AccountAuthority.Service.Filters;

namespace Ae.AccountAuthority.Service.Controllers
{
    [Route("[controller]/[action]")]
    //[Filter(nameof(AccountController))]
    public class AccountController : Controller
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly ApolloErpLogger<AccountController> logger;

        private readonly IAccountService acctSvc;

        public AccountController(ApolloErpLogger<AccountController> logger, IAccountService acctSvc)
        {
            this.logger = logger;
            this.acctSvc = acctSvc;
        }

        // ---------------------------------- 创建账号，更新密码和登录操作相关方法 --------------------------------------
        /// <summary>
        /// 创建账号，返回True OR False
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Obsolete("暂时未使用到")]
        private async Task<ApiResult<bool>> CreateAccountAsync([FromBody] AccountCreateEntityDTO req)
        {
            var acct = await acctSvc.GetAccountByNameAsync(new AccountEntityReqByNameDTO { Name = req.Name });
            if (null != acct && acct.Name.EqualsIgnoreCase(req.Name))
            {
                return Result.Failed<bool>(ResultCode.SUCCESS_EXIST, $"账号 {req.Name} 已存在");
            }

            var res = await acctSvc.CreateAccountAsync(req);
            if (res.IsNullOrWhiteSpace())
            {
                return Result.Failed<bool>(CommonConstant.OperateFailure);
            }
            return Result.Success(res.IsNotNullOrWhiteSpace(), CommonConstant.OperateSuccess);
        }

        /// <summary>
        /// 创建账号，并返回accountId
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Obsolete("暂时未使用到")]
        private async Task<ApiResult<string>> CreateAccountWithIdAsync([FromBody] AccountCreateEntityDTO req)
        {
            var acct = await acctSvc.GetAccountByNameAsync(new AccountEntityReqByNameDTO { Name = req.Name });
            if (null != acct && acct.Name.EqualsIgnoreCase(req.Name))
            {
                return Result.Failed<string>(ResultCode.SUCCESS_EXIST, $"账号 {req.Name} 已存在");
            }

            var res = await acctSvc.CreateAccountAsync(req);
            if (res.IsNullOrWhiteSpace())
            {
                return Result.Failed<string>(CommonConstant.OperateFailure);
            }
            return Result.Success(res, CommonConstant.OperateSuccess);
        }

        /// <summary>
        /// 创建账号，并返回账号Id
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<CreateAccountResDTO>> CreateAccountWithIdDefaultPwd([FromBody] AccountCreateEntityDTO req)
        {
            if (null == req || !req.Name.IsTelephone())
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return Result.Failed<CreateAccountResDTO>(ResultCode.ArgumentError, "用户名或是账号格式不正确");
            }

            var acct = await acctSvc.GetAccountByNameAsync(new AccountEntityReqByNameDTO { Name = req.Name });
            if (null != acct && acct.Name.EqualsIgnoreCase(req.Name))
            {
                return Result.Success(new CreateAccountResDTO
                {
                    Id = acct.Id,
                    Message = $"账号 {req.Name} 已存在",
                    HasAccount = true
                }, CommonConstant.OperateSuccess);
            }

            var res = await acctSvc.CreateAccountWithIdPwd(req);
            if (res == null)
            {
                return Result.Failed<CreateAccountResDTO>(CommonConstant.OperateFailure);
            }
            return Result.Success(res, CommonConstant.OperateSuccess);
        }

        /// <summary>
        /// 根据登录账号，获取账号关键性信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns>返回accountId</returns>
        [HttpGet]
        public async Task<ApiResult<AccountKeyInfoWithPwdEntityResDTO>> GetAccountByNameAsync([FromQuery] AccountEntityReqByNameDTO req)
        {
            var res = await acctSvc.GetAccountByNameAsync(req);
            return Result.Success(res, CommonConstant.OperateSuccess);
        }

        /// <summary>
        /// 根据账号和旧密码，修改密码
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> UpdatePasswordAsync([FromBody] AccountUpdatePasswordEntityDTO req)
        {
            var acct = await acctSvc.GetAccountByNameAsync(new AccountEntityReqByNameDTO { Name = req.Name });
            if (null == acct || acct.Name.NotEqualsIgnoreCase(req.Name))
            {
                return Result.Failed<bool>(ResultCode.Failed, $"账号 {req.Name} 不存在");
            }

            if (req.NewPassword.Length < 8 || req.NewPassword.Length > 18)
            {
                return Result.Failed<bool>(ResultCode.Failed, "密码长度至少是8位，且不能超过18位");
            }

            var hashTable = new Hashtable
            {
                {"Password", acct.Password},
                {"PasswordIteration", acct.PasswordIteration},
                {"PasswordSalt", acct.PasswordSalt}
            };
            var validPwd = EncryptPassword.ValidatePassword(req.Password, hashTable);
            if (!validPwd)
            {
                return Result.Failed<bool>(ResultCode.Failed, "账号或是旧密码不正确");
            }

            var res = await acctSvc.UpdateAccountPasswordAsync(req, acct);
            if (!res)
            {
                return Result.Failed<bool>(CommonConstant.OperateFailure);
            }
            return Result.Success(true, CommonConstant.OperateSuccess);
        }

        /// <summary>
        /// 根据登录账号，判断账号是否已经存在
        /// </summary>
        /// <param name="req"></param>
        /// <returns>返回accountId</returns>
        [HttpGet]
        public async Task<ApiResult<CheckAccountExistDTO>> CheckAccountIsExistByName([FromQuery] AccountEntityReqByNameDTO req)
        {
            var res = await acctSvc.GetAccountByNameAsync(new AccountEntityReqByNameDTO { Name = req.Name });
            
            var hasAccount = res != null;

            return Result.Success(new CheckAccountExistDTO
            {
                AccountId = hasAccount ? res.Id : "",
                Message = hasAccount ? $"账号 {req.Name} 已存在" : $"账号 {req.Name} 不存在存在",
                HasAccount = hasAccount
            }, CommonConstant.QuerySuccess);
        }

        // ---------------------------------- Service相关的方法 --------------------------------------
        /// <summary>
        /// 获取账号分页列表信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResultData<AccountPageDTO>> GetAuthorityPage([FromQuery] AccountPageReqDTO req)
        {
            var res = await acctSvc.GetAuthorityPage(req);
            return res;
        }

        /// <summary>
        /// 根据筛条件，获取账号的关键信息列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<AccountKeyInfoListResDTO>> GetAccountKeyInfoListById([FromBody] AccountListReqDTO req)
        {
            var res = await acctSvc.GetAccountKeyInfoListById(req);
            return res;
        }

        /// <summary>
        /// 获取所有账号的关键信息列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<AccountKeyInfoListResDTO>> GetAllAccountKeyInfoListAsync()
        {
            var res = await acctSvc.GetAllAccountKeyInfoListAsync();
            return res;
        }

        /// <summary>
        /// 根据AccountId，解锁账号
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> UnlockAccountById([FromBody] AccountUnlockReqByIdDTO req)
        {
            if (null == req)
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return false;
            }

            var res = await acctSvc.UnlockAccountById(req);
            return res;
        }

        /// <summary>
        /// 根据AccountId，批量锁定账号
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> LockAccountById([FromBody] AccountLockReqByIdDTO req)
        {
            if (null == req)
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return false;
            }
            var res = await acctSvc.LockAccountById(req);
            return res;
        }

        /// <summary>
        /// 根据AccountId，删除账号
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> DeleteAccountById([FromBody] AccountDeleteReqByIdDTO req)
        {
            if (null == req)
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return false;
            }

            var res = await acctSvc.DeleteAccountById(req);
            return res;
        }

        /// <summary>
        /// 根据AccountId，批量删除账号
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> DeleteBatchAccountById([FromBody] AccountBatchDeleteReqByIdDTO req)
        {
            if (null == req)
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return false;
            }
            var res = await acctSvc.DeleteBatchAccountById(req);
            return res;
        }

        /// <summary>
        /// 根据AccountId，重置密码
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AccountResetPasswordResByIdDTO> ResetAccountPasswordById([FromBody] AccountResetPasswordReqByIdDTO req)
        {
            var res = new AccountResetPasswordResByIdDTO();
            if (null == req)
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());

                res.Message = CommonConstant.ArgumentError;
                return res;
            }

            var (flag, message) = await acctSvc.ResetAccountPasswordById(req);
            res.flag = flag;
            if (flag)
            {
                res.ResetPassword = message;
                res.Message = CommonConstant.OperateSuccess;
            }
            else
            {
                res.Message = message;
            }
            return res;
        }

    }
}