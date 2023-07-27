using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.BasicData.Service.Dal.Model;

namespace Ae.BasicData.Service.Dal.Repositorys.IDAL
{
    public interface IAppOperationLogRepository
    {
        Task<bool> CreateAppOperationLog(AppOperationLogDO req);

    }
}
