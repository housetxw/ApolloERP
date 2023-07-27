using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.B.Login.Api.Dal.Model;

namespace Ae.B.Login.Api.Dal.Repositorys.IDAL
{
    public interface ILoginLogRepository
    {
        Task CreateLoginLogAsync(SysLoginLogDO req);

    }
}
