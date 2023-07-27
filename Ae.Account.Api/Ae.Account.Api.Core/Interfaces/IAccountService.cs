using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.Account.Api.Client.Request;
using Ae.Account.Api.Client.Response;
using Ae.Account.Api.Core.Model;
using Ae.Account.Api.Core.Request;
using Ae.Account.Api.Core.Response;

namespace Ae.Account.Api.Core.Interfaces
{
    public interface IAccountService
    {
        Task<ApiPagedResultData<AccountPageResVO>> GetAuthorityPage(AccountPageReqDTO req);

        Task<List<AccountKeyInfoListDTO>> GetAccountKeyInfoListById(AccountListReqDTO req);

        Task<List<AccountKeyInfoListDTO>> GetAllAccountKeyInfoListAsync();

        Task<bool> UnlockAccountById(AccountUnlockReqByIdVO req);

        Task<bool> LockAccountById(AccountLockReqByIdVO req);

        Task<bool> DeleteBatchAccountById(AccountBatchDeleteReqByIdVO req);

        Task<AccountResetPasswordResByIdVO> ResetAccountPasswordById(AccountResetPasswordReqByIdVO req);

        Task<ApiResult<bool>> UpdatePasswordAsync(AccountUpdatePasswordEntityVO req);

    }
}
