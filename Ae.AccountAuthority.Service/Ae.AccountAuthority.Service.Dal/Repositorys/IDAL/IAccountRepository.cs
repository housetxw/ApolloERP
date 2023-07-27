using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.AccountAuthority.Service.Core.Model;
using Ae.AccountAuthority.Service.Core.Request;
using Ae.AccountAuthority.Service.Core.Response;
using Ae.AccountAuthority.Service.Dal.Model;

namespace Ae.AccountAuthority.Service.Dal.Repositorys.IDAL
{
    public interface IAccountRepository
    {
        #region 创建账号，更新密码和登录操作相关方法

        Task<string> CreateAccountAsync(AccountDO req);

        //Task<bool> RegisterAccountAsync(AccountDO req);

        Task<AccountKeyInfoWithPwdEntityResDTO> GetAccountByNameAsync(AccountEntityReqByNameDTO req);

        Task<bool> UpdateAccountPasswordByNameAsync(AccountDO req);


        #endregion 创建账号，更新密码和登录操作相关方法

        #region Service相关的方法


        #endregion Service相关的方法

        #region API相关的方法

        Task<PagedEntity<AccountPageDTO>> GetAuthorityPage(AccountPageReqDTO req);

        Task<List<AccountDO>> GetAccountKeyInfoListById(AccountListReqDTO req);

        Task<List<AccountDO>> GetAllAccountListAsync();

        Task<AccountDO> GetAccountEntityById(AccountEntityReqDTO req);

        Task<bool> UnlockAccountById(AccountUnlockReqByIdDTO req);

        Task<bool> LockAccountById(AccountLockReqByIdDTO req);

        Task<bool> DeleteAccountById(AccountDeleteReqByIdDTO req);

        Task<bool> DeleteBatchAccountById(AccountBatchDeleteReqByIdDTO req);

        Task<bool> UpdateAccountPasswordById(AccountResetPasswordReqByIdIntlDTO req);


        #endregion API相关的方法
    }
}
