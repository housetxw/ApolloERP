using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.B.Login.Api.Core.Request;
using Ae.B.Login.Api.Core.Response;

namespace Ae.B.Login.Api.Core.Interfaces
{
    public interface IAccountService
    {
        string GetSMSCode(ApiRequest<LoginPhoneVO> req);

        Task<AccountKeyInfoWithPwdEntityResVO> GetAccountByNameAsync(AccountEntityReqByNameVO req);

        Task<bool> UpdateAccountStateOrErrorCountByIdAsync(AccountStateOrErrorCountReqVO req);

        Task<bool> ResetAccountStateAndErrorCountByIdOrNameAsync(AccountStateOrErrorCountReqVO req);

        Task<bool> UpdateAccountPasswordByNameAsync(UpdateAccountPwdVO req);

    }
}
