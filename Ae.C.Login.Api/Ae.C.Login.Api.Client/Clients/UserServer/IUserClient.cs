using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.C.Login.Api.Client.Request;

namespace Ae.C.Login.Api.Client.Clients.UserServer
{
    public interface IUserClient
    {
        Task<bool> OperateUserPoint(OperateUserPointRequest req);

    }
}
