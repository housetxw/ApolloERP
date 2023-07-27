using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.Account.Api.Core.Request;
using Ae.Account.Api.Core.Response;

namespace Ae.Account.Api.Core.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeResVO>> GetAllEmployeeListAsync();

        Task<List<EmployeeResVO>> GetEmployeeListByOrgIdAndTypeAsync(EmployeeListReqVO req);

        Task<ApiPagedResultData<EmployeePageResVO>> GetEmployeePage(EmployeePageReqVO req);

    }
}
