using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.Account.Api.Client.Model;
using Ae.Account.Api.Client.Request;
using Ae.Account.Api.Client.Response;

namespace Ae.Account.Api.Client.Interface.AccountAuthorityServer
{
    public interface IAccountClient
    {
        Task<ApiPagedResultData<AccountPageDTO>> GetAuthorityPage(AccountPageReqDTO req);

        Task<List<AccountKeyInfoListDTO>> GetAccountKeyInfoListById(AccountListReqDTO req);

        Task<List<AccountKeyInfoListDTO>> GetAllAccountKeyInfoListAsync();

        Task<bool> UnlockAccountById(AccountUnlockReqByIdDTO req);

        Task<bool> LockAccountById(AccountLockReqByIdDTO req);

        Task<bool> DeleteBatchAccountById(AccountBatchDeleteReqByIdDTO req);

        Task<AccountResetPasswordResByIdDTO> ResetAccountPasswordById(AccountResetPasswordReqByIdDTO req);

        Task<ApiResult<bool>> UpdatePasswordAsync(AccountUpdatePasswordEntityDTO req);


    }
}
