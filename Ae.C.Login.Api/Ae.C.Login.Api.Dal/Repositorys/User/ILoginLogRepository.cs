using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.C.Login.Api.Dal.Model;

namespace Rg.LoginAuthentication.Api.Dal.Repositorys.IDAL
{
    public interface ILoginLogRepository
    {
        Task CreateLoginLogAsync(SysLoginLogDO req);

    }
}
