using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.B.Login.Api.Core.Model;

namespace Ae.B.Login.Api.Core.Interfaces
{
    public interface ILoginLogService
    {
        Task CreateLoginLogAsync(SysLoginLogVO req);

    }
}
