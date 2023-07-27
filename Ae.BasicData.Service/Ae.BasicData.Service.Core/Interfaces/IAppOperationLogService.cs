using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.BasicData.Service.Core.Request;

namespace Ae.BasicData.Service.Core.Interfaces
{
    public interface IAppOperationLogService
    {
        Task<bool> CreateAppOperationLog(CreateAppOperationLogReqDTO req);


    }
}
