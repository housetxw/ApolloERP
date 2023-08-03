using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Client.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Client.Interface
{
    public interface IEmployeeClient
    {
        Task<GetEmployeeClientResponse> GetEmployeeSimpleInfo(GetEmployeeInfoClientRequest request);
    }
}
