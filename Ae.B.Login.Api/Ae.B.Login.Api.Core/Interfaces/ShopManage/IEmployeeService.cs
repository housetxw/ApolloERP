using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.B.Login.Api.Client.Request.ShopManage.Employee;
using Ae.B.Login.Api.Core.Request;
using Ae.B.Login.Api.Core.Response;

namespace Ae.B.Login.Api.Core.Interfaces.ShopManage
{
    public interface IEmployeeService
    {
        Task<List<OrganizationVO>> GetEmpAndOrgListForLoginByAccountIdAsync(EmployeeListReqDTO req);

        Task<ApiPagedResult<OrganizationVO>> GetEmpAndOrgPageForLoginByAccountIdAsync(EmployeePageForLoginListReqDTO req);

        Task<ApiPagedResult<OrganizationVO>> GetEmpAndOrgPageForLoginByEmpIdAsync(EmployeePageForLoginListByTokenReqDTO req);

        Task<ApiPagedResult<OrganizationVO>> GetEmpAndOrgPageForLoginByEmpIdExcCurOrgIdAsync(EmployeePageForLoginListByTokenReqDTO req);

        Task<ApiPagedResult<OrganizationVO>> GetOrgRangePageForLoginByEmpIdExcCurOrgId(EmployeePageForLoginListByTokenReqDTO req);


    }
}
