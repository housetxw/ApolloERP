using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.B.Login.Api.Core.Request;
using Ae.B.Login.Api.Core.Response;

namespace Ae.B.Login.Api.Dal.Repositorys.IDAL
{
    public interface IAccountRepository
    {
        #region 登录操作相关方法

        Task<AccountKeyInfoWithPwdEntityResVO> GetAccountByNameAsync(AccountEntityReqByNameVO req);

        Task<bool> UpdateAccountStateOrErrorCountByIdAsync(AccountStateOrErrorCountReqVO req);

        Task<bool> ResetAccountStateAndErrorCountByIdOrNameAsync(AccountStateOrErrorCountReqVO req);

        Task<bool> UpdateAccountPasswordByNameAsync(UpdateAccountPwdVO req);

        #endregion 登录操作相关方法
    }
}
