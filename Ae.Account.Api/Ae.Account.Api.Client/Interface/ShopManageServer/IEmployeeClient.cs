using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.Account.Api.Client.Model;
using Ae.Account.Api.Client.Request;
using Ae.Account.Api.Client.Response;

namespace Ae.Account.Api.Client.Interface
{
    public interface IEmployeeClient
    {
        Task<List<EmployeeResDTO>> GetAllEmployeeListAsync();

        Task<List<EmployeeResDTO>> GetEmployeeListByOrgIdAndTypeAsync(EmployeeListReqDTO req);

        Task<ApiPagedResultData<EmployeePageDTO>> GetEmployeePage(EmployeePageReqDTO req);

        Task<List<long>> GetOrgRangeRoleIdList(OrgRangeRoleListForLoginReqDTO req);

    }

}
