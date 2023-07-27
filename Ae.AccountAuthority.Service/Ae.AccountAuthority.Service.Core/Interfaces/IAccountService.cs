using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.AccountAuthority.Service.Core.Model;
using Ae.AccountAuthority.Service.Core.Request;
using Ae.AccountAuthority.Service.Core.Response;

namespace Ae.AccountAuthority.Service.Core.Interfaces
{
    public interface IAccountService
    {
        #region 创建账号，更新密码和登录操作相关方法

        Task<string> CreateAccountAsync(AccountCreateEntityDTO req);

        Task<CreateAccountResDTO> CreateAccountWithIdPwd(AccountCreateEntityDTO req);

        Task<AccountKeyInfoWithPwdEntityResDTO> GetAccountByNameAsync(AccountEntityReqByNameDTO req);

        Task<bool> UpdateAccountPasswordAsync(AccountUpdatePasswordEntityDTO req, AccountKeyInfoWithPwdEntityResDTO acct);


        #endregion 创建账号，更新密码和登录操作相关方法

        #region Service相关的方法

        Task<ApiPagedResultData<AccountPageDTO>> GetAuthorityPage(AccountPageReqDTO req);

        Task<List<AccountKeyInfoListResDTO>> GetAccountKeyInfoListById(AccountListReqDTO req);

        Task<List<AccountKeyInfoListResDTO>> GetAllAccountKeyInfoListAsync();

        Task<bool> UnlockAccountById(AccountUnlockReqByIdDTO req);

        Task<bool> LockAccountById(AccountLockReqByIdDTO req);

        Task<bool> DeleteAccountById(AccountDeleteReqByIdDTO req);

        Task<bool> DeleteBatchAccountById(AccountBatchDeleteReqByIdDTO req);

        Task<(bool flag, string message)> ResetAccountPasswordById(AccountResetPasswordReqByIdDTO req);


        #endregion Service相关的方法
    }
}
